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

namespace Ozyaysan.UserControls
{
    /// <summary>
    /// Interaction logic for UCSearchCustomer.xaml
    /// </summary>
    public partial class UCSearchCustomer : UserControl
    {
        private int totalRecordCount = 0;
        private int pageSize = 8;
        public UCSearchCustomer()
        {
            InitializeComponent();
            LoadData();
            dgCustomers.ItemsSource = BLL.Customer.getCustomerList(CreateWhereString(), 0, pageSize, "", 0).Tables[1].DefaultView;
            this.totalRecordCount =  BLL.Customer.getCustomerList(CreateWhereString(), 0, int.MaxValue - 1, "", 0).Tables[1].Rows.Count;
            btn1Previous.IsEnabled = false;
            btnAlPrevious.IsEnabled = false;
            lblRecordCount.Content = this.totalRecordCount.ToString();

            if (BLL.Customer.getCustomerList(CreateWhereString(), 0, int.MaxValue - 1, "", 0).Tables[1].Rows.Count < pageSize)
            {
                btn1Forward.IsEnabled = false;
                btnAllForward.IsEnabled = false;
            }
        }
       
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            DataSet ds =  BLL.Customer.getCustomerList(CreateWhereString(), 0, pageSize, "", 0);
            dgCustomers.ItemsSource = ds.Tables[1].DefaultView;
            lblRecordCount.Content = BLL.Customer.getCustomerList(CreateWhereString(), 0, Int32.MaxValue-1, "", 0).Tables[1].Rows.Count;
           
        }

        private string CreateWhereString()
        {
            string strWhere = "";
           
            if (txtPName.Text != "")
            {
                strWhere += "and LU.CName like '%" + txtPName.Text + "%'";
            }

            if (txtCountry.Text != "")
            {
                strWhere += "and LU.Country like '%" + txtCountry.Text + "%'";
            }

            if (txtCity.Text != "")
            {
                strWhere += "and LU.City like '%" + txtCity.Text + "%'";
            }

            if (txtEmail.Text != "")
            {
                strWhere += "and LU.Email like '%" + txtEmail.Text + "%'";
            }

            if (txtAddress.Text != "")
            {
                strWhere += "and LU.Address like '%" + txtAddress.Text + "%'";
            }
            if (cmbState.SelectedValue.ToString()!="0")
            {
                 strWhere += "and LU.State=" + cmbState.SelectedValue + "";
            }
            this.totalRecordCount =  BLL.Customer.getCustomerList(strWhere, 0, int.MaxValue - 1, "", 0).Tables[1].Rows.Count;

            return strWhere;
        }       

        private void LoadData()
        {

            DataTable dtState = BLL.Utility.GetStateListForDDL();
            cmbState.ItemsSource = dtState.DefaultView;
            cmbState.DisplayMemberPath = dtState.Columns["SName"].ToString();
            cmbState.SelectedValuePath = dtState.Columns["SID"].ToString();
            cmbState.SelectedIndex = 2;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).CommandParameter != null)
            {
                (((Start)((Grid)(((TabControl)(((TabItem)((Grid)((Grid)(this.Parent)).Parent).Parent).Parent)).Parent)).Parent)).GrdContentSearchCustomer.Visibility = Visibility.Hidden;
                (((Start)((Grid)(((TabControl)(((TabItem)((Grid)((Grid)(this.Parent)).Parent).Parent).Parent)).Parent)).Parent)).GrdContentAddCustomer.Visibility = Visibility.Hidden;
                (((Start)((Grid)(((TabControl)(((TabItem)((Grid)((Grid)(this.Parent)).Parent).Parent).Parent)).Parent)).Parent)).GrdContentCustomerDetail.Visibility = Visibility.Visible;
                Ozyaysan.UserControls.UCCustomerDetail oWindowsCustomerDetail = new UserControls.UCCustomerDetail(Int32.Parse((sender as Button).CommandParameter.ToString()));
                (((Start)((Grid)(((TabControl)(((TabItem)((Grid)((Grid)(this.Parent)).Parent).Parent).Parent)).Parent)).Parent)).GrdContentCustomerDetail.Children.Clear();
                (((Start)((Grid)(((TabControl)(((TabItem)((Grid)((Grid)(this.Parent)).Parent).Parent).Parent)).Parent)).Parent)).GrdContentCustomerDetail.Children.Add(oWindowsCustomerDetail);
            }
        }

        private void txtPName_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataSet ds = BLL.Customer.getCustomerList(CreateWhereString(), 0, pageSize, "", 0);
            dgCustomers.ItemsSource = ds.Tables[1].DefaultView;
            lblRecordCount.Content = ds.Tables[1].Rows.Count;
            
        }
        #region Paging Related Part
        private void btn1Forward_Click(object sender, RoutedEventArgs e)
        {
            int page = Int32.Parse(txtPage.Text);
            txtPage.Text = (page + 1).ToString();
            dgCustomers.ItemsSource =  BLL.Customer.getCustomerList(CreateWhereString(), Int32.Parse(txtPage.Text), pageSize, "", 0).Tables[1].DefaultView;
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
            dgCustomers.ItemsSource =  BLL.Customer.getCustomerList(CreateWhereString(), Int32.Parse(txtPage.Text), pageSize, "", 0).Tables[1].DefaultView;
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
            dgCustomers.ItemsSource =  BLL.Customer.getCustomerList(CreateWhereString(), 0, pageSize, "", 0).Tables[1].DefaultView;
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
                dgCustomers.ItemsSource = BLL.Customer.getCustomerList(CreateWhereString(), Int32.Parse(txtPage.Text), pageSize, "", 0).Tables[1].DefaultView;
                btnAllForward.IsEnabled = false;
                btn1Forward.IsEnabled = false;
                btnAlPrevious.IsEnabled = true;
                btn1Previous.IsEnabled = true;
            }
        }

        #endregion
    }
}
