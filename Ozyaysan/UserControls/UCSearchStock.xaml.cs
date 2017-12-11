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
using System.Windows.Shapes;
using BLL = OzyaysanBusinessEngine;
using System.Data;
using Ozyaysan.Windows;

namespace Ozyaysan.UserControls
{
    /// <summary>
    /// Interaction logic for UCSearchStock.xaml
    /// </summary>
    public partial class UCSearchStock : UserControl
    {
        private int totalRecordCount = 0;
        private int pageSize = 8;
        public UCSearchStock()
        {
            InitializeComponent();
            LoadData();
            DataSet ds = BLL.Product.getProductList(CreateWhereString(), 0, pageSize, "", 0);
            dgProducts.ItemsSource = ds.Tables[1].DefaultView;
            this.totalRecordCount = BLL.Product.getProductList(CreateWhereString(), 0, int.MaxValue - 1, "", 0).Tables[1].Rows.Count;
            btn1Previous.IsEnabled = false;
            btnAlPrevious.IsEnabled = false;
            lblRecordCount.Content = this.totalRecordCount.ToString();
            if (BLL.Product.getProductList(CreateWhereString(), 0, int.MaxValue - 1, "", 0).Tables[1].Rows.Count < pageSize)
            {
                btn1Forward.IsEnabled = false;
                btnAllForward.IsEnabled = false;
            }
        }

        private string CreateWhereString()
        {
            string strWhere = "";

            

            if (cmbProduct.SelectedValue.ToString() != "0")
            {
                strWhere += "and LU.PID=" + cmbProduct.SelectedValue + "";
            }
            this.totalRecordCount = BLL.Product.getProductList(strWhere, 0, int.MaxValue - 1, "", 0).Tables[1].Rows.Count;

            return strWhere;
        }

        private void LoadData()
        {
            DataTable dtProduct = BLL.Product.GetProductListForDDL();
            cmbProduct.ItemsSource = dtProduct.DefaultView;
            cmbProduct.DisplayMemberPath = dtProduct.Columns["PName"].ToString();
            cmbProduct.SelectedValuePath = dtProduct.Columns["PID"].ToString();
            cmbProduct.SelectedIndex = 0;
        }
        #region Events
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

          private void btnSearch_Click(object sender, RoutedEventArgs e)
          {
              DataSet ds = BLL.Product.getProductList(CreateWhereString(), 0, pageSize, "", 0);
              dgProducts.ItemsSource = ds.Tables[1].DefaultView;
              lblRecordCount.Content = ds.Tables[1].Rows.Count;
          }
        #endregion
          #region Paging Related Part
          private void btn1Forward_Click(object sender, RoutedEventArgs e)
          {
              int page = Int32.Parse(txtPage.Text);
              txtPage.Text = (page + 1).ToString();
              dgProducts.ItemsSource = BLL.Machine.getMachineList(CreateWhereString(), Int32.Parse(txtPage.Text), pageSize, "", 0).Tables[1].DefaultView;
              int lastPage = (totalRecordCount / pageSize);
              if (Int32.Parse(txtPage.Text) == lastPage)
              {
                  btnAllForward.IsEnabled = false;
                  btn1Forward.IsEnabled = false;
              }
              btnAlPrevious.IsEnabled = true;
              btn1Previous.IsEnabled = true;
          }

          private void btn1Previous_Click(object sender, RoutedEventArgs e)
          {
              int page = Int32.Parse(txtPage.Text);
              txtPage.Text = (page - 1).ToString();
              dgProducts.ItemsSource = BLL.Machine.getMachineList(CreateWhereString(), Int32.Parse(txtPage.Text), pageSize, "", 0).Tables[1].DefaultView;
              if (Int32.Parse(txtPage.Text) == 0)
              {
                  btnAlPrevious.IsEnabled = false;
                  btn1Previous.IsEnabled = false;

              }
              btn1Forward.IsEnabled = true;
              btnAllForward.IsEnabled = true;
          }

          private void btnAlPrevious_Click(object sender, RoutedEventArgs e)
          {
              txtPage.Text = "0";
              dgProducts.ItemsSource = BLL.Machine.getMachineList(CreateWhereString(), 0, pageSize, "", 0).Tables[1].DefaultView;
              btnAlPrevious.IsEnabled = false;
              btn1Previous.IsEnabled = false;
              btn1Forward.IsEnabled = true;
              btnAllForward.IsEnabled = true;
          }

          private void btnAllForward_Click(object sender, RoutedEventArgs e)
          {
              if (totalRecordCount > pageSize)
              {
                  txtPage.Text = Convert.ToInt32((totalRecordCount / pageSize)).ToString();
                  dgProducts.ItemsSource = BLL.Machine.getMachineList(CreateWhereString(), Int32.Parse(txtPage.Text), pageSize, "", 0).Tables[1].DefaultView;
                  btnAllForward.IsEnabled = false;
                  btn1Forward.IsEnabled = false;
                  btnAlPrevious.IsEnabled = true;
                  btn1Previous.IsEnabled = true;
              }

          }

          #endregion
    }
}
