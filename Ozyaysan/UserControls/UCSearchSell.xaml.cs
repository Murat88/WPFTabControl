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
using System.Data;
using BLL = OzyaysanBusinessEngine;
using Ozyaysan.Windows;
using Microsoft.Windows.Controls;
using Microsoft.Windows.Controls.Primitives;

namespace Ozyaysan.UserControls
{
    /// <summary>
    /// Interaction logic for UCSearchSell.xaml
    /// </summary>
    public partial class UCSearchSell : UserControl
    {
        
        private int totalRecordCount = 0;
        private int pageSize = 8;


        #region Construtors
        public UCSearchSell()
        {
            InitializeComponent();
            LoadData();
            dgSoldProduct.ItemsSource = BLL.Product.getSoldProductList(CreateWhereString(), CreateHavingString(), 0, pageSize, "", 0).Tables[1].DefaultView;
            this.totalRecordCount = BLL.Product.getSoldProductList(CreateWhereString(), CreateHavingString(), 0, int.MaxValue - 1, "", 0).Tables[1].Rows.Count;
            btn1Previous.IsEnabled = false;
            btnAlPrevious.IsEnabled = false;
            lblRecordCount.Content = this.totalRecordCount.ToString();

            if (BLL.Product.getSoldProductList(CreateWhereString(), CreateHavingString(), 0, int.MaxValue - 1, "", 0).Tables[1].Rows.Count < pageSize)
            {
                btn1Forward.IsEnabled = false;
                btnAllForward.IsEnabled = false;
            }
        }
        #endregion    
        
        #region Paging Related Part
        private void btn1Forward_Click(object sender, RoutedEventArgs e)
        {
            int page = Int32.Parse(txtPage.Text);
            txtPage.Text = (page + 1).ToString();
            dgSoldProduct.ItemsSource = BLL.Product.getSoldProductList(CreateWhereString(),CreateHavingString(), Int32.Parse(txtPage.Text), pageSize, "", 0).Tables[1].DefaultView;
            int lastPage = (totalRecordCount / pageSize);
            if (Int32.Parse(txtPage.Text) == lastPage)
            {
                btnAllForward.IsEnabled = false;
                btn1Forward.IsEnabled = false;
            }
            btnAlPrevious.IsEnabled = true;
            btn1Previous.IsEnabled = true;
            dgSoldProduct.UpdateLayout();
            UpdateGridLayout();
        }

        private void btn1Previous_Click(object sender, RoutedEventArgs e)
        {
            int page = Int32.Parse(txtPage.Text);
            txtPage.Text = (page - 1).ToString();
            dgSoldProduct.ItemsSource = BLL.Product.getSoldProductList(CreateWhereString(), CreateHavingString(), Int32.Parse(txtPage.Text), pageSize, "", 0).Tables[1].DefaultView;
            if (Int32.Parse(txtPage.Text) == 0)
            {
                btnAlPrevious.IsEnabled = false;
                btn1Previous.IsEnabled = false;

            }
            btn1Forward.IsEnabled = true;
            btnAllForward.IsEnabled = true;
            dgSoldProduct.UpdateLayout();
            UpdateGridLayout();
        }

        private void btnAlPrevious_Click(object sender, RoutedEventArgs e)
        {
            txtPage.Text = "0";
            dgSoldProduct.ItemsSource = BLL.Product.getSoldProductList(CreateWhereString(), CreateHavingString(), 0, pageSize, "", 0).Tables[1].DefaultView;
            btnAlPrevious.IsEnabled = false;
            btn1Previous.IsEnabled = false;
            btn1Forward.IsEnabled = true;
            btnAllForward.IsEnabled = true;
            dgSoldProduct.UpdateLayout();
            UpdateGridLayout();
        }

        private void btnAllForward_Click(object sender, RoutedEventArgs e)
        {
            if (totalRecordCount > pageSize)
            {
                txtPage.Text = Convert.ToInt32((totalRecordCount / pageSize)).ToString();
                dgSoldProduct.ItemsSource = BLL.Product.getSoldProductList(CreateWhereString(), CreateHavingString(), Int32.Parse(txtPage.Text), pageSize, "", 0).Tables[1].DefaultView;
                btnAllForward.IsEnabled = false;
                btn1Forward.IsEnabled = false;
                btnAlPrevious.IsEnabled = true;
                btn1Previous.IsEnabled = true;
                dgSoldProduct.UpdateLayout();
                UpdateGridLayout();
            }
        }

