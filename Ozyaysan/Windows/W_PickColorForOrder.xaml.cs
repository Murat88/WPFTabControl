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


namespace Ozyaysan.Windows
{
    /// <summary>
    /// Interaction logic for W_PickColorForOrder.xaml
    /// </summary>
    public partial class W_PickColorForOrder : Window
    {
        #region Fields
        private BLL.Color m_SelectedColor = new BLL.Color(0, BLL.Enumarations.State.Aktif);

        
        #endregion
        #region Properties
        public BLL.Color SelectedColor
        {
            get { return m_SelectedColor; }
            set { m_SelectedColor = value; }
        }
        #endregion
        private int totalRecordCount=0;
        private int pageSize = 8;
        public W_PickColorForOrder()
        {
            InitializeComponent();
            LoadData();
            DataSet ds = BLL.Color.getColorList(CreateWhereString(), 0, pageSize, "", 0);
            dgColors.ItemsSource = ds.Tables[1].DefaultView;
            this.totalRecordCount = BLL.Color.getColorList(CreateWhereString(), 0, int.MaxValue-1, "", 0).Tables[1].Rows.Count;
            btn1Previous.IsEnabled = false;
            btnAlPrevious.IsEnabled = false;
            lblRecordCount.Content = this.totalRecordCount.ToString();
        }
       
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
           
            DataSet ds =  BLL.Color.getColorList(CreateWhereString(), 0, pageSize, "", 0);
            dgColors.ItemsSource = ds.Tables[1].DefaultView;
            lblRecordCount.Content = ds.Tables[1].Rows.Count;
        }

        private string CreateWhereString()
        {
            string strWhere = "";
           
            if (txtColorCode.Text != "")
            {
                strWhere += "and LU.CName like '%" + txtColorCode.Text + "%'";
            }
            if (cmbState.SelectedValue.ToString()!="0")
            {
                 strWhere += "and LU.State=" + cmbState.SelectedValue + "";
            }
            this.totalRecordCount = BLL.Color.getColorList(strWhere, 0, int.MaxValue - 1, "", 0).Tables[1].Rows.Count;

            return strWhere;
        }       

        private void LoadData()
        {

            DataTable dtState = BLL.Utility.GetStateListForDDL();
            cmbState.ItemsSource = dtState.DefaultView;
            cmbState.DisplayMemberPath = dtState.Columns["SName"].ToString();
            cmbState.SelectedValuePath = dtState.Columns["SID"].ToString();
            cmbState.SelectedIndex = 0;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.SelectedColor = new BLL.Color(Int32.Parse((sender as Button).CommandParameter.ToString()), BLL.Enumarations.State.Aktif);
            this.Close();
        }

        private void txtColorCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            dgColors.ItemsSource = BLL.Color.getColorList(CreateWhereString(), 0, pageSize, "", 0).Tables[1].DefaultView;
            
        }
        #region Paging Related Part
        private void btn1Forward_Click(object sender, RoutedEventArgs e)
        {
            int page = Int32.Parse(txtPage.Text);
            txtPage.Text = (page + 1).ToString();
            dgColors.ItemsSource = BLL.Color.getColorList(CreateWhereString(), Int32.Parse(txtPage.Text), pageSize, "", 0).Tables[1].DefaultView;
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
            dgColors.ItemsSource = BLL.Color.getColorList(CreateWhereString(), Int32.Parse(txtPage.Text), pageSize, "", 0).Tables[1].DefaultView;
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
            dgColors.ItemsSource = BLL.Color.getColorList(CreateWhereString(), 0, pageSize, "", 0).Tables[1].DefaultView;
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
                dgColors.ItemsSource = BLL.Color.getColorList(CreateWhereString(), Int32.Parse(txtPage.Text), pageSize, "", 0).Tables[1].DefaultView;
                btnAllForward.IsEnabled = false;
                btn1Forward.IsEnabled = false;
                btnAlPrevious.IsEnabled = true;
                btn1Previous.IsEnabled = true;
            }
        }

        #endregion
       
    }
}
