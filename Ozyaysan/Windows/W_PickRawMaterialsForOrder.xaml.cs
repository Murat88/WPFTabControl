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
    /// Interaction logic for W_PickRawMaterialsForOrder.xaml
    /// </summary>
    public partial class W_PickRawMaterialsForOrder : Window
    {
       
        private int totalRecordCount=0;
        private int pageSize = 8;
        private BLL.RawMaterials m_SelectedRawMaterials = new BLL.RawMaterials(0, BLL.Enumarations.State.Aktif);

        public BLL.RawMaterials SelectedRawMaterials
        {
            get { return m_SelectedRawMaterials; }
            set { m_SelectedRawMaterials = value; }
        }
        public W_PickRawMaterialsForOrder()
        {
            InitializeComponent();
            LoadData();
            DataSet ds = BLL.RawMaterials.getRawMaterialsList(CreateWhereString(), 0, pageSize, "", 0);
            dgRawMaterials.ItemsSource = ds.Tables[1].DefaultView;
            this.totalRecordCount = BLL.RawMaterials.getRawMaterialsList(CreateWhereString(), 0, int.MaxValue-1, "", 0).Tables[1].Rows.Count;
            btn1Previous.IsEnabled = false;
            btnAlPrevious.IsEnabled = false;
            lblRecordCount.Content = this.totalRecordCount.ToString();
            if (BLL.RawMaterials.getRawMaterialsList(CreateWhereString(), 0, int.MaxValue - 1, "", 0).Tables[1].Rows.Count < pageSize)
            {
                btn1Forward.IsEnabled = false;
                btnAllForward.IsEnabled = false;
            }
        }
       
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
           
            DataSet ds =  BLL.RawMaterials.getRawMaterialsList(CreateWhereString(), 0, pageSize, "", 0);
            dgRawMaterials.ItemsSource = ds.Tables[1].DefaultView;
            lblRecordCount.Content = ds.Tables[1].Rows.Count;
        }

        private string CreateWhereString()
        {
            string strWhere = "";
           
            if ( txtCompound.Text != "")
            {
                strWhere += "and LU.Compound like '%" + txtCompound.Text + "%'";
            }
            //-----------
            if (txtWireDiameter1.Text != "" && txtWireDiameter2.Text == "")
            {
                strWhere += "and LU.WireDiameter>=" + Int32.Parse(txtWireDiameter1.Text) + " ";
            }
            else if (txtWireDiameter1.Text == "" && txtWireDiameter2.Text != "")
            {
                strWhere += "and LU.WireDiameter<=" + Int32.Parse(txtWireDiameter2.Text) + " ";
            }
            else if (txtWireDiameter1.Text != "" && txtWireDiameter2.Text != "")
            {
                strWhere += "and LU.WireDiameter between " + Int32.Parse(txtWireDiameter1.Text) + " and " + Int32.Parse(txtWireDiameter2.Text) + " ";
            }
            //-------------
            if (txtHardness1.Text != "" && txtHardness2.Text == "")
            {
                strWhere += "and LU.Hardness>=" + Int32.Parse(txtHardness1.Text) + " ";
            }
            else if (txtHardness1.Text == "" && txtHardness2.Text != "")
            {
                strWhere += "and LU.Hardness<=" + Int32.Parse(txtHardness2.Text) + " ";
            }
            else if (txtHardness1.Text != "" && txtHardness2.Text != "")
            {
                strWhere += "and LU.Hardness between " + Int32.Parse(txtHardness1.Text) + " and " + Int32.Parse(txtHardness2.Text) + " ";
            }
            //-----------
            if (cmbState.SelectedValue.ToString()!="0")
            {
                 strWhere += "and LU.State=" + cmbState.SelectedValue + "";
            }
            this.totalRecordCount = BLL.RawMaterials.getRawMaterialsList(strWhere, 0, int.MaxValue - 1, "", 0).Tables[1].Rows.Count;

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
            if ((sender as Button).CommandParameter != null)
            {
                this.SelectedRawMaterials = new BLL.RawMaterials(Int32.Parse((sender as Button).CommandParameter.ToString()), BLL.Enumarations.State.Aktif);
                this.Close();
            }
        }

       
        #region Paging Related Part
        private void btn1Forward_Click(object sender, RoutedEventArgs e)
        {
            int page = Int32.Parse(txtPage.Text);
            txtPage.Text = (page + 1).ToString();
            dgRawMaterials.ItemsSource = BLL.RawMaterials.getRawMaterialsList(CreateWhereString(), Int32.Parse(txtPage.Text), pageSize, "", 0).Tables[1].DefaultView;
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
            dgRawMaterials.ItemsSource = BLL.RawMaterials.getRawMaterialsList(CreateWhereString(), Int32.Parse(txtPage.Text), pageSize, "", 0).Tables[1].DefaultView;
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
            dgRawMaterials.ItemsSource = BLL.RawMaterials.getRawMaterialsList(CreateWhereString(), 0, pageSize, "", 0).Tables[1].DefaultView;
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
                dgRawMaterials.ItemsSource = BLL.RawMaterials.getRawMaterialsList(CreateWhereString(), Int32.Parse(txtPage.Text), pageSize, "", 0).Tables[1].DefaultView;
                btnAllForward.IsEnabled = false;
                btn1Forward.IsEnabled = false;
                btnAlPrevious.IsEnabled = true;
                btn1Previous.IsEnabled = true;
            }
        }

        #endregion
       
       
    }
}
