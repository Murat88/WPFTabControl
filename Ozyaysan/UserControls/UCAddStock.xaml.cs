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
    /// Interaction logic for UCAddStock.xaml
    /// </summary>
    public partial class UCAddStock : UserControl
    {


        public UCAddStock()
        {
            InitializeComponent();
            this.MID = MID;
            LoadData();
        }

        #region Fields
        int m_MID = 0;
        #endregion
        #region Properties
        public int MID
        {
            get { return m_MID; }
            set { m_MID = value; }
        }
        #endregion

        private void LoadData()
        {
            LoadDDLs();
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string strVal = ValidationMethot();
            if (strVal == "")
            {
                BLL.Product oProduct = new BLL.Product(Int32.Parse(cmbProduct.SelectedValue.ToString()), BLL.Enumarations.State.Aktif);
               // oProduct.Stock += Int32.Parse(txtAddedStockAmount.Text);
                if (oProduct.Save()==0)
                {
                    MessageBox.Show("İşlem başarılı");
                }
                else
                {
                    MessageBox.Show("Kayıt esnasında bir hata oluştu"); 
                }
            }
            else
            {
                MessageBox.Show(strVal);
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
        private string ValidationMethot()
        {
            string ErrorMessage = "";

            if (cmbProduct.SelectedIndex == 0)
            {
                ErrorMessage += "Lütfen bir yay seçiniz !" + Environment.NewLine + "";
            }
            if (txtAddedStockAmount.Text.Trim() == "")
            {
                ErrorMessage += "Eklenecek stok adeti alanı boş bırakılamaz !" + Environment.NewLine + "";
            }
            return ErrorMessage;
        }
        private void LoadDDLs()
        {
            DataTable dtProduct = BLL.Product.GetProductListForDDL();
            cmbProduct.ItemsSource = dtProduct.DefaultView;
            cmbProduct.DisplayMemberPath = dtProduct.Columns["PName"].ToString();
            cmbProduct.SelectedValuePath = dtProduct.Columns["PID"].ToString();
            cmbProduct.SelectedIndex = 0;
        }
        #endregion

        #region Events
        #region KeyDown       
        private void KeyDown_OnlyNumeric(object sender, KeyEventArgs e)
        {
            AllowOnlyNumeric(e);
        }
        #endregion

        #region MouseDown
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
        #endregion

       
       


        #endregion

        
    }
}
