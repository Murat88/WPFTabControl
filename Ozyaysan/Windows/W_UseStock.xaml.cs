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
    /// Interaction logic for W_UseStock.xaml
    /// </summary>
    public partial class W_UseStock : Window
    {
        private bool m_IsSuccesfulOperation = false;

        public bool IsSuccesfulOperation
        {
            get { return m_IsSuccesfulOperation; }
            set { m_IsSuccesfulOperation = value; }
        }
        private bool m_IsUsed;

        public bool IsUsed
        {
            get { return m_IsUsed; }
            set { m_IsUsed = value; }
        }
        private int m_OID;

        public int OID
        {
            get { return m_OID; }
            set { m_OID = value; }
        }
        private int m_PID;

        public int PID
        {
            get { return m_PID; }
            set { m_PID = value; }
        }
        private int m_TotalCount;

        public int TotalCount
        {
            get { return m_TotalCount; }
            set { m_TotalCount = value; }
        }

        public W_UseStock(int PID, int TotalCount, int OID, bool IsUsed)
        {
            InitializeComponent();
            this.PID = PID;
            this.OID = OID;
            this.IsUsed = IsUsed;
            this.TotalCount = TotalCount;
            LoadDetail();
        }

        private void LoadDetail()
        {
            BLL.Product oProduct = new BLL.Product(this.PID, BLL.Enumarations.State.Aktif);
           // lblStock.Content = oProduct.Stock; // kontrol edilecek
            lblStotalCount.Content = this.TotalCount;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string strVal = ValidationMethot();
            if (strVal == "")
            {
                int p_IsUsed = 0;
                if (this.IsUsed)
                {
                    p_IsUsed = 1;
                }
                else
                {
                    p_IsUsed = 0;
                }
                int nResult = BLL.Order.UpdateOrderDetailForStock(this.OID, this.PID, p_IsUsed, Int32.Parse(txtUseOfStock.Text));
                if (nResult == 0)
                {
                    MessageBox.Show("İşlem başarılı");
                    this.IsSuccesfulOperation = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Bir hata oluştu");
                }
            }
            else
            {
                MessageBox.Show(strVal);
            }
        }
        private string ValidationMethot()
        {
            string ErrorMessage = "";
            if (txtUseOfStock.Text.Trim() == "")
            {
                ErrorMessage += "Kullanılacak miktar alanı boş bırakılamaz!" + Environment.NewLine + "";
            }
            else
            {               

                if (Int32.Parse(txtUseOfStock.Text) > Int32.Parse(lblStock.Content.ToString()))
                {
                    ErrorMessage += "Kullanılacak miktar stoktaki miktardan fazla olamaz!" + Environment.NewLine + "";
                }

                if (Int32.Parse(txtUseOfStock.Text) > Int32.Parse(lblStotalCount.Content.ToString()))
                {
                    ErrorMessage += "Kullanılacak miktar toplam sipariş miktarından fazla olamaz!" + Environment.NewLine + "";
                }
            }
            return ErrorMessage;
        }
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



        private void txtUseOfStock_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            AllowOnlyNumeric(e);
        }


    }
}
