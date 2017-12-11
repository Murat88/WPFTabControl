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
    /// Interaction logic for UCSearchPaintProduct_.xaml
    /// </summary>
    public partial class UCSearchPaintProduct : UserControl
    {

        private int totalRecordCount = 0;
        private int pageSize = 8;
        public UCSearchPaintProduct()
        {
            InitializeComponent();
            LoadData();
            dgPaintProduct.ItemsSource = BLL.Product.getPaintProductList(CreateWhereString(), 0, pageSize, "", 0).Tables[1].DefaultView;
            this.totalRecordCount = BLL.Product.getPaintProductList(CreateWhereString(), 0, int.MaxValue - 1, "", 0).Tables[1].Rows.Count;
            btn1Previous.IsEnabled = false;
            btnAlPrevious.IsEnabled = false;
            lblRecordCount.Content = this.totalRecordCount.ToString();

            if (BLL.Product.getPaintProductList(CreateWhereString(), 0, int.MaxValue - 1, "", 0).Tables[1].Rows.Count < pageSize)
            {
                btn1Forward.IsEnabled = false;
                btnAllForward.IsEnabled = false;
            }
        }

        private void LoadData()
        {
           
            cmbOPStatus.SelectedIndex = 0;
        }
        private string CreateWhereString()
        {
            string strWhere = "";
            if (cmbOPStatus.SelectedIndex!=0)
            {
                strWhere += " and LU.OPStatus=" + (cmbOPStatus.SelectedItem as ComboBoxItem).Tag.ToString() + ""; 
            }
            else
            {
                strWhere += " and LU.OPStatus=" + (int)BLL.Enumarations.OrderStatus.Imalatta + " or LU.OPStatus=" + (int)BLL.Enumarations.OrderStatus.Boyahanede + " or LU.OPStatus=" + (int)BLL.Enumarations.OrderStatus.Boyanıyor + " or  LU.OPStatus=" + (int)BLL.Enumarations.OrderStatus.Hazir + "";
            }
          
            if (txtCode.Text!="")
            {
                strWhere = " and P.Code like '%" + txtCode.Text + "%'";
            }
            if (dpDateOfDeliveryFirst.SelectedDate != null)
            {
                DateTime dtFirst = dpDateOfDeliveryFirst.SelectedDate.Value;
                if (dpDateOfDeliverySecond.SelectedDate != null)
                {
                    DateTime dtSecond = dpDateOfDeliverySecond.SelectedDate.Value;
                    strWhere += " and convert(datetime, '" + dtSecond + "', 103)>convert(datetime, O.DateOfDelivery, 103) AND convert(datetime, O.DateOfDelivery, 103)>convert(datetime, '" + dtFirst + "', 103)";
                }
                else
                {
                    strWhere += " and convert(datetime, O.DateOfDelivery, 103)>convert(datetime, '" + dtFirst + "', 103)";

                }

            }
            else
            {
                if (dpDateOfDeliverySecond.SelectedDate != null)
                {
                    DateTime dtSecond = dpDateOfDeliverySecond.SelectedDate.Value;
                    strWhere += "and convert(datetime, '" + dtSecond + "', 103)>convert(datetime, O.DateOfDelivery, 103)";
                }

            }
            return strWhere;
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
                            //ClearAllControls();
                            dgPaintProduct.ItemsSource = BLL.Product.getPaintProductList(CreateWhereString(), 0, pageSize, "", 0).Tables[1].DefaultView;
                            this.totalRecordCount = BLL.Product.getPaintProductList(CreateWhereString(), 0, int.MaxValue - 1, "", 0).Tables[1].Rows.Count;
                            lblRecordCount.Content = this.totalRecordCount.ToString();
                            dgPaintProduct.UpdateLayout();
                            UpdateGridLayout();
                        }
                    }
                }            
            }
        }

        private void ClearAllControls()
        {
            txtCode.Text = "";
            cmbOPStatus.SelectedIndex = 0;
            dpDateOfDeliveryFirst.SelectedDate = null;
            dpDateOfDeliverySecond.SelectedDate = null;
        }
        #region Paging Related Part
        private void btn1Forward_Click(object sender, RoutedEventArgs e)
        {
            int page = Int32.Parse(txtPage.Text);
            txtPage.Text = (page + 1).ToString();
            dgPaintProduct.ItemsSource = BLL.Product.getPaintProductList(CreateWhereString(), Int32.Parse(txtPage.Text), pageSize, "", 0).Tables[1].DefaultView;
            int lastPage = (totalRecordCount / pageSize);
            if (Int32.Parse(txtPage.Text) == lastPage)
            {
                btnAllForward.IsEnabled = false;
                btn1Forward.IsEnabled = false;
            }
            btnAlPrevious.IsEnabled = true;
            btn1Previous.IsEnabled = true;
            dgPaintProduct.UpdateLayout();
            UpdateGridLayout();
        }

        private void btn1Previous_Click(object sender, RoutedEventArgs e)
        {
            int page = Int32.Parse(txtPage.Text);
            txtPage.Text = (page - 1).ToString();
            dgPaintProduct.ItemsSource = BLL.Product.getPaintProductList(CreateWhereString(), Int32.Parse(txtPage.Text), pageSize, "", 0).Tables[1].DefaultView;
            if (Int32.Parse(txtPage.Text) == 0)
            {
                btnAlPrevious.IsEnabled = false;
                btn1Previous.IsEnabled = false;

            }
            btn1Forward.IsEnabled = true;
            btnAllForward.IsEnabled = true;
            dgPaintProduct.UpdateLayout();
            UpdateGridLayout();
        }

        private void btnAlPrevious_Click(object sender, RoutedEventArgs e)
        {
            txtPage.Text = "0";
            dgPaintProduct.ItemsSource = BLL.Product.getPaintProductList(CreateWhereString(), 0, pageSize, "", 0).Tables[1].DefaultView;
            btnAlPrevious.IsEnabled = false;
            btn1Previous.IsEnabled = false;
            btn1Forward.IsEnabled = true;
            btnAllForward.IsEnabled = true;
            dgPaintProduct.UpdateLayout();
            UpdateGridLayout();
        }

        private void btnAllForward_Click(object sender, RoutedEventArgs e)
        {
            if (totalRecordCount > pageSize)
            {
                txtPage.Text = Convert.ToInt32((totalRecordCount / pageSize)).ToString();
                dgPaintProduct.ItemsSource = BLL.Product.getPaintProductList(CreateWhereString(), Int32.Parse(txtPage.Text), pageSize, "", 0).Tables[1].DefaultView;
                btnAllForward.IsEnabled = false;
                btn1Forward.IsEnabled = false;
                btnAlPrevious.IsEnabled = true;
                btn1Previous.IsEnabled = true;
                dgPaintProduct.UpdateLayout();
                UpdateGridLayout();
            }
        }

        #endregion
        public Microsoft.Windows.Controls.DataGridCell GetCell(Microsoft.Windows.Controls.DataGridRow rowContainer, int column)
        {
            return HelperPaint.GetCell(dgPaintProduct, rowContainer, column);

        }

        private void dgPaintProduct_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateGridLayout();
        }

        private void UpdateGridLayout()
        {
            //
            Microsoft.Windows.Controls.DataGridCell cell;
            for (int i = 0; i < Int32.Parse(lblRecordCount.Content.ToString()); i++)
            {
                Microsoft.Windows.Controls.DataGridRow row = (Microsoft.Windows.Controls.DataGridRow)dgPaintProduct.ItemContainerGenerator.ContainerFromIndex(i);
                if (row != null)
                {
                    DataRowView item = row.Item as DataRowView;
                    if (item != null)
                    {
                        switch (Int32.Parse(item["OPStatus"].ToString()))
                        {
                            case 1:
                                cell = GetCell(row, 5);
                                cell.Background = Brushes.Red;                             
                                break;
                            case 2:
                                cell = GetCell(row, 5);
                                cell.Background = Brushes.Yellow;
                                break;
                            case 3:
                                cell = GetCell(row, 5);
                                cell.Background = Brushes.HotPink;
                                break;
                            case 4:
                                cell = GetCell(row, 5);                            
                                cell.Background = Brushes.Orange;
                                //button_cell = GetCell(row, 0);
                                //var cp = button_cell.Content as ContentPresenter;
                                //var button = cp.ContentTemplate.FindName("btnStart", cp) as Button;
                                //button.IsEnabled = false;
                                break;
                            case 5:
                                cell = GetCell(row, 5);
                                cell.Background = Brushes.Green;                              
                                break;
                            case 6:
                                cell = GetCell(row, 5);
                                cell.Background = Brushes.Gray;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            dgPaintProduct.ItemsSource = BLL.Product.getPaintProductList(CreateWhereString(), 0, pageSize, "", 0).Tables[1].DefaultView;
            this.totalRecordCount = BLL.Product.getPaintProductList(CreateWhereString(), 0, int.MaxValue - 1, "", 0).Tables[1].Rows.Count;
            this.totalRecordCount = BLL.Product.getPaintProductList(CreateWhereString(), 0, int.MaxValue - 1, "", 0).Tables[1].Rows.Count;
            lblRecordCount.Content = this.totalRecordCount.ToString();
            dgPaintProduct.UpdateLayout();
            UpdateGridLayout();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearAllControls();
            dgPaintProduct.ItemsSource = BLL.Product.getPaintProductList(CreateWhereString(), 0, pageSize, "", 0).Tables[1].DefaultView;
            this.totalRecordCount = BLL.Product.getPaintProductList(CreateWhereString(), 0, int.MaxValue - 1, "", 0).Tables[1].Rows.Count;
            this.totalRecordCount = BLL.Product.getPaintProductList(CreateWhereString(), 0, int.MaxValue - 1, "", 0).Tables[1].Rows.Count;
            lblRecordCount.Content = this.totalRecordCount.ToString();
            dgPaintProduct.UpdateLayout();
            UpdateGridLayout();
        }
    }
    public static class HelperPaint
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
