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
    /// Interaction logic for UCSearchOrder.xaml
    /// </summary>
    public partial class UCSearchOrder : UserControl
    {
        private int totalRecordCount = 0;
        private int pageSize = 8;
        public UCSearchOrder()
        {
            InitializeComponent();
            LoadData();
            dgOrders.ItemsSource = BLL.Order.getOrderList(CreateWhereString(), 0, pageSize, "", 0).Tables[1].DefaultView;
            this.totalRecordCount = BLL.Order.getOrderList(CreateWhereString(), 0, int.MaxValue - 1, "", 0).Tables[1].Rows.Count;
            btn1Previous.IsEnabled = false;
            btnAlPrevious.IsEnabled = false;
            lblRecordCount.Content = this.totalRecordCount.ToString();

            if (BLL.Order.getOrderList(CreateWhereString(), 0, int.MaxValue - 1, "", 0).Tables[1].Rows.Count < pageSize)
            {
                btn1Forward.IsEnabled = false;
                btnAllForward.IsEnabled = false;
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            DataSet ds = BLL.Order.getOrderList(CreateWhereString(), 0, pageSize, "", 1);
            dgOrders.ItemsSource = ds.Tables[1].DefaultView;
            lblRecordCount.Content = BLL.Order.getOrderList(CreateWhereString(), 0, Int32.MaxValue - 1, "", 0).Tables[1].Rows.Count;
            dgOrders.UpdateLayout();
            UpdateGridLayout();
        }

        private string CreateWhereString()
        {
            string strWhere = "";
            if (txtOrderNo.Text != "")
            {
                strWhere += "and LU.OrderNo like '%" + txtOrderNo.Text + "%' ";
            }
            if (cmbCustomer.SelectedIndex != 0)
            {
                strWhere += "and LU.Customer=" + Int32.Parse(cmbCustomer.SelectedValue.ToString()) + " ";
            }
            if (cmbProduct.SelectedIndex != 0)
            {
                strWhere += "and LU.Products like ',%" + Int32.Parse(cmbProduct.SelectedValue.ToString()) + "%,' ";
            }
            if (cmbOrderStatus.SelectedIndex != 0)
            {
                strWhere += "and LU.OrderStatus=" + Int32.Parse((cmbOrderStatus.SelectedItem as ComboBoxItem).Tag.ToString()) + "";
            }
            if (dpDateOfDeliveryFirst.SelectedDate != null)
            {
                DateTime dtFirst = dpDateOfDeliveryFirst.SelectedDate.Value;
                if (dpDateOfDeliverySecond.SelectedDate != null)
                {
                    DateTime dtSecond = dpDateOfDeliverySecond.SelectedDate.Value;
                    strWhere += "and convert(datetime, '" + dtSecond + "', 103)>convert(datetime, LU.DateOfDelivery, 103) AND convert(datetime, LU.DateOfDelivery, 103)>convert(datetime, '" + dtFirst + "', 103)";
                }
                else
                {
                    strWhere += "and convert(datetime, LU.DateOfDelivery, 103)>convert(datetime, '" + dtFirst + "', 103)";

                }

            }
            else
            {
                if (dpDateOfDeliverySecond.SelectedDate != null)
                {
                    DateTime dtSecond = dpDateOfDeliverySecond.SelectedDate.Value;
                    strWhere += "and convert(datetime, '" + dtSecond + "', 103)>convert(datetime, LU.DateOfDelivery, 103)";
                }

            }

            //if (dpDateOfDeliveryFirst.SelectedDate.ToString()!="" && dpDateOfDeliverySecond.SelectedDate.ToString()=="")
            //{
            //    strWhere += "and LU.DateOfDelivery > " + dpDateOfDeliveryFirst.SelectedDate.Value+ "";
            //}
            this.totalRecordCount = BLL.Order.getOrderList(strWhere, 0, int.MaxValue - 1, "", 1).Tables[1].Rows.Count;

            return strWhere;
        }
        private void LoadData()
        {
            DataTable dtCustomer = BLL.Customer.GetCustomerListForDDL();
            cmbCustomer.ItemsSource = dtCustomer.DefaultView;
            cmbCustomer.DisplayMemberPath = dtCustomer.Columns["CName"].ToString();
            cmbCustomer.SelectedValuePath = dtCustomer.Columns["CID"].ToString();
            cmbCustomer.SelectedIndex = 0;



            DataTable dtProduct = BLL.Product.GetProductListForDDL();
            cmbProduct.ItemsSource = dtProduct.DefaultView;
            cmbProduct.DisplayMemberPath = dtProduct.Columns["PName"].ToString();
            cmbProduct.SelectedValuePath = dtProduct.Columns["PID"].ToString();
            cmbProduct.SelectedIndex = 0;



            cmbOrderStatus.SelectedIndex = 0;
        }

        private void imgProductSearch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var window = new W_PickProductForOrder()
                {
                    Title = "Siparise Urun Ekle",
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).CommandParameter != null)
            {
                BLL.User oCurrentUser = (ApplicationState.GetValue<object>("user") as BLL.User);
            if (oCurrentUser.UserType==BLL.Enumarations.UserType.Master)
            {                
                    (((MasterStart)((Grid)(((TabControl)(((TabItem)((Grid)((Grid)(this.Parent)).Parent).Parent).Parent)).Parent)).Parent)).GrdContentSearchOrder.Visibility = Visibility.Hidden;
                    (((MasterStart)((Grid)(((TabControl)(((TabItem)((Grid)((Grid)(this.Parent)).Parent).Parent).Parent)).Parent)).Parent)).GrdContentAddOrder.Visibility = Visibility.Hidden;
                    (((MasterStart)((Grid)(((TabControl)(((TabItem)((Grid)((Grid)(this.Parent)).Parent).Parent).Parent)).Parent)).Parent)).GrdContentOrderDetail.Visibility = Visibility.Visible;
                    Ozyaysan.UserControls.UCOrderDetail oWindowsProductDetail = new UserControls.UCOrderDetail(Int32.Parse((sender as Button).CommandParameter.ToString()));
                    (((MasterStart)((Grid)(((TabControl)(((TabItem)((Grid)((Grid)(this.Parent)).Parent).Parent).Parent)).Parent)).Parent)).GrdContentOrderDetail.Children.Clear();
                    (((MasterStart)((Grid)(((TabControl)(((TabItem)((Grid)((Grid)(this.Parent)).Parent).Parent).Parent)).Parent)).Parent)).GrdContentOrderDetail.Children.Add(oWindowsProductDetail);


            }
            else if (oCurrentUser.UserType == BLL.Enumarations.UserType.Owner)
                {
                    (((Start)((Grid)(((TabControl)(((TabItem)((Grid)((Grid)(this.Parent)).Parent).Parent).Parent)).Parent)).Parent)).GrdContentSearchOrder.Visibility = Visibility.Hidden;
                    (((Start)((Grid)(((TabControl)(((TabItem)((Grid)((Grid)(this.Parent)).Parent).Parent).Parent)).Parent)).Parent)).GrdContentAddOrder.Visibility = Visibility.Hidden;
                    (((Start)((Grid)(((TabControl)(((TabItem)((Grid)((Grid)(this.Parent)).Parent).Parent).Parent)).Parent)).Parent)).GrdContentOrderDetail.Visibility = Visibility.Visible;
                    Ozyaysan.UserControls.UCOrderDetail oWindowsProductDetail = new UserControls.UCOrderDetail(Int32.Parse((sender as Button).CommandParameter.ToString()));
                    (((Start)((Grid)(((TabControl)(((TabItem)((Grid)((Grid)(this.Parent)).Parent).Parent).Parent)).Parent)).Parent)).GrdContentOrderDetail.Children.Clear();
                    (((Start)((Grid)(((TabControl)(((TabItem)((Grid)((Grid)(this.Parent)).Parent).Parent).Parent)).Parent)).Parent)).GrdContentOrderDetail.Children.Add(oWindowsProductDetail);

                }
            }
        }

        #region Paging Related Part
        private void btn1Forward_Click(object sender, RoutedEventArgs e)
        {
            int page = Int32.Parse(txtPage.Text);
            txtPage.Text = (page + 1).ToString();
            dgOrders.ItemsSource = BLL.Order.getOrderList(CreateWhereString(), Int32.Parse(txtPage.Text), pageSize, "", 0).Tables[1].DefaultView;
            int lastPage = (totalRecordCount / pageSize);
            if (Int32.Parse(txtPage.Text) == lastPage)
            {
                btnAllForward.IsEnabled = false;
                btn1Forward.IsEnabled = false;
            }
            btnAlPrevious.IsEnabled = true;
            btn1Previous.IsEnabled = true;
            dgOrders.UpdateLayout();
            UpdateGridLayout();
        }

        private void btn1Previous_Click(object sender, RoutedEventArgs e)
        {
            int page = Int32.Parse(txtPage.Text);
            txtPage.Text = (page - 1).ToString();
            dgOrders.ItemsSource = BLL.Order.getOrderList(CreateWhereString(), Int32.Parse(txtPage.Text), pageSize, "", 0).Tables[1].DefaultView;
            if (Int32.Parse(txtPage.Text) == 0)
            {
                btnAlPrevious.IsEnabled = false;
                btn1Previous.IsEnabled = false;

            }
            btn1Forward.IsEnabled = true;
            btnAllForward.IsEnabled = true;
            dgOrders.UpdateLayout();
            UpdateGridLayout();
        }

        private void btnAlPrevious_Click(object sender, RoutedEventArgs e)
        {
            txtPage.Text = "0";
            dgOrders.ItemsSource = BLL.Order.getOrderList(CreateWhereString(), 0, pageSize, "",0).Tables[1].DefaultView;
            btnAlPrevious.IsEnabled = false;
            btn1Previous.IsEnabled = false;
            btn1Forward.IsEnabled = true;
            btnAllForward.IsEnabled = true;
            dgOrders.UpdateLayout();
            UpdateGridLayout();
        }

        private void btnAllForward_Click(object sender, RoutedEventArgs e)
        {
            if (totalRecordCount > pageSize)
            {
                txtPage.Text = Convert.ToInt32((totalRecordCount / pageSize)).ToString();
                dgOrders.ItemsSource = BLL.Order.getOrderList(CreateWhereString(), Int32.Parse(txtPage.Text), pageSize, "", 0).Tables[1].DefaultView;
                btnAllForward.IsEnabled = false;
                btn1Forward.IsEnabled = false;
                btnAlPrevious.IsEnabled = true;
                btn1Previous.IsEnabled = true;
                dgOrders.UpdateLayout();
                UpdateGridLayout();
            }
        }

        #endregion



        public Microsoft.Windows.Controls.DataGridCell GetCell(Microsoft.Windows.Controls.DataGridRow rowContainer, int column)
        {
            return Helper.GetCell(dgOrders, rowContainer, column);

        }

        private void dgOrders_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateGridLayout();
        }

        private void UpdateGridLayout()
        {
            //
            Microsoft.Windows.Controls.DataGridCell cell;
            for (int i = 0; i < Int32.Parse(lblRecordCount.Content.ToString()); i++)
            {
                Microsoft.Windows.Controls.DataGridRow row = (Microsoft.Windows.Controls.DataGridRow)dgOrders.ItemContainerGenerator.ContainerFromIndex(i);
                if (row != null)
                {
                    DataRowView item = row.Item as DataRowView;
                    if (item != null)
                    {
                        switch (Int32.Parse(item["OrderStatus"].ToString()))
                        {
                            case 1:
                                cell = GetCell(row, 4);
                                cell.Background = Brushes.Red;
                                break;
                            case 2:
                                cell = GetCell(row, 4);
                                cell.Background = Brushes.Yellow;
                                break;
                            case 3:
                                cell = GetCell(row, 4);
                                cell.Background = Brushes.HotPink;
                                break;
                            case 4:
                                cell = GetCell(row, 4);
                                cell.Background = Brushes.Orange;
                                break;
                            case 5:
                                cell = GetCell(row, 4);
                                cell.Background = Brushes.Green;
                                break;
                            case 6:
                                cell = GetCell(row, 4);
                                cell.Background = Brushes.Gray;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
        private void txtOrderNo_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataSet ds = BLL.Order.getOrderList(CreateWhereString(), 0, pageSize, "", 1);
            dgOrders.ItemsSource = ds.Tables[1].DefaultView;
            lblRecordCount.Content = ds.Tables[1].Rows.Count;
            dgOrders.UpdateLayout();
            UpdateGridLayout();
        }
    }
    public static class Helper
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