        #endregion
               
        #region Events

        #region KeyDown
        private void txtTotalPrice_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            AllowOnlyNumericAndDecimal(e);
        }
        private void txtCount_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            AllowOnlyNumeric(e);
        }
        #endregion

        #region Click
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            dgSoldProduct.ItemsSource = BLL.Product.getSoldProductList(CreateWhereString(), CreateHavingString(), 0, pageSize, "", 0).Tables[1].DefaultView;
            this.totalRecordCount = BLL.Product.getSoldProductList(CreateWhereString(), CreateHavingString(), 0, int.MaxValue - 1, "", 0).Tables[1].Rows.Count;
            this.totalRecordCount = BLL.Product.getSoldProductList(CreateWhereString(), CreateHavingString(), 0, int.MaxValue - 1, "", 0).Tables[1].Rows.Count;
            lblRecordCount.Content = this.totalRecordCount.ToString();
            dgSoldProduct.UpdateLayout();
            UpdateGridLayout();
        }
         private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).CommandParameter != null)
            {
                if ((sender as Button).Uid != null)
                {
                    if ((sender as Button).ToolTip != null)
                    {
                        var window = new W_ChangePaintStatus(Int32.Parse((sender as Button).CommandParameter.ToString()),Int32.Parse((sender as Button).Uid),Int32.Parse((sender as Button).ToolTip.ToString()))
                        {                          
                            ShowInTaskbar = false,
                            Topmost = true,
                            ResizeMode = ResizeMode.CanMinimize,
                            Owner = Application.Current.MainWindow
                        };
                        if (window.ShowDialog() == false)
                        {
                            ClearAllControls();
                            dgSoldProduct.ItemsSource = BLL.Product.getSoldProductList(CreateWhereString(), CreateHavingString(), 0, pageSize, "", 0).Tables[1].DefaultView;
                            this.totalRecordCount = BLL.Product.getSoldProductList(CreateWhereString(), CreateHavingString(), 0, int.MaxValue - 1, "", 0).Tables[1].Rows.Count;
                            lblRecordCount.Content = this.totalRecordCount.ToString();
                            dgSoldProduct.UpdateLayout();
                            UpdateGridLayout();
                        }
                    }
                }            
            }
        }
        #endregion

        #region MouseDown
         private void imgProductSearch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var window = new W_PickProductForOrder()
                {
                    Title = "Yay Seçme Formu",
                    ShowInTaskbar = false,
                    Topmost = true,
                    ResizeMode = ResizeMode.NoResize,
                    Owner = Application.Current.MainWindow,
                };
                if (window.ShowDialog() == false)
                {
                    BLL.Product PickedProduct = window.SelectedProduct;
                    cmbProduct.SelectedValue = PickedProduct.ID;
                }
            }
        }
        #endregion

        #region Loaded
         private void dgSoldProduct_Loaded(object sender, RoutedEventArgs e)
         {
             UpdateGridLayout();
         }
        #endregion
        #endregion

        #region Custom Methots
         private void UpdateGridLayout()
         {
             ////
             //DataGridCell cell;
             //for (int i = 0; i < Int32.Parse(lblRecordCount.Content.ToString()); i++)
             //{
             //    DataGridRow row = (DataGridRow)dgSoldProduct.ItemContainerGenerator.ContainerFromIndex(i);
             //    if (row != null)
             //    {
             //        DataRowView item = row.Item as DataRowView;
             //        if (item != null)
             //        {
             //            switch (Int32.Parse(item["OPStatus"].ToString()))
             //            {
             //                case 1:
             //                    cell = GetCell(row, 5);
             //                    cell.Background = Brushes.Red;                             
             //                    break;
             //                case 2:
             //                    cell = GetCell(row, 5);
             //                    cell.Background = Brushes.Yellow;
             //                    break;
             //                case 3:
             //                    cell = GetCell(row, 5);
             //                    cell.Background = Brushes.HotPink;
             //                    break;
             //                case 4:
             //                    cell = GetCell(row, 5);                            
             //                    cell.Background = Brushes.Orange;
             //                    //button_cell = GetCell(row, 0);
             //                    //var cp = button_cell.Content as ContentPresenter;
             //                    //var button = cp.ContentTemplate.FindName("btnStart", cp) as Button;
             //                    //button.IsEnabled = false;
             //                    break;
             //                case 5:
             //                    cell = GetCell(row, 5);
             //                    cell.Background = Brushes.Green;                              
             //                    break;
             //                case 6:
             //                    cell = GetCell(row, 5);
             //                    cell.Background = Brushes.Gray;
             //                    break;
             //                default:
             //                    break;
             //            }
             //        }
             //    }
             //}
         }
         private void ClearAllControls()
         {
             cmbProduct.SelectedIndex = 0;
         }
         private string CreateWhereString()
         {
             string strWhere = "";
             if (cmbProduct.SelectedValue.ToString() != "0")
             {
                 strWhere += " and LU.PID=" + cmbProduct.SelectedValue.ToString() + "";
             }



             return strWhere;
         }
         private string CreateHavingString()
         {
             string strHaving = "";
             if (txtCountFirst.Text != "" && txtCountSecond.Text == "")
             {
                 strHaving += " HAVING Sum([DeliveredAmount])>=" + Int32.Parse(txtCountFirst.Text) + " ";
             }
             else if (txtCountFirst.Text == "" && txtCountSecond.Text != "")
             {
                 strHaving += " HAVING Sum([DeliveredAmount])<=" + Int32.Parse(txtCountSecond.Text) + " ";
             }
             else if (txtCountFirst.Text != "" && txtCountSecond.Text != "")
             {
                 strHaving += " HAVING Sum([DeliveredAmount]) between " + Int32.Parse(txtCountFirst.Text) + " and " + Int32.Parse(txtCountSecond.Text) + " ";
             }
             if (strHaving == "")
             {
                 if (txtTotalPriceFirst.Text != "" && txtTotalPriceSecond.Text == "")
                 {
                     strHaving += " HAVING Sum([SellPrice])>=" + decimal.Parse(txtTotalPriceFirst.Text) + " ";
                 }
                 else if (txtTotalPriceFirst.Text == "" && txtTotalPriceSecond.Text != "")
                 {
                     strHaving += " HAVING Sum([SellPrice])<=" + decimal.Parse(txtTotalPriceSecond.Text) + " ";
                 }
                 else if (txtTotalPriceFirst.Text != "" && txtTotalPriceSecond.Text != "")
                 {
                     strHaving += " HAVING Sum([SellPrice]) between " + decimal.Parse(txtTotalPriceFirst.Text) + " and " + decimal.Parse(txtTotalPriceSecond.Text) + " ";
                 }
             }
             else
             {
                 if (txtTotalPriceFirst.Text != "" && txtTotalPriceSecond.Text == "")
                 {
                     strHaving += " and Sum([SellPrice])>=" + decimal.Parse(txtTotalPriceFirst.Text) + " ";
                 }
                 else if (txtTotalPriceFirst.Text == "" && txtTotalPriceSecond.Text != "")
                 {
                     strHaving += " and Sum([SellPrice])<=" + decimal.Parse(txtTotalPriceSecond.Text) + " ";
                 }
                 else if (txtTotalPriceFirst.Text != "" && txtTotalPriceSecond.Text != "")
                 {
                     strHaving += " and Sum([SellPrice]) between " + decimal.Parse(txtTotalPriceFirst.Text) + " and " + decimal.Parse(txtTotalPriceSecond.Text) + " ";
                 }
             }

             return strHaving;
         }
         private void LoadData()
         {

             DataTable dtProduct = BLL.Product.GetProductListForDDL();
             cmbProduct.ItemsSource = dtProduct.DefaultView;
             cmbProduct.DisplayMemberPath = dtProduct.Columns["PName"].ToString();
             cmbProduct.SelectedValuePath = dtProduct.Columns["PID"].ToString();
             cmbProduct.SelectedIndex = 0;
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
         public Microsoft.Windows.Controls.DataGridCell GetCell(Microsoft.Windows.Controls.DataGridRow rowContainer, int column)
         {
             return HelperPaint.GetCell(dgSoldProduct, rowContainer, column);

         }
         #endregion 

    }
    public static class HelperSold
    {
        public static Microsoft.Windows.Controls.DataGridCell GetCell(this Microsoft.Windows.Controls.DataGrid grid, Microsoft.Windows.Controls.DataGridRow row, int column)
        {
            if (row != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(row);

                if (presenter == null)
                {
                    grid.ScrollIntoView(row, grid.Columns[column]);
                    presenter = GetVisualChild<DataGridCellsPresenter>(row);
                }

                Microsoft.Windows.Controls.DataGridCell cell = (Microsoft.Windows.Controls.DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                return cell;
            }
            return null;
        }
        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }
    }
}
