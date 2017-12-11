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
    /// Interaction logic for UCSearchMachine.xaml
    /// </summary>
    public partial class UCSearchMachine : UserControl
    {
        private int totalRecordCount = 0;
        private int pageSize = 8;
        public UCSearchMachine()
        {
            InitializeComponent();
            LoadData();
            DataSet ds = BLL.Machine.getMachineList(CreateWhereString(), 0, pageSize, "", 0);
            dgMachines.ItemsSource = ds.Tables[1].DefaultView;
            this.totalRecordCount = BLL.Machine.getMachineList(CreateWhereString(), 0, int.MaxValue - 1, "", 0).Tables[1].Rows.Count;
            btn1Previous.IsEnabled = false;
            btnAlPrevious.IsEnabled = false;
            lblRecordCount.Content = this.totalRecordCount.ToString();
            if (BLL.Machine.getMachineList(CreateWhereString(), 0, int.MaxValue - 1, "", 0).Tables[1].Rows.Count < pageSize)
            {
                btn1Forward.IsEnabled = false;
                btnAllForward.IsEnabled = false;
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

            DataSet ds = BLL.Machine.getMachineList(CreateWhereString(), 0, pageSize, "", 0);
            dgMachines.ItemsSource = ds.Tables[1].DefaultView;
            lblRecordCount.Content = ds.Tables[1].Rows.Count;
        }

        private string CreateWhereString()
        {
            string strWhere = "";

            if (txtMachineCode.Text != "")
            {
                strWhere += "and LU.MCode like '%" + txtMachineCode.Text + "%'";
            }

            if (cmbState.SelectedValue.ToString() != "0")
            {
                strWhere += "and LU.State=" + cmbState.SelectedValue + "";
            }
            this.totalRecordCount = BLL.Machine.getMachineList(strWhere, 0, int.MaxValue - 1, "", 0).Tables[1].Rows.Count;

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
                (((Start)((Grid)(((TabControl)(((TabItem)((Grid)((Grid)(this.Parent)).Parent).Parent).Parent)).Parent)).Parent)).GrdContentSearchMachine.Visibility = Visibility.Hidden;
                (((Start)((Grid)(((TabControl)(((TabItem)((Grid)((Grid)(this.Parent)).Parent).Parent).Parent)).Parent)).Parent)).GrdContentAddMachine.Visibility = Visibility.Hidden;
                (((Start)((Grid)(((TabControl)(((TabItem)((Grid)((Grid)(this.Parent)).Parent).Parent).Parent)).Parent)).Parent)).GrdContentMachineDetail.Visibility = Visibility.Visible;
                Ozyaysan.UserControls.UCMachineDetail oWindowsMachineDetail = new UserControls.UCMachineDetail(Int32.Parse((sender as Button).CommandParameter.ToString()));
                (((Start)((Grid)(((TabControl)(((TabItem)((Grid)((Grid)(this.Parent)).Parent).Parent).Parent)).Parent)).Parent)).GrdContentMachineDetail.Children.Clear();
                (((Start)((Grid)(((TabControl)(((TabItem)((Grid)((Grid)(this.Parent)).Parent).Parent).Parent)).Parent)).Parent)).GrdContentMachineDetail.Children.Add(oWindowsMachineDetail);
            }

        }

        private void txtMachineCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataSet ds = BLL.Machine.getMachineList(CreateWhereString(), 0, pageSize, "", 0);
            dgMachines.ItemsSource = ds.Tables[1].DefaultView;
            lblRecordCount.Content = BLL.Machine.getMachineList(CreateWhereString(), 0, Int32.MaxValue-1, "", 0).Tables[1].Rows.Count;

        }
        #region Paging Related Part
        private void btn1Forward_Click(object sender, RoutedEventArgs e)
        {
            int page = Int32.Parse(txtPage.Text);
            txtPage.Text = (page + 1).ToString();
            dgMachines.ItemsSource = BLL.Machine.getMachineList(CreateWhereString(), Int32.Parse(txtPage.Text), pageSize, "", 0).Tables[1].DefaultView;
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
            dgMachines.ItemsSource = BLL.Machine.getMachineList(CreateWhereString(), Int32.Parse(txtPage.Text), pageSize, "", 0).Tables[1].DefaultView;
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
            dgMachines.ItemsSource = BLL.Machine.getMachineList(CreateWhereString(), 0, pageSize, "", 0).Tables[1].DefaultView;
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
                dgMachines.ItemsSource = BLL.Machine.getMachineList(CreateWhereString(), Int32.Parse(txtPage.Text), pageSize, "", 0).Tables[1].DefaultView;
                btnAllForward.IsEnabled = false;
                btn1Forward.IsEnabled = false;
                btnAlPrevious.IsEnabled = true;
                btn1Previous.IsEnabled = true;
            }

        }

        #endregion
    }
}
