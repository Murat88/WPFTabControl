using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BLL = OzyaysanBusinessEngine;
using System.Data;
using Ozyaysan.Windows;
using System.Collections.ObjectModel;
using System.Transactions;
using System.ComponentModel;

namespace Ozyaysan.UserControls
{
    /// <summary>
    /// Interaction logic for UCOrderDetail.xaml
    /// </summary>
    public partial class UCOrderDetail : UserControl
    {

        #region Constructors
        public UCOrderDetail(int OID)
        {
            InitializeComponent();
            DataContext = this;
            this.OID = OID;
            this.oOrder = new BLL.Order(this.OID, BLL.Enumarations.State.Aktif);
            LoadData();
            LoadDetail();
          //  ConfigureControlVisibilityByRole();
            //this.lstSelectedProducts.SelectedIndex = 0;
            //ListBoxItem listItem = this.lstSelectedProducts.ItemContainerGenerator.ContainerFromIndex(this.lstSelectedProducts.SelectedIndex) as ListBoxItem;
            //BindOrderDetailPanel(listItem.Uid);

        }
        #endregion

        #region RoleRelated Part
        //private void ConfigureControlVisibilityByRole()
        //{
        //    if (oCurrentUser.UserType == BLL.Enumarations.UserType.Owner)
        //    {
        //        cmbOperator.Visibility = Visibility.Hidden;
        //        lblOperatorName.Visibility = Visibility.Visible;
        //    }
        //    else if (oCurrentUser.UserType == BLL.Enumarations.UserType.Master)
        //    {
        //        cmbOperator.Visibility = Visibility.Visible;
        //        DataTable dtOperator = BLL.Operator.GetOpearatorListForDDL();
        //        cmbOperator.ItemsSource = dtOperator.DefaultView;
        //        cmbOperator.DisplayMemberPath = dtOperator.Columns["NameSurName"].ToString();
        //        cmbOperator.SelectedValuePath = dtOperator.Columns["OPID"].ToString();
        //        cmbOperator.SelectedIndex = 0;
        //        lblOperatorName.Content = "";
        //        lblOperatorName.Visibility = Visibility.Hidden;
        //        txtCode.IsEnabled = false;
        //        txtOrderCount.IsEnabled = false;
        //        txtPackageType.IsEnabled = false;
        //        cmbColor.IsEnabled = false;
        //        cmbCustomer.IsEnabled = false;
        //        cmbOrderStatus.IsEnabled = false;
        //        //cmbState.IsEnabled = false;
        //        imgAddProduct.IsEnabled = false;
        //        imgColorSearch.IsEnabled = false;
        //        btnSearch.Visibility = Visibility.Hidden;
        //        tblOrderUpdate.Visibility = Visibility.Hidden;
        //        stkSellPrice.Visibility = Visibility.Collapsed;
        //        //stkTotalPrice.Visibility = Visibility.Collapsed;
        //    }
        //    else if (oCurrentUser.UserType == BLL.Enumarations.UserType.Painter)
        //    {
        //        cmbOperator.Visibility = Visibility.Hidden;
        //        lblOperatorName.Visibility = Visibility.Visible;
        //    }
        //}
        #endregion

        #region Fields

        private bool _panelLoading;
        private string _panelMainMessage = "Main Loading Message";
        private string _panelSubMessage = "Sub Loading Message";

        BLL.User oCurrentUser = (ApplicationState.GetValue<object>("user") as BLL.User);

        bool StartModeOn = false;
        bool StartModeOnForOPStatus = false;
        int totalProductionTime = 0;
        decimal totalCost = 0;
        int m_OID;
        BLL.Order oOrder;
        BLL.Product m_SelectedProduct;
        DataTable dtDetailedOrder;
        ObservableCollection<BLL.Product> oProductList = new ObservableCollection<BLL.Product>();
        List<BLL.Product> oNewAddedProductList = new List<BLL.Product>();
        #endregion

        #region Properties



        public BLL.Product SelectedProduct
        {
            get { return m_SelectedProduct; }
            set { m_SelectedProduct = value; }
        }
        public int OID
        {
            get { return m_OID; }
            set { m_OID = value; }
        }
        #endregion

        #region events

