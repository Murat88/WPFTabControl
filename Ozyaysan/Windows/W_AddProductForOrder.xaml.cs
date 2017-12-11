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
using System.Data;
using BLL = OzyaysanBusinessEngine;
namespace Ozyaysan.Windows
{
    /// <summary>
    /// Interaction logic for W_AddProductForOrder.xaml
    /// </summary>
    public partial class W_AddProductForOrder : Window
    {
      
        #region fields
        private BLL.Product m_SelectedProduct;
        private BLL.Product m_AddedProduct;
        BLL.Stock NonColoredStock, ColoredStock;
        #endregion

        #region Properties
        public BLL.Product AddedProduct
        {
            get { return m_AddedProduct; }
        }
        #endregion

        public W_AddProductForOrder()
        {
            InitializeComponent();
            LoadData();
        }
        public W_AddProductForOrder(BLL.Product _oproduct)
        {
            InitializeComponent();
            LoadData();
            m_SelectedProduct = _oproduct;
            LoadDetail();
            cmbProduct.SelectionChanged += CmbProduct_SelectionChanged;
            cmbColor.SelectionChanged+= CmbColor_SelectionChanged;
        }

        

        private void LoadDetail()
        {
            cmbProduct.SelectedValue =   m_SelectedProduct.ID;
            cmbColor.SelectedValue   =   m_SelectedProduct.CColor.ID;
            txtStockColoredUse.Text  =   m_SelectedProduct.UsedStockCountColored.ToString();           
            txtProductionCount.Text  =   m_SelectedProduct.ProductionCount.ToString();
            txtOrderCount.Text       =   m_SelectedProduct.OrderCount.ToString();
            txtPackageType.Text      =   m_SelectedProduct.CurrentPackageType;
            NonColoredStock          =   new BLL.Stock(m_SelectedProduct.ID);

            if (m_SelectedProduct.CColor.ID!=0)
            {
                ColoredStock = new BLL.Stock(m_SelectedProduct.ID, m_SelectedProduct.CColor.ID);
                txtStockColored.Text = String.IsNullOrEmpty(ColoredStock.Count.ToString()) ? 0.ToString() : ColoredStock.Count.ToString();
            }  

            txtStockNonColored.Text = NonColoredStock.Count.ToString();
            txtStockNonColoredUse.Text= String.IsNullOrEmpty(m_SelectedProduct.UsedStockCountNonColored.ToString()) ? 0.ToString() : m_SelectedProduct.UsedStockCountNonColored.ToString();
        }
        #region Events
        #region KeyDowns
        private void KeyDown_OnlyNumeric(object sender, KeyEventArgs e)
        {
            AllowOnlyNumeric(e);
        }
        #endregion
        #endregion

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
         private void AllowOnlyNumericAndDecimal(KeyEventArgs e)
         {

             switch (e.Key)
             {
                 case Key.Decimal:
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
        #endregion
        private void lblDetailSearchForProduct_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
        private void LoadData()
        {
            DataTable dtProduct = BLL.Product.GetProductListForDDL();
            cmbProduct.ItemsSource = dtProduct.DefaultView;
            cmbProduct.DisplayMemberPath = dtProduct.Columns["PName"].ToString();
            cmbProduct.SelectedValuePath = dtProduct.Columns["PID"].ToString();
            cmbProduct.SelectedIndex = 0;

            DataTable dtColor = BLL.Color.GetColorListForDDL();
            cmbColor.ItemsSource = dtColor.DefaultView;
            cmbColor.DisplayMemberPath = dtColor.Columns["CName"].ToString();
            cmbColor.SelectedValuePath = dtColor.Columns["CID"].ToString();
            cmbColor.SelectedIndex = 0;
        }
        private void btnAddProductToOrder_Click(object sender, RoutedEventArgs e)
        {
            string strVal = ValidationMethot();
            if (strVal == "")
            {
                this.m_AddedProduct = new BLL.Product(Int32.Parse(cmbProduct.SelectedValue.ToString()), BLL.Enumarations.State.Aktif);
                this.m_AddedProduct.OrderCount = Int32.Parse(txtOrderCount.Text);
                this.m_AddedProduct.CColor = new BLL.Color(Int32.Parse(cmbColor.SelectedValue.ToString()),OzyaysanBusinessEngine.Enumarations.State.Aktif);
                this.m_AddedProduct.CurrentPackageType = txtPackageType.Text;
                this.m_AddedProduct.CurrentOPStatus = (int)BLL.Enumarations.OrderStatus.İmalataHazır;              
                this.m_AddedProduct.UsedStockCountColored = Int32.Parse((txtStockColoredUse.Text.Trim()=="")?0.ToString():txtStockColoredUse.Text.Trim());
                this.m_AddedProduct.UsedStockCountNonColored = Int32.Parse((txtStockNonColoredUse.Text.Trim() == "") ? 0.ToString() : txtStockNonColoredUse.Text.Trim()); 
                this.m_AddedProduct.ProductionCount= Int32.Parse((txtProductionCount.Text.Trim() == "") ? 0.ToString() : txtProductionCount.Text.Trim());
                this.Close();
            }
            else
            {
                MessageBox.Show(strVal);
            }
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

        private void CmbProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            m_SelectedProduct = new BLL.Product(Int32.Parse(cmbProduct.SelectedValue.ToString()), OzyaysanBusinessEngine.Enumarations.State.Aktif);
            if (cmbColor.SelectedValue.ToString()!="0")
            {
                ColoredStock = new BLL.Stock(m_SelectedProduct.ID, Int32.Parse(cmbColor.SelectedValue.ToString()));
                txtStockColored.Text = ColoredStock.Count.ToString();
            }
            NonColoredStock = new BLL.Stock(m_SelectedProduct.ID);           
            txtStockNonColored.Text = NonColoredStock.Count.ToString();
         
        }
        private void CmbColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ColoredStock = new BLL.Stock(m_SelectedProduct.ID,Int32.Parse(cmbColor.SelectedValue.ToString()));
            txtStockColored.Text = ColoredStock.Count.ToString();
        }
        private void imgColorSearch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var window = new W_PickColorForOrder()
                {
                    Title = "Boyanacak Rengi Seç",
                    ShowInTaskbar = false,
                    Topmost = true,
                    ResizeMode = ResizeMode.NoResize,
                    Owner = Application.Current.MainWindow,
                };
                if (window.ShowDialog() == false)
                {
                    BLL.Color SelectedColor = window.SelectedColor;
                    if (SelectedColor != null)
                    {
                        cmbColor.SelectedValue = SelectedColor.ID.ToString();
                    }
                }
            }
        }
        private string ValidationMethot()
        {
            string ErrorMessage = "";
            if (txtOrderCount.Text.Trim() == "")
            {
                ErrorMessage += "Adet alanı boş bırakılamaz !" + Environment.NewLine + "";
            }           
            if (cmbProduct.SelectedIndex == 0)
            {
                ErrorMessage += "Lütfen bir yay seçiniz !" + Environment.NewLine + "";
            }
            if (!String.IsNullOrEmpty(txtStockColoredUse.Text) && Int32.Parse(txtStockColoredUse.Text)!=0)
            {
                if (Int32.Parse(txtStockColored.Text)< Int32.Parse(txtStockColoredUse.Text))
                {
                    ErrorMessage += "Kullanılan stok miktarı toplam stok miktarından fazla olamaz !" + Environment.NewLine + "";
                }
            }
            if (!String.IsNullOrEmpty(txtStockNonColoredUse.Text) && Int32.Parse(txtStockNonColoredUse.Text) != 0)
            {
                if (Int32.Parse(txtStockNonColored.Text) < Int32.Parse(txtStockNonColoredUse.Text))
                {
                    ErrorMessage += "Kullanılan stok miktarı toplam stok miktarından fazla olamaz !" + Environment.NewLine + "";
                }
            }
            return ErrorMessage;
        }

        private void txtSellPrice_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            AllowOnlyNumericAndDecimal(e);
        }
       
    }
}
