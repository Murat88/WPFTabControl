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
using System.Transactions;

namespace Ozyaysan.UserControls
{
    /// <summary>
    /// Interaction logic for UCAddOrder.xaml
    /// </summary>
    public partial class UCAddOrder : UserControl
    {

        #region Fields
        int totalProductionTime = 0;
        BLL.Product AddedProduct;
        #endregion

        #region Contructors
        public UCAddOrder()
        {
            InitializeComponent();
            LoadData();
        }
        #endregion

        #region Events

        #region Click
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Click_btnSave();
        }


        #endregion

        #region MouseDown
        private void lblAddProduct_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var window = new W_AddProductForOrder()
                {
                    Title = "Siparise Yay Ekle",
                    ShowInTaskbar = false,
                    Topmost = true,
                    ResizeMode = ResizeMode.CanMinimize,
                    Owner = Application.Current.MainWindow,
                };
                if (window.ShowDialog() == false)
                {
                    AddedProduct = window.AddedProduct;
                    if (AddedProduct != null)
                    {
                        if (IfExistsSameProductOrNot(AddedProduct))
                        {
                            MessageBox.Show("Seçmiş olduğunuz yay zaten siparişte mevcut!!");
                        }
                        else
                        {                           
                            CalculateTotalProductionTime(AddedProduct);
                            dgOrderProduct.Items.Add(AddedProduct);
                        }

                    }

                }
            }
        }
        private void lblDeleteProduct_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                dgOrderProduct.Items.Remove(dgOrderProduct.SelectedItem);
            }
        }
        private void tblOrderSave_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MouseButtonState.Pressed == e.LeftButton)
            {
                Click_btnSave();
            }
        }
        #endregion

        #region PreviewKeyDown
        private void txtTutar_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            AllowOnlyNumericAndDecimal(e);
        }
        #endregion
        #endregion

        #region Custom Methots
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
        bool IfExistsSameProductOrNot(BLL.Product SelectedProduct)
        {
            foreach (BLL.Product item in dgOrderProduct.Items)
            {
                if (SelectedProduct.ID == item.ID)
                {
                    return true;
                }
            }
            return false;
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
                    lblProductionTime.Content = DayValue;
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
        private void LoadData()
        {
            DataTable dtCustomer = BLL.Customer.GetCustomerListForDDL();
            cmbCustomer.ItemsSource = dtCustomer.DefaultView;
            cmbCustomer.DisplayMemberPath = dtCustomer.Columns["CName"].ToString();
            cmbCustomer.SelectedValuePath = dtCustomer.Columns["CID"].ToString();
            cmbCustomer.SelectedIndex = 0;

            //DataTable dtState = BLL.Utility.GetStateListForDDLWithoutChose();
            //cmbState.ItemsSource = dtState.DefaultView;
            //cmbState.DisplayMemberPath = dtState.Columns["SName"].ToString();
            //cmbState.SelectedValuePath = dtState.Columns["SID"].ToString();
            //cmbState.SelectedIndex = 1;
        }
        #endregion       

        #region ClickEventMethots
        private void Click_btnSave()
        {

            using (TransactionScope scope1 = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    string strVal = ValidationMethot();
                    if (strVal == "")
                    {

                        BLL.Order oOrder = new BLL.Order();
                        oOrder.CustomerID = Int32.Parse(cmbCustomer.SelectedValue.ToString());
                        oOrder.DateOfDelivery = dpDateOfDelivery.SelectedDate.Value;
                        string a = String.Format("{0:d/M/yyyy HH:mm:ss}", oOrder.DateOfDelivery);
                        oOrder.DateOfDelivery2 = a;
                        oOrder.MinState = BLL.Enumarations.State.Aktif;
                        oOrder.OrderStatus = BLL.Enumarations.OrderStatus.İmalataHazır;
                        oOrder.State = BLL.Enumarations.State.Aktif;
                        oOrder.Products = "";
                        foreach (BLL.Product item in dgOrderProduct.Items)
                        {
                            oOrder.Products += "," + item.ID.ToString() + "";
                        }
                        oOrder.Products += ",";
                        oOrder.EstimatedTotalProductionTime = Int32.Parse(lblProductionTime.Content.ToString());
                        //if (txtTutar.Text!="")
                        //{
                        //    oOrder.TotalCost = decimal.Parse(txtTutar.Text);
                        //}

                        int firstResult = -1;
                        int secondResult = -1;
                        firstResult = oOrder.Save();
                        if (firstResult == 0)
                        {
                            foreach (BLL.Product item in dgOrderProduct.Items)
                            {
                                secondResult = BLL.Order.SaveOrderDetail(oOrder.ID, item.ID, item.OrderCount, item.UsedStockCountColored, item.UsedStockCountNonColored, item.ProductionCount, item.CColor.ID, item.CurrentPackageType, item.CurrentOPStatus);
                                if (secondResult == 0)
                                {                                    
                                    (((Start)((Grid)(((TabControl)(((TabItem)((Grid)((Grid)(this.Parent)).Parent).Parent).Parent)).Parent)).Parent)).GrdContentSearchOrder.Visibility = Visibility.Hidden;
                                    (((Start)((Grid)(((TabControl)(((TabItem)((Grid)((Grid)(this.Parent)).Parent).Parent).Parent)).Parent)).Parent)).GrdContentAddOrder.Visibility = Visibility.Hidden;
                                    (((Start)((Grid)(((TabControl)(((TabItem)((Grid)((Grid)(this.Parent)).Parent).Parent).Parent)).Parent)).Parent)).GrdContentOrderDetail.Visibility = Visibility.Visible;
                                    Ozyaysan.UserControls.UCOrderDetail oWindowsOrderDetail = new UserControls.UCOrderDetail(oOrder.ID);
                                    (((Start)((Grid)(((TabControl)(((TabItem)((Grid)((Grid)(this.Parent)).Parent).Parent).Parent)).Parent)).Parent)).GrdContentOrderDetail.Children.Clear();
                                    (((Start)((Grid)(((TabControl)(((TabItem)((Grid)((Grid)(this.Parent)).Parent).Parent).Parent)).Parent)).Parent)).GrdContentOrderDetail.Children.Add(oWindowsOrderDetail);
                                }
                            }
                        }
                        MessageBox.Show("İşlem Başarili");
                    }
                    else
                    {
                        MessageBox.Show(strVal);
                    }
                    scope1.Complete();
                 
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
            #endregion
        }
    }
}
