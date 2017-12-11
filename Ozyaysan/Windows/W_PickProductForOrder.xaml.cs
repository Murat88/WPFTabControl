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
    /// Interaction logic for W_PickProductForOrder.xaml
    /// </summary>
    public partial class W_PickProductForOrder : Window
    {
        #region Fields
        private int totalRecordCount = 0;
        private int pageSize = 8;
        private BLL.Product m_SelectedProduct=new BLL.Product(0,BLL.Enumarations.State.Aktif);
        #endregion
        #region Properties
        public BLL.Product SelectedProduct
        {
            get { return m_SelectedProduct; }
            set { m_SelectedProduct = value; }
        }
        #endregion
        public W_PickProductForOrder()
        {
            InitializeComponent();
            LoadData();
            dgProducts.ItemsSource = BLL.Product.getProductList(CreateWhereString(), 0, pageSize, "", 0).Tables[1].DefaultView;
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

        private string CreateWhereString()
        {
            string strWhere = "";
            if (cmbCategory.SelectedIndex != 0)
            {
                strWhere += "and LU.Category=" + Int32.Parse(cmbCategory.SelectedValue.ToString()) + " ";
            }
            if (txtNumberOfTurnsFirst.Text != "" && txtNumberOfTurnsSecond.Text == "")
            {
                strWhere += "and LU.NumberOfTurns>=" + Int32.Parse(txtNumberOfTurnsFirst.Text) + " ";
            }
            else if (txtNumberOfTurnsFirst.Text == "" && txtNumberOfTurnsSecond.Text != "")
            {
                strWhere += "and LU.NumberOfTurns<=" + Int32.Parse(txtNumberOfTurnsSecond.Text) + " ";
            }
            else if (txtNumberOfTurnsFirst.Text != "" && txtNumberOfTurnsSecond.Text != "")
            {
                strWhere += "and LU.NumberOfTurns between " + Int32.Parse(txtNumberOfTurnsFirst.Text) + " and " + Int32.Parse(txtNumberOfTurnsSecond.Text) + " ";
            }


            if (txtNumberOfFormFirst.Text != "" && txtNumberOfFormSecond.Text == "")
            {
                strWhere += "and LU.NumberOfForm>=" + Int32.Parse(txtNumberOfFormFirst.Text) + " ";
            }
            else if (txtNumberOfFormFirst.Text == "" && txtNumberOfFormSecond.Text != "")
            {
                strWhere += "and LU.NumberOfForm<=" + Int32.Parse(txtNumberOfFormSecond.Text) + " ";
            }
            else if (txtNumberOfFormFirst.Text != "" && txtNumberOfFormSecond.Text != "")
            {
                strWhere += "and LU.NumberOfForm between " + Int32.Parse(txtNumberOfFormFirst.Text) + " and " + Int32.Parse(txtNumberOfFormSecond.Text) + " ";
            }
            if (txtCode.Text != "")
            {
                strWhere += "and LU.Code like '%" + txtCode.Text + "%' ";
            }
            if (txtExtraKod.Text != "")
            {
                strWhere += "and (LU.ExtraCode1 like '%" + txtExtraKod.Text + "%' or LU.ExtraCode2 like '%" + txtExtraKod.Text + "%' or LU.ExtraCode3 like '%" + txtExtraKod.Text + "%' or LU.ExtraCode4 like '%" + txtExtraKod.Text + "%') ";
            }

            if (txtWireDiameterFirst.Text != "" && txtWireDiameterSecond.Text == "")
            {
                strWhere += "and LU.WireDiameter>=" + Int32.Parse(txtWireDiameterFirst.Text) + " ";
            }
            else if (txtWireDiameterFirst.Text == "" && txtWireDiameterSecond.Text != "")
            {
                strWhere += "and LU.WireDiameter<=" + Int32.Parse(txtWireDiameterSecond.Text) + " ";
            }
            else if (txtWireDiameterFirst.Text != "" && txtWireDiameterSecond.Text != "")
            {
                strWhere += "and LU.WireDiameter between " + Int32.Parse(txtWireDiameterFirst.Text) + " and " + Int32.Parse(txtWireDiameterSecond.Text) + " ";
            }
            if (txtStraighteningLengthFirst.Text != "" && txtStraighteningLengthSecond.Text == "")
            {
                strWhere += "and LU.StraighteningLength>=" + Int32.Parse(txtStraighteningLengthFirst.Text) + " ";
            }
            else if (txtStraighteningLengthFirst.Text == "" && txtStraighteningLengthSecond.Text != "")
            {
                strWhere += "and LU.StraighteningLength<=" + Int32.Parse(txtStraighteningLengthSecond.Text) + " ";
            }
            else if (txtStraighteningLengthFirst.Text != "" && txtStraighteningLengthSecond.Text != "")
            {
                strWhere += "and LU.StraighteningLength between " + Int32.Parse(txtStraighteningLengthFirst.Text) + " and " + Int32.Parse(txtStraighteningLengthSecond.Text) + " ";
            }

            if (txtWeightFirst.Text != "" && txtWeightSecond.Text == "")
            {
                strWhere += "and LU.Weight>=" + Int32.Parse(txtWeightFirst.Text) + " ";
            }
            else if (txtWeightFirst.Text == "" && txtWeightSecond.Text != "")
            {
                strWhere += "and LU.Weight<=" + Int32.Parse(txtWeightSecond.Text) + " ";
            }
            else if (txtWeightFirst.Text != "" && txtWeightSecond.Text != "")
            {
                strWhere += "and LU.Weight between " + Int32.Parse(txtWeightFirst.Text) + " and " + Int32.Parse(txtWeightSecond.Text) + " ";
            }
            if (cmbState.SelectedValue.ToString() != "0")
            {
                strWhere += "and LU.State=" + cmbState.SelectedValue + "";
            }
            this.totalRecordCount = BLL.Product.getProductList(strWhere, 0, int.MaxValue - 1, "", 0).Tables[1].Rows.Count;

            return strWhere;
        }

        private void LoadData()
        {

            DataTable dtCategories = BLL.Category.GetProductCategoryListForDDL();
            cmbCategory.ItemsSource = dtCategories.DefaultView;
            cmbCategory.DisplayMemberPath = dtCategories.Columns["CAName"].ToString();
            cmbCategory.SelectedValuePath = dtCategories.Columns["CAID"].ToString();
            cmbCategory.SelectedIndex = 0;

            DataTable dtState = BLL.Utility.GetStateListForDDL();
            cmbState.ItemsSource = dtState.DefaultView;
            cmbState.DisplayMemberPath = dtState.Columns["SName"].ToString();
            cmbState.SelectedValuePath = dtState.Columns["SID"].ToString();
            cmbState.SelectedIndex = 0;

        }

        #endregion

        #region Events

        #region KeyDowns
        private void KeyDown_OnlyNumeric(object sender, KeyEventArgs e)
        {
            AllowOnlyNumeric(e);
        }
        #endregion

        #region Click
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            DataSet ds = BLL.Product.getProductList(CreateWhereString(), 0, pageSize, "", 0);
            dgProducts.ItemsSource =ds.Tables[1].DefaultView;
            lblRecordCount.Content = ds.Tables[1].Rows.Count;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.SelectedProduct = new BLL.Product(Int32.Parse((sender as Button).CommandParameter.ToString()), BLL.Enumarations.State.Aktif);
            this.Close();
        }
        #endregion

        #region TextChanged
        private void txtCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataSet ds=BLL.Product.getProductList(CreateWhereString(), 0, pageSize, "", 0);
            dgProducts.ItemsSource = ds.Tables[1].DefaultView;
            lblRecordCount.Content = ds.Tables[1].Rows.Count;
        }
        #endregion


        #endregion

        #region Paging Related Part
        private void btn1Forward_Click(object sender, RoutedEventArgs e)
        {
            int page = Int32.Parse(txtPage.Text);
            txtPage.Text = (page + 1).ToString();
            dgProducts.ItemsSource = BLL.Product.getProductList(CreateWhereString(), Int32.Parse(txtPage.Text), pageSize, "", 0).Tables[1].DefaultView;
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
            dgProducts.ItemsSource = BLL.Product.getProductList(CreateWhereString(), Int32.Parse(txtPage.Text), pageSize, "", 0).Tables[1].DefaultView;
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
            dgProducts.ItemsSource = BLL.Product.getProductList(CreateWhereString(), 0, pageSize, "", 0).Tables[1].DefaultView;
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
                dgProducts.ItemsSource = BLL.Product.getProductList(CreateWhereString(), Int32.Parse(txtPage.Text), pageSize, "", 0).Tables[1].DefaultView;
                btnAllForward.IsEnabled = false;
                btn1Forward.IsEnabled = false;
                btnAlPrevious.IsEnabled = true;
                btn1Previous.IsEnabled = true;
            }
        }

        #endregion




    }
}
