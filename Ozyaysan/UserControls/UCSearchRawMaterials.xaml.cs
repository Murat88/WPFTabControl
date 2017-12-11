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
using System.Collections.ObjectModel;

namespace Ozyaysan.UserControls
{
    /// <summary>
    /// Interaction logic for UCSearchRawMaterials.xaml
    /// </summary>
    public partial class UCSearchRawMaterials : UserControl
    {

        private int totalRecordCount = 0;
        private int pageSize = 8;
        DataSet ds;
        BLL.ViewModel model;
        public UCSearchRawMaterials()
        {
            InitializeComponent();
            LoadData();

            model = new BLL.ViewModel();
            ds = BLL.RawMaterials.getRawMaterialsList(CreateWhereString(), 0, pageSize, "", 0);
            BindList<BLL.ViewModel>(ds.Tables[1]);                    


            //dgRawMaterials.ItemsSource = ds.Tables[1].DefaultView;
            this.totalRecordCount = BLL.RawMaterials.getRawMaterialsList(CreateWhereString(), 0, int.MaxValue - 1, "", 0).Tables[1].Rows.Count;
            btn1Previous.IsEnabled = false;
            btnAlPrevious.IsEnabled = false;
            lblRecordCount.Content = this.totalRecordCount.ToString();
            if (BLL.RawMaterials.getRawMaterialsList(CreateWhereString(), 0, int.MaxValue - 1, "", 0).Tables[1].Rows.Count < pageSize)
            {
                btn1Forward.IsEnabled = false;
                btnAllForward.IsEnabled = false;
            }
            decimal totalAmount = 0;
            //foreach (DataRow item in ds.Tables[1].Rows)
            //{
            //    totalAmount += Decimal.Parse(item["Amount"].ToString());
            //}
            lblTotalWeight.Content = totalAmount.ToString();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

            ds = BLL.RawMaterials.getRawMaterialsList(CreateWhereString(), 0, pageSize, "", 0);
            BindList<BLL.ViewModel>(ds.Tables[1]);     
            lblRecordCount.Content = ds.Tables[1].Rows.Count;
            decimal totalAmount = 0;
            //foreach (DataRow item in ds.Tables[1].Rows)
            //{
            //    totalAmount += Decimal.Parse(item["Amount"].ToString());
            //}
            lblTotalWeight.Content = totalAmount.ToString();
        }

        private string CreateWhereString()
        {
            string strWhere = "";

            if (txtCompound.Text != "")
            {
                strWhere += "and LU.Compound like '%" + txtCompound.Text + "%'";
            }
            if (txtRegistry.Text != "")
            {
                strWhere += "and LU.RegistryNo like '%" + txtRegistry.Text + "%'";
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
            //------------- Sertlik kaldırıldı
            //if (txtHardness1.Text != "" && txtHardness2.Text == "")
            //{
            //    strWhere += "and LU.Hardness>=" + Int32.Parse(txtHardness1.Text) + " ";
            //}
            //else if (txtHardness1.Text == "" && txtHardness2.Text != "")
            //{
            //    strWhere += "and LU.Hardness<=" + Int32.Parse(txtHardness2.Text) + " ";
            //}
            //else if (txtHardness1.Text != "" && txtHardness2.Text != "")
            //{
            //    strWhere += "and LU.Hardness between " + Int32.Parse(txtHardness1.Text) + " and " + Int32.Parse(txtHardness2.Text) + " ";
            //}
            //----------- Durum Kaldırıldı
            //if (cmbState.SelectedValue.ToString()!="0")
            //{
            //     strWhere += "and LU.State=" + cmbState.SelectedValue + "";
            //}
            this.totalRecordCount = BLL.RawMaterials.getRawMaterialsList(strWhere, 0, int.MaxValue - 1, "", 0).Tables[1].Rows.Count;

            return strWhere;
        }

        private void LoadData()
        {
            //Durum Kaldırıldı
            //DataTable dtState = BLL.Utility.GetStateListForDDL();
            //cmbState.ItemsSource = dtState.DefaultView;
            //cmbState.DisplayMemberPath = dtState.Columns["SName"].ToString();
            //cmbState.SelectedValuePath = dtState.Columns["SID"].ToString();
            //cmbState.SelectedIndex = 0;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).CommandParameter != null)
            {
                (((Start)((Grid)(((TabControl)(((TabItem)((Grid)((Grid)(this.Parent)).Parent).Parent).Parent)).Parent)).Parent)).GrdContentSearchRawMaterials.Visibility = Visibility.Hidden;
                (((Start)((Grid)(((TabControl)(((TabItem)((Grid)((Grid)(this.Parent)).Parent).Parent).Parent)).Parent)).Parent)).GrdContentAddRawMaterials.Visibility = Visibility.Hidden;
                (((Start)((Grid)(((TabControl)(((TabItem)((Grid)((Grid)(this.Parent)).Parent).Parent).Parent)).Parent)).Parent)).GrdContentRawMaterialsDetail.Visibility = Visibility.Visible;
                Ozyaysan.UserControls.UCRawMaterialsDetail oWindowsRawMaterialsDetail = new UserControls.UCRawMaterialsDetail(Int32.Parse((sender as Button).CommandParameter.ToString()));
                (((Start)((Grid)(((TabControl)(((TabItem)((Grid)((Grid)(this.Parent)).Parent).Parent).Parent)).Parent)).Parent)).GrdContentRawMaterialsDetail.Children.Clear();
                (((Start)((Grid)(((TabControl)(((TabItem)((Grid)((Grid)(this.Parent)).Parent).Parent).Parent)).Parent)).Parent)).GrdContentRawMaterialsDetail.Children.Add(oWindowsRawMaterialsDetail);
            }
        }

        #region Custom Methots
        public void BindList<T>(DataTable dt)
        {
            model.Collection.Clear();
            var fields = typeof(T).GetProperties();
            foreach (DataRow dr in dt.Rows)
            {
                BLL.ViewModel rm = new BLL.ViewModel();
                foreach (var fieldInfo in fields)
                {
                    foreach (DataColumn dc in dt.Columns)
                    {
                        if (fieldInfo.Name == dc.ColumnName)
                        {
                            object value = dr[dc.ColumnName];
                            if (String.IsNullOrEmpty(value.ToString()))
                            {
                                value = String.Empty;
                            }
                            fieldInfo.SetValue(rm, value, null);
                            break;
                        }
                    }
                }
                model.Collection.Add(rm);
            }
            model.ViewSource.GroupDescriptions.Clear();
            model.ViewSource.GroupDescriptions.Add(new PropertyGroupDescription("RegistryNo"));
            model.ViewSource.View.Refresh();        
            dgRawMaterials.ItemsSource = model.ViewSource.View;
        }
        #endregion
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
    public class GroupsToTotalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is ReadOnlyObservableCollection<Object>)
            {               
                var items = (ReadOnlyObservableCollection<Object>)value;
                Decimal total = 0;
                foreach (BLL.ViewModel gi in items)
                {
                    total += gi.CurrentInventory;
                }
                return total.ToString();
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

}