        #region MouseDown
        private void tblUseRawMaterials_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MouseButtonState.Pressed == e.LeftButton)
            {
                Click_btnRawMaterials();
            }
        }
        private void tblUseStock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MouseButtonState.Pressed == e.LeftButton)
            {
                // Click_btnUseStock();
            }
        }
        private void tblOrderUpdate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MouseButtonState.Pressed == e.LeftButton)
            {
                Click_btnUpdate();
            }
        }
        private void tblProductUpdate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MouseButtonState.Pressed == e.LeftButton)
            {
                Click_btnProductUpdate();
            }
        }
        private void tblExamineProduct_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MouseButtonState.Pressed == e.LeftButton)
            {
                Click_btnExamine();
            }
        }
        private void imgAddProduct_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var window = new W_AddProductForOrder()
                {
                    Title = "Siparise Urun Ekle",
                    ShowInTaskbar = false,
                    Topmost = true,
                    ResizeMode = ResizeMode.CanMinimize,
                    Owner = Application.Current.MainWindow,
                };
                if (window.ShowDialog() == false)
                {
                    BLL.Product AddedProduct = window.AddedProduct;
                    if (AddedProduct != null)
                    {
                        if (IfExistsSameProductOrNot(AddedProduct))
                        {
                            MessageBox.Show("Seçmiş olduğunuz yay zaten siparişte mevcut!!");
                        }
                        else
                        {
                            this.oNewAddedProductList.Add(AddedProduct);
                            this.oProductList.Add(AddedProduct);
                            dgOrderProduct.Items.Add(AddedProduct);
                            CalculateTotalProductionTimeForNewProduct(AddedProduct);

                            dgOrderProduct.Items.Refresh();

                        }
                    }
                }
            }
        }
        private void lblDeleteProduct_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (dgOrderProduct.SelectedIndex >= 0)
                {
                    dgOrderProduct.Items.RemoveAt(dgOrderProduct.SelectedIndex);
                }

            }
        }
        #endregion

        #region Click
        private void btnExamine_Click(object sender, RoutedEventArgs e)
        {
            Click_btnExamine();
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Click_btnUpdate();
        }
        private void btnProductUpdate_Click(object sender, RoutedEventArgs e)
        {
            Click_btnProductUpdate();

        }
        private void btnUseRawMaterials_Click(object sender, RoutedEventArgs e)
        {
            Click_btnRawMaterials();
        }




        #endregion

        #region PreviewKeyDown
        private void txtCount_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            AllowOnlyNumeric(e);
        }
        private void txtSellPrice_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            AllowOnlyNumericAndDecimal(e);
        }
        #endregion

        #region SelectionChanged
        //private void lstSelectedProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //    ListBoxItem listItem = this.lstSelectedProducts.ItemContainerGenerator.ContainerFromIndex(this.lstSelectedProducts.SelectedIndex) as ListBoxItem;
        //    if (brdRightContent.Visibility == Visibility.Hidden)
        //    {
        //        brdRightContent.Visibility = Visibility.Visible;
        //        brdRightContentEmptyTemplate.Visibility = Visibility.Hidden;
        //        BindOrderDetailPanel(listItem.Uid);
        //    }
        //    else
        //    {
        //        BindOrderDetailPanel(listItem.Uid);
        //    }

        //}
        //private void cmbOPStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (cmbOPStatus.SelectedIndex == 1)
        //    {
        //        if (!StartModeOnForOPStatus)
        //        {
        //            if (oCurrentUser.UserType == BLL.Enumarations.UserType.Master)
        //            {
        //                Click_btnRawMaterials();
        //            }
        //        }
        //    }
        //}
        #endregion

        #region Checked
        //private void chcStock_Checked(object sender, RoutedEventArgs e)
        //{
        //    // Renk kontrolü yapılacak
        //    BLL.Stock stock = new BLL.Stock(this.SelectedProduct.ID);
        //    if (!StartModeOn)
        //    {
        //        if (stock.Count > 0)
        //        {
        //            var window = new W_UseStock(this.SelectedProduct.ID, Int32.Parse(txtOrderCount.Text), this.oOrder.ID, true)
        //            {
        //                ShowInTaskbar = false,
        //                Topmost = true,
        //                ResizeMode = ResizeMode.CanMinimize,
        //                Owner = Application.Current.MainWindow,
        //            };
        //            if (window.ShowDialog() == false)
        //            {
        //                if (!window.IsSuccesfulOperation)
        //                {
        //                    StartModeOn = true;
        //                    chcStock.IsChecked = false;
        //                    StartModeOn = false;
        //                }
        //                dtDetailedOrder = BLL.Order.GetOrderDetailListByOrderID(this.OID);
        //                BindOrderDetailPanel(this.SelectedProduct.ID.ToString());
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("Stokta bu yaydan bulunmamaktadır.");
        //            StartModeOn = true;
        //            chcStock.IsChecked = false;
        //            StartModeOn = false;
        //        }
        //    }
        //}
        //private void chcStock_Unchecked(object sender, RoutedEventArgs e)
        //{
        //    if (!StartModeOn)
        //    {
        //        foreach (DataRow oRow in dtDetailedOrder.Rows)
        //        {
        //            if (oRow["PID"].ToString() == this.SelectedProduct.ID.ToString())
        //            {
        //                int nResult = BLL.Order.UpdateOrderDetailForStock(this.oOrder.ID, this.SelectedProduct.ID, 0, 0);
        //                if (nResult == 0)
        //                {
        //                    MessageBox.Show("İşlem başarılı");
        //                    dtDetailedOrder = BLL.Order.GetOrderDetailListByOrderID(this.OID);
        //                    BindOrderDetailPanel(this.SelectedProduct.ID.ToString());
        //                }
        //                else
        //                {
        //                    MessageBox.Show("Bir hata oluştu");
        //                }
        //            }
        //        }
        //    }
        //}
        #endregion
        #endregion

        #region Custom Methots
        private void AllowOnlyNumeric(KeyEventArgs e)
        {

            switch (e.Key)
            {
                case Key.D0:
                case Key.D1:
                case Key.D2:
                case Key.D3:
                case Key.D4:
                case Key.D5:
                case Key.D6:
                case Key.D7:
                case Key.D8:
                case Key.D9:
                case Key.NumLock:
                case Key.NumPad0:
                case Key.NumPad1:
                case Key.NumPad2:
                case Key.NumPad3:
                case Key.NumPad4:
                case Key.NumPad5:
                case Key.NumPad6:
                case Key.NumPad7:
                case Key.NumPad8:
                case Key.NumPad9:
                case Key.Back:
                    break;
                default:
                    e.Handled = true;
                    break;
            }
        }
        private void AllowOnlyNumericAndDecimal(KeyEventArgs e)
        {

            switch (e.Key)
            {
                case Key.Decimal:
                case Key.D0:
                case Key.D1:
                case Key.D2:
                case Key.D3:
                case Key.D4:
                case Key.D5:
                case Key.D6:
                case Key.D7:
                case Key.D8:
                case Key.D9:
                case Key.NumLock:
                case Key.NumPad0:
                case Key.NumPad1:
                case Key.NumPad2:
                case Key.NumPad3:
                case Key.NumPad4:
                case Key.NumPad5:
                case Key.NumPad6:
                case Key.NumPad7:
                case Key.NumPad8:
                case Key.NumPad9:
                case Key.Back:
                    break;
                default:
                    e.Handled = true;
                    break;
            }
        }
        private bool IsExistSmallerStatus(int OS)
        {
            foreach (DataRow oRow in dtDetailedOrder.Rows)
            {
                if (Int32.Parse(oRow["OPStatus"].ToString()) < OS)
                {
                    return true;
                }
            }

            return false;
        }
        //private void imgColorSearch_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    if (e.LeftButton == MouseButtonState.Pressed)
        //    {
        //        var window = new W_PickColorForOrder()
        //        {
        //            Title = "Boyanacak Rengi Seç",
        //            ShowInTaskbar = false,
        //            Topmost = true,
        //            ResizeMode = ResizeMode.CanMinimize,
        //            Owner = Application.Current.MainWindow,
        //        };
        //        if (window.ShowDialog() == false)
        //        {
        //            BLL.Color SelectedColor = window.SelectedColor;
        //            if (SelectedColor != null && SelectedColor.ID != 0)
        //            {
        //                cmbColor.SelectedValue = SelectedColor.ID.ToString();
        //            }

        //        }
        //    }
        //}
        private void CalculateTotalProductionTimeForNewProduct(BLL.Product P)
        {
            DataTable dt = BLL.Product.CalculateProductionTimeByPIDAndCount(P.ID, P.ProductionCount);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["ProductionTime"] != null && dt.Rows[0]["ProductionTime"].ToString() != "")
                {
                    totalProductionTime += Int32.Parse(dt.Rows[0]["ProductionTime"].ToString()) / 420;
                    string DayValue = (totalProductionTime).ToString();
                    lblTotalProductionTime.Content = DayValue;
                }

            }

        }
        private void CalculateTotalProductionTimeSelectedProduct(int difference)
        {
            DataTable dt = BLL.Product.CalculateProductionTimeByPIDAndCount(SelectedProduct.ID, difference);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["ProductionTime"] != null && dt.Rows[0]["ProductionTime"].ToString() != "")
                {
                    totalProductionTime += Int32.Parse(dt.Rows[0]["ProductionTime"].ToString()) / 420;
                    string DayValue = (totalProductionTime).ToString();
                    lblTotalProductionTime.Content = DayValue;
                }

            }
        }

        private string ValidationMethot()
        {
            string ErrorMessage = "";
            if (dpDateOfDelivery.SelectedDate == null)
            {
                ErrorMessage += "Lütfen bir teslimat tarihini seçiniz !" + Environment.NewLine + "";
            }
            if (dgOrderProduct.Items.Count == 0)
            {
                ErrorMessage += "Lütfen siparişe bir ürün ekleyiniz !" + Environment.NewLine + "";
            }
            if (cmbCustomer.SelectedIndex == 0)
            {
                ErrorMessage += "Lütfen bir müşteri seçiniz !" + Environment.NewLine + "";
            }
            return ErrorMessage;
        }
        bool IfExistsSameProductOrNot(BLL.Product SelectedProduct)
        {
            foreach (BLL.Product item in this.oProductList)
            {
                if (SelectedProduct.ID == item.ID)
                {
                    return true;
                }
            }
            return false;
        }
        private void UpdateGeneralOrderStatus(int Status)
        {
            if (dgOrderProduct.Items.Count == 1)
            {
                switch (Status)
                {
                    case 1://bekleme
                        cmbOrderStatus.SelectedIndex = 0;
                        cmbOrderStatus.Foreground = Brushes.Red;
                        break;
                    case 2://imalatta
                        cmbOrderStatus.SelectedIndex = 1;
                        cmbOrderStatus.Foreground = Brushes.Goldenrod;
                        break;
                    case 3://boyahane
                        cmbOrderStatus.SelectedIndex = 2;
                        cmbOrderStatus.Foreground = Brushes.HotPink;
                        break;
                    case 4://boyanıyor
                        cmbOrderStatus.SelectedIndex = 3;
                        cmbOrderStatus.Foreground = Brushes.Orange;
                        break;
                    case 5://hazır
                        cmbOrderStatus.SelectedIndex = 4;
                        cmbOrderStatus.Foreground = Brushes.Green;
                        break;
                    case 6://Gönderildi
                        cmbOrderStatus.SelectedIndex = 5;
                        cmbOrderStatus.Foreground = Brushes.Gray;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (Status)
                {
                    case 1://bekleme
                        cmbOrderStatus.SelectedIndex = 0;
                        cmbOrderStatus.Foreground = Brushes.Red;
                        break;
                    case 2://imalatta
                        if (!IsExistSmallerStatus((int)BLL.Enumarations.OrderStatus.Imalatta))
                        {
                            cmbOrderStatus.SelectedIndex = 1;
                            cmbOrderStatus.Foreground = Brushes.Goldenrod;
                        }
                        break;
                    case 3://boyahane
                        if (!IsExistSmallerStatus((int)BLL.Enumarations.OrderStatus.Boyahanede))
                        {
                            cmbOrderStatus.SelectedIndex = 2;
                            cmbOrderStatus.Foreground = Brushes.HotPink;
                        }
                        break;
                    case 4://boyanıyor
                        if (!IsExistSmallerStatus((int)BLL.Enumarations.OrderStatus.Boyanıyor))
                        {
                            cmbOrderStatus.SelectedIndex = 3;
                            cmbOrderStatus.Foreground = Brushes.Orange;
                        }
                        break;
                    case 5://hazır
                        if (!IsExistSmallerStatus((int)BLL.Enumarations.OrderStatus.Hazir))
                        {
                            cmbOrderStatus.SelectedIndex = 4;
                            cmbOrderStatus.Foreground = Brushes.Green;
                        }
                        break;
                    case 6://Gönderildi
                        if (!IsExistSmallerStatus((int)BLL.Enumarations.OrderStatus.Gonderildi))
                        {
                            cmbOrderStatus.SelectedIndex = 5;
                            cmbOrderStatus.Foreground = Brushes.Gray;
                        }
                        break;
                    default:
                        break;
                }
            }

            this.oOrder.OrderStatus = (BLL.Enumarations.OrderStatus)Int32.Parse((cmbOrderStatus.SelectedItem as ComboBoxItem).Tag.ToString());


            this.oOrder.EstimatedTotalProductionTime = Int32.Parse(lblTotalProductionTime.Content.ToString());
            if (this.oOrder.Save() == 0)
            {
                MessageBox.Show("İşlem başarılı");

            }
            else
            {
                MessageBox.Show("Kayıt işlemi sırasında bir hata oluştu");
            }
        }
        //private void BindOrderDetailPanel(string PID)
        //{
        //    TinyPaintIcon.Visibility = Visibility.Hidden;
        //    tblPainting.Visibility = Visibility.Hidden;
        //    lblPaintedAmount.Visibility = Visibility.Hidden;
        //    lblDeliveredAmount.Visibility = Visibility.Hidden;
        //    cmbOPStatus.Visibility = Visibility.Visible;
        //    foreach (DataRow oRow in dtDetailedOrder.Rows)
        //    {
        //        if (oRow["PID"].ToString() == PID)
        //        {
        //            if (brdRightContent.Visibility == Visibility.Hidden)
        //            {
        //                brdRightContent.Visibility = Visibility.Visible;
        //                brdRightContentEmptyTemplate.Visibility = Visibility.Hidden;
        //            }
        //            this.SelectedProduct = new BLL.Product(Int32.Parse(PID), BLL.Enumarations.State.Aktif);
        //            txtCode.Text = oRow["Code"].ToString();
        //            txtOrderCount.Text = oRow["OrderCount"].ToString();
        //            txtPackageType.Text = oRow["PackageType"].ToString();
        //            cmbColor.SelectedValue = oRow["Color"];
        //            // txtSellPrice.Text = oRow["SellPrice"].ToString();
        //            //txtProductedAmount.Text = oRow["ProductedAmount"].ToString();
        //            //lblPaintedAmount.Content = "(" + oRow["PaintedAmount"].ToString() + ")";
        //            //lblDeliveredAmount.Content = oRow["DeliveredAmount"].ToString() + " adet teslim edildi";
        //            StartModeOn = true;
        //            chcStock.IsChecked = (oRow["IsUsedStock"].ToString() == "1") ? true : false;
        //            StartModeOn = false;
        //            if (chcStock.IsChecked.Value)
        //            {
        //                brdStockInfo.Visibility = Visibility.Visible;
        //                tblStockInfo.Text = oRow["UsedStockAmount"].ToString();
        //            }
        //            else
        //            {
        //                brdStockInfo.Visibility = Visibility.Hidden;
        //                tblStockInfo.Text = "0";
        //            }
        //            if (oRow["Operator"] != null)
        //            {
        //                if (Int32.Parse(oRow["Operator"].ToString()) != 0)
        //                {
        //                    BLL.Operator OP = new BLL.Operator(Int32.Parse(oRow["Operator"].ToString()), BLL.Enumarations.State.Aktif);
        //                    lblOperatorName.Content = OP.Name + " " + OP.LastName;
        //                }
        //                else
        //                {
        //                    lblOperatorName.Content = "Operatör seçimi henüz yapılmadı !";
        //                }
        //            }
        //            else
        //            {
        //                lblOperatorName.Content = "Operatör seçimi henüz yapılmadı !";
        //            }
        //            switch (Int32.Parse(oRow["OPStatus"].ToString()))
        //            {
        //                case (int)OzyaysanBusinessEngine.Enumarations.OrderStatus.İmalataHazır:
        //                    cmbOPStatus.SelectedIndex = 0;
        //                    break;
        //                case (int)OzyaysanBusinessEngine.Enumarations.OrderStatus.Imalatta:
        //                    StartModeOnForOPStatus = true;
        //                    cmbOPStatus.SelectedIndex = 1;
        //                    StartModeOnForOPStatus = false;
        //                    break;
        //                case (int)OzyaysanBusinessEngine.Enumarations.OrderStatus.Boyahanede:
        //                    cmbOPStatus.SelectedIndex = 2;
        //                    break;
        //                case (int)OzyaysanBusinessEngine.Enumarations.OrderStatus.Hazir:
        //                    cmbOPStatus.SelectedIndex = 3;
        //                    break;
        //                case (int)OzyaysanBusinessEngine.Enumarations.OrderStatus.Gonderildi:
        //                    cmbOPStatus.SelectedIndex = 4;
        //                    lblDeliveredAmount.Visibility = Visibility.Visible;
        //                    break;
        //                case (int)OzyaysanBusinessEngine.Enumarations.OrderStatus.Boyanıyor:
        //                    cmbOPStatus.Visibility = Visibility.Collapsed;
        //                    TinyPaintIcon.Visibility = Visibility.Visible;
        //                    tblPainting.Visibility = Visibility.Visible;
        //                    lblPaintedAmount.Visibility = Visibility.Visible;
        //                    break;
        //                default:
        //                    break;
        //            }
        //            if (oCurrentUser.UserType == BLL.Enumarations.UserType.Master)
        //            {
        //                lblOperatorName.Content = "";
        //                cmbOperator.SelectedValue = oRow["Operator"].ToString();
        //            }
        //        }
        //    }

        //}
        private void LoadDetail()
        {
            cmbCustomer.SelectedValue = this.oOrder.CustomerID;
            switch (this.oOrder.OrderStatus)
            {
                case OzyaysanBusinessEngine.Enumarations.OrderStatus.İmalataHazır:
                    cmbOrderStatus.SelectedIndex = 0;
                    cmbOrderStatus.Foreground = Brushes.Red;
                    break;
                case OzyaysanBusinessEngine.Enumarations.OrderStatus.Imalatta:
                    cmbOrderStatus.SelectedIndex = 1;
                    cmbOrderStatus.Foreground = Brushes.Goldenrod;
                    break;
                case OzyaysanBusinessEngine.Enumarations.OrderStatus.Boyahanede:
                    cmbOrderStatus.Foreground = Brushes.HotPink;
                    cmbOrderStatus.SelectedIndex = 2;
                    break;
                case OzyaysanBusinessEngine.Enumarations.OrderStatus.Boyanıyor:
                    cmbOrderStatus.Foreground = Brushes.Orange;
                    cmbOrderStatus.SelectedIndex = 3;
                    break;
                case OzyaysanBusinessEngine.Enumarations.OrderStatus.Hazir:
                    cmbOrderStatus.Foreground = Brushes.Green;
                    cmbOrderStatus.SelectedIndex = 4;
                    break;
                case OzyaysanBusinessEngine.Enumarations.OrderStatus.Gonderildi:
                    cmbOrderStatus.Foreground = Brushes.Gray;
                    cmbOrderStatus.SelectedIndex = 5;
                    break;
                default:
                    break;
            }
            dpDateOfDelivery.SelectedDate = oOrder.DateOfDelivery;
            cmbOrderStatus.IsEnabled = false;
            lblTotalProductionTime.Content = oOrder.EstimatedTotalProductionTime;
            totalProductionTime = oOrder.EstimatedTotalProductionTime;
            totalCost = oOrder.TotalCost;
            lblOrderNo.Content = oOrder.OrderNo;
        }
        private void LoadData()
        {
            DataTable dtCustomer = BLL.Customer.GetCustomerListForDDL();
            cmbCustomer.ItemsSource = dtCustomer.DefaultView;
            cmbCustomer.DisplayMemberPath = dtCustomer.Columns["CName"].ToString();
            cmbCustomer.SelectedValuePath = dtCustomer.Columns["CID"].ToString();
            cmbCustomer.SelectedIndex = 0;

            dtDetailedOrder = BLL.Order.GetOrderDetailListByOrderID(this.OID);
            ListBoxItem lbItem;
            foreach (DataRow oRow in dtDetailedOrder.Rows)
            {
                BLL.Product _product = new BLL.Product(Int32.Parse(oRow["PID"].ToString()), BLL.Enumarations.State.Aktif);
                _product.OrderCount = Int32.Parse(oRow["OrderCount"].ToString());
                _product.UsedStockCountColored = Int32.Parse(oRow["UsedStockCountColored"].ToString());
                _product.UsedStockCountNonColored = Int32.Parse(oRow["UsedStockCountNonColored"].ToString());
                _product.ProductionCount = Int32.Parse(oRow["ProductionCount"].ToString());
                _product.CColor = new BLL.Color(Int32.Parse(oRow["Color"].ToString()), OzyaysanBusinessEngine.Enumarations.State.Aktif);
                _product.CurrentPackageType = oRow["PackageType"].ToString();
                this.oProductList.Add(_product);
                dgOrderProduct.Items.Add(_product);
            }


            //DataTable dtColor = BLL.Color.GetColorListForDDL();
            //cmbColor.ItemsSource = dtColor.DefaultView;
            //cmbColor.DisplayMemberPath = dtColor.Columns["CName"].ToString();
            //cmbColor.SelectedValuePath = dtColor.Columns["CID"].ToString();
            //cmbColor.SelectedIndex = 0;

            //DataTable dtState = BLL.Utility.GetStateListForDDLWithoutChose();
            //cmbState.ItemsSource = dtState.DefaultView;
            //cmbState.DisplayMemberPath = dtState.Columns["SName"].ToString();
            //cmbState.SelectedValuePath = dtState.Columns["SID"].ToString();
            //cmbState.SelectedIndex = 1;



        }

        private void CalculateTotalProductionTime(BLL.Product P)
        {
            DataTable dt = BLL.Product.CalculateProductionTimeByPIDAndCount(P.ID, P.ProductionCount);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["ProductionTime"] != null && dt.Rows[0]["ProductionTime"].ToString() != "")
                {
                    totalProductionTime += Int32.Parse(dt.Rows[0]["ProductionTime"].ToString()) / 420;
                    string DayValue = (totalProductionTime).ToString();
                    lblTotalProductionTime.Content = DayValue;
                }

            }

        }
        #endregion

        #region ClickEventMethots
        private void Click_btnUseStock()
        {

            // Kontrol edilecek
            //if (this.SelectedProduct.Stock > 0)
            //{
            //    var window = new W_UseStock(this.SelectedProduct.ID, Int32.Parse(txtOrderCount.Text), this.oOrder.ID, true)
            //    {
            //        ShowInTaskbar = false,
            //        Topmost = true,
            //        ResizeMode = ResizeMode.CanMinimize,
            //        Owner = Application.Current.MainWindow,
            //    };
            //    if (window.ShowDialog() == false)
            //    {

            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Stokta bu yaydan bulunmamaktadır.");
            //}

        }
        private void Click_btnUpdate()
        {
            string strVal = ValidationMethot();
            using (TransactionScope scope1 = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    if (strVal == "")
                    {
                        this.oOrder.CustomerID = Int32.Parse(cmbCustomer.SelectedValue.ToString());
                        this.oOrder.EstimatedTotalProductionTime = Int32.Parse(lblTotalProductionTime.Content.ToString());
                        //if (txtTutar.Text != "")
                        //{
                        //    this.oOrder.TotalCost = decimal.Parse(txtTutar.Text);
                        //}

                        this.oOrder.DateOfDelivery = dpDateOfDelivery.SelectedDate.Value;
                        this.oOrder.OrderStatus = (BLL.Enumarations.OrderStatus)Int32.Parse((cmbOrderStatus.SelectedItem as ComboBoxItem).Tag.ToString());
                        this.oOrder.Products = "";
                        foreach (BLL.Product item in this.oProductList)
                        {
                            this.oOrder.Products += "," + item.ID.ToString() + "";
                        }
                        if (oProductList.Count > 0)
                        {
                            this.oOrder.Products += ",";
                        }
                        this.oOrder.State = BLL.Enumarations.State.Aktif;
                        int firstResult = oOrder.Save();
                        if (firstResult == 0)
                        {
                            if (oProductList.Count == 0)
                            {
                                MessageBox.Show("Siparişte en az 1 adet yay olmalıdır.");
                                return;
                            }
                            BLL.Order.DeleteOrderDetail(this.OID);
                            foreach (BLL.Product item in dgOrderProduct.Items)
                            {
                                int secondResult = BLL.Order.SaveOrderDetail(this.OID, item.ID, item.OrderCount, item.UsedStockCountColored, item.UsedStockCountNonColored, item.ProductionCount, item.CColor.ID, item.CurrentPackageType, item.CurrentOPStatus);
                                if (secondResult == 0)
                                {
                                    // MessageBox.Show("İşlem başarılı");
                                    this.oOrder.OrderStatus = BLL.Enumarations.OrderStatus.İmalataHazır;
                                    cmbOrderStatus.SelectedIndex = 0;
                                    cmbOrderStatus.Foreground = Brushes.Red;
                                    this.oOrder.Save();
                                }
                            }
                            MessageBox.Show("İşlem başarılı");
                            (((Start)((Grid)(((TabControl)(((TabItem)((Grid)((Grid)(this.Parent)).Parent).Parent).Parent)).Parent)).Parent)).GrdContentSearchOrder.Visibility = Visibility.Hidden;
                            (((Start)((Grid)(((TabControl)(((TabItem)((Grid)((Grid)(this.Parent)).Parent).Parent).Parent)).Parent)).Parent)).GrdContentAddOrder.Visibility = Visibility.Hidden;
                            (((Start)((Grid)(((TabControl)(((TabItem)((Grid)((Grid)(this.Parent)).Parent).Parent).Parent)).Parent)).Parent)).GrdContentOrderDetail.Visibility = Visibility.Visible;
                            Ozyaysan.UserControls.UCOrderDetail oWindowsOrderDetail = new UserControls.UCOrderDetail(oOrder.ID);
                            (((Start)((Grid)(((TabControl)(((TabItem)((Grid)((Grid)(this.Parent)).Parent).Parent).Parent)).Parent)).Parent)).GrdContentOrderDetail.Children.Add(oWindowsOrderDetail);
                            (((Start)((Grid)(((TabControl)(((TabItem)((Grid)((Grid)(this.Parent)).Parent).Parent).Parent)).Parent)).Parent)).GrdContentOrderDetail.Children.RemoveAt(0);
                        }
                        scope1.Complete();
                    }
                    else
                    {
                        MessageBox.Show(strVal);
                    }
                }
                catch (System.Data.SqlClient.SqlException Exp)
                {
                    MessageBox.Show("Sql Hatası oluştu : " + Exp.Message);
                }
                catch (Exception Exp)
                {
                    MessageBox.Show("Hata oluştu : " + Exp.Message);
                }
            }
        }

        private void Click_btnProductUpdate()
        {


            //ListBoxItem listItem = this.lstSelectedProducts.ItemContainerGenerator.ContainerFromIndex(this.lstSelectedProducts.SelectedIndex) as ListBoxItem;
            //int firstCount = 0;
            //int secondCount = Int32.Parse(txtOrderCount.Text);
            //int OperatorID = 0;
            //decimal SellPrice = 0;
            //foreach (DataRow oRow in dtDetailedOrder.Rows)
            //{
            //    if (oRow["PID"].ToString() == listItem.Uid)
            //    {
            //        OperatorID = Int32.Parse(oRow["Operator"].ToString());
            //        firstCount = Int32.Parse(oRow["Count"].ToString());
            //       // SellPrice = decimal.Parse(oRow["SellPrice"].ToString());
            //    }
            //}
            //int OPStatus = 0;
            //if (TinyPaintIcon.Visibility==Visibility.Visible)
            //{
            //    OPStatus = ((int)BLL.Enumarations.OrderStatus.Boyanıyor);
            //}
            //else
            //{
            //    OPStatus = Int32.Parse((cmbOPStatus.SelectedItem as ComboBoxItem).Tag.ToString());
            //}
            //int Result = -1;
            //if (oCurrentUser.UserType ==BLL.Enumarations.UserType.Owner)
            //{
            //    Result = BLL.Order.UpdateOrderDetail(this.OID, SelectedProduct.ID, Int32.Parse(txtOrderCount.Text), Int32.Parse(txtOrderCount.Text), Int32.Parse(txtOrderCount.Text), Int32.Parse(txtOrderCount.Text), Int32.Parse(cmbColor.SelectedValue.ToString()), txtPackageType.Text,  OPStatus, OperatorID,  Int32.Parse(txtProductedAmount.Text));
            //}
            //else if (oCurrentUser.UserType == BLL.Enumarations.UserType.Master)
            //{
            //    Result = BLL.Order.UpdateOrderDetail(this.OID, SelectedProduct.ID, Int32.Parse(txtOrderCount.Text), Int32.Parse(txtOrderCount.Text), Int32.Parse(txtOrderCount.Text), Int32.Parse(txtOrderCount.Text), Int32.Parse(cmbColor.SelectedValue.ToString()), txtPackageType.Text,  OPStatus, Int32.Parse(cmbOperator.SelectedValue.ToString()), Int32.Parse(txtProductedAmount.Text));
            //}
            //else
            //{
            //    Result = BLL.Order.UpdateOrderDetail(this.OID, SelectedProduct.ID, Int32.Parse(txtOrderCount.Text), Int32.Parse(txtOrderCount.Text), Int32.Parse(txtOrderCount.Text), Int32.Parse(txtOrderCount.Text), Int32.Parse(cmbColor.SelectedValue.ToString()), txtPackageType.Text, OPStatus, OperatorID,  Int32.Parse(txtProductedAmount.Text));
            //}
            //if (Result == 0)
            //{
            //    dtDetailedOrder = BLL.Order.GetOrderDetailListByOrderID(this.OID);
            //    CalculateTotalProductionTimeSelectedProduct(secondCount - firstCount);
            //    UpdateGeneralOrderStatus(OPStatus);//hem statusu hemde total süre ve tutarı günceller
            //}
            //else
            //{
            //    MessageBox.Show("Kayıt işlemi sırasında bir hata oluştu");
            //}
        }

        private void Click_btnExamine()
        {
            var window = new W_ExamineProduct(SelectedProduct.ID)
            {
                Title = "Yay İnceleme Formu",
                ShowInTaskbar = false,
                Topmost = true,
                ResizeMode = ResizeMode.CanMinimize,
                Owner = Application.Current.MainWindow

            };
            if (window.ShowDialog() == false)
            {

            }

        }

        private void Click_btnRawMaterials()
        {
            var window = new W_UseRawMaterials(this.oOrder.ID, this.SelectedProduct.ID)
            {
                Title = "Hammadde Kullan",
                ShowInTaskbar = false,
                Topmost = true,
                ResizeMode = ResizeMode.CanMinimize,
                Owner = Application.Current.MainWindow,
            };
            if (window.ShowDialog() == false)
            {


            }
        }

        private void btnExamineProduct(object sender, RoutedEventArgs e)
        {
          
            var window = new W_AddProductForOrder(dgOrderProduct.SelectedValue as BLL.Product)
            {
                Title = "Siparise Yay Ekle",
                ShowInTaskbar = false,
                Topmost = true,
                ResizeMode = ResizeMode.CanMinimize,
                Owner = Application.Current.MainWindow,
            };
            if (window.ShowDialog() == false)
            {
                SelectedProduct = window.AddedProduct;
                if (SelectedProduct != null)
                {
                    dgOrderProduct.Items.Remove(dgOrderProduct.SelectedItem);                    
                    dgOrderProduct.Items.Add(SelectedProduct);
                    CalculateTotalProductionTime(SelectedProduct);
                }
            }


        }
        #endregion

        #region LoaidngPanelRelatedPart

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets a value indicating whether [panel loading].
        /// </summary>
        /// <value>
        /// <c>true</c> if [panel loading]; otherwise, <c>false</c>.
        /// </value>
        public bool PanelLoading
        {
            get
            {
                return _panelLoading;
            }
            set
            {
                _panelLoading = value;
                RaisePropertyChanged("PanelLoading");
            }
        }

        /// <summary>
        /// Gets or sets the panel main message.
        /// </summary>
        /// <value>The panel main message.</value>
        public string PanelMainMessage
        {
            get
            {
                return _panelMainMessage;
            }
            set
            {
                _panelMainMessage = value;
                RaisePropertyChanged("PanelMainMessage");
            }
        }

        /// <summary>
        /// Gets or sets the panel sub message.
        /// </summary>
        /// <value>The panel sub message.</value>
        public string PanelSubMessage
        {
            get
            {
                return _panelSubMessage;
            }
            set
            {
                _panelSubMessage = value;
                RaisePropertyChanged("PanelSubMessage");
            }
        }

        /// <summary>
        /// Gets the panel close command.
        /// </summary>
        public ICommand PanelCloseCommand
        {
            get
            {
                return new BLL.DelegateCommand(() =>
                {
                    // Your code here.
                    // You may want to terminate the running thread etc.
                    PanelLoading = false;
                });
            }
        }

        /// <summary>
        /// Gets the show panel command.
        /// </summary>
        public ICommand ShowPanelCommand
        {
            get
            {
                return new BLL.DelegateCommand(() =>
                {
                    PanelLoading = true;
                });
            }
        }

        /// <summary>
        /// Gets the hide panel command.
        /// </summary>
        public ICommand HidePanelCommand
        {
            get
            {
                return new BLL.DelegateCommand(() =>
                {
                    PanelLoading = false;
                });
            }
        }

        /// <summary>
        /// Gets the change sub message command.
        /// </summary>
        public ICommand ChangeSubMessageCommand
        {
            get
            {
                return new BLL.DelegateCommand(() =>
                {
                    PanelSubMessage = string.Format("Message: {0}", DateTime.Now);
                });
            }
        }

        /// <summary>
        /// Raises the property changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

    }
}
