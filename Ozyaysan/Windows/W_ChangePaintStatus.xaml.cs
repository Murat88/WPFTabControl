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
    /// Interaction logic for W_ChangePaintStatus.xaml
    /// </summary>
    public partial class W_ChangePaintStatus : Window
    {
        DataTable dtDetailedOrder;
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
        private int m_Status;

        public int Status
        {
            get { return m_Status; }
            set { m_Status = value; }
        }
        public W_ChangePaintStatus(int OID, int PID, int oldStatus)
        {
            InitializeComponent();
            this.OID = OID;
            this.PID = PID;
            this.Status = oldStatus;
            dtDetailedOrder = BLL.Order.GetOrderDetailListByOrderID(this.OID);
            LoadDetail();
        }

        private void LoadDetail()
        {
            switch (this.Status)
            {
                case 2:
                    cmbOPStatus.SelectedIndex = 0;
                    cmbOPStatus.IsEnabled = false;
                    btnSearch.IsEnabled = false;
                    break;
                case 3:
                    cmbOPStatus.SelectedIndex = 1;
                    break;
                case 4:
                    cmbOPStatus.SelectedIndex = 2;
                    break;
                case 5:
                    cmbOPStatus.SelectedIndex = 3;
                    break;
                case 6:
                    cmbOPStatus.SelectedIndex = 4;
                    break;
                default:
                    break;
            }
            foreach (DataRow item in dtDetailedOrder.Rows)
            {
                if (item["PID"].ToString() == this.PID.ToString())
                {
                    BLL.Color oColor = new BLL.Color(Int32.Parse(item["Color"].ToString()), BLL.Enumarations.State.Aktif);
                    if (oColor.Name == null)
                    {
                        txtColor.Text = "Boya Yok";
                    }
                    else
                    {
                        txtColor.Text = oColor.Name;
                    }

                    txtOemCode.Text = new BLL.Product(this.PID, BLL.Enumarations.State.Aktif).Code;
                    txtPaintCount.Text = item["Count"].ToString();
                    txtStockCount.Text = item["UsedStockAmount"].ToString();
                    txtProductedCount.Text = item["ProductedAmount"].ToString();
                    txtPaintedCount.Text = item["PaintedAmount"].ToString();
                    txtDeliveredCount.Text = item["DeliveredAmount"].ToString();
                    var bc = new BrushConverter();
                    if (oColor.RGBCode != null)
                    {
                        brdRGBColor.Background = (Brush)bc.ConvertFrom(oColor.RGBCode);
                    }
                    else
                    {
                        brdRGBColor.Background = (Brush)bc.ConvertFrom("#FFF");
                    }

                }
            }


        }
        private string ValidationMethot()
        {
            string ErrorMessage = "";
            if (txtPaintedCount.Text.Trim()=="")
            {
               ErrorMessage += "Boyanan adet alanı boş bırakılamaz !" + Environment.NewLine + "";
            }
            if (stkDelivered.Visibility==Visibility.Visible)
            {
                if (txtDeliveredCount.Text.Trim() == "")
                {
                    ErrorMessage += "Teslim edilen adet alanı boş bırakılamaz !" + Environment.NewLine + "";
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
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string strVal = ValidationMethot();
            if (strVal == "")
            {
                int Result = BLL.Order.ChangeOrderProductPaintStatus(this.m_OID, this.m_PID, Int32.Parse((cmbOPStatus.SelectedItem as ComboBoxItem).Tag.ToString()), Int32.Parse(txtPaintedCount.Text), Int32.Parse(txtDeliveredCount.Text));
                if (Result == 0)
                {
                    dtDetailedOrder = BLL.Order.GetOrderDetailListByOrderID(this.OID);
                    UpdateGeneralOrderStatus(Convert.ToInt32((cmbOPStatus.SelectedItem as ComboBoxItem).Tag));
                }
                else
                {
                    MessageBox.Show("Kayıt işlemi sırasında bir hata oluştu");
                }
            }
        }
        private bool IsExistSmallerStatus(int OS)
        {
            foreach (DataRow oRow in dtDetailedOrder.Rows)
            {
                if (Int32.Parse(oRow["OPStatus"].ToString()) < OS)
                {
                    return true;
                }
            }

            return false;
        }
        private void UpdateGeneralOrderStatus(int Status)
        {
            BLL.Order oOrder = new BLL.Order(this.OID, BLL.Enumarations.State.Aktif);
            if (dtDetailedOrder.Rows.Count == 1)
            {

                oOrder.OrderStatus = (BLL.Enumarations.OrderStatus)Status;
                if (oOrder.Save() == 0)
                {
                    MessageBox.Show("İşlem başarılı");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Kayıt işlemi sırasında bir hata oluştu");
                }
            }
            else if (dtDetailedOrder.Rows.Count > 1)
            {
                switch (Status)
                {
                    case 3://boyahane
                        if (!IsExistSmallerStatus((int)BLL.Enumarations.OrderStatus.Boyahanede))
                        {
                            oOrder.OrderStatus = BLL.Enumarations.OrderStatus.Boyahanede;
                        }
                        break;
                    case 4://boyanıyor
                        if (!IsExistSmallerStatus((int)BLL.Enumarations.OrderStatus.Boyanıyor))
                        {
                            oOrder.OrderStatus = BLL.Enumarations.OrderStatus.Boyanıyor;
                        }
                        break;
                    case 5://hazır
                        if (!IsExistSmallerStatus((int)BLL.Enumarations.OrderStatus.Hazir))
                        {
                            oOrder.OrderStatus = BLL.Enumarations.OrderStatus.Hazir;
                        }
                        break;
                    case 6://gönderildi
                        if (!IsExistSmallerStatus((int)BLL.Enumarations.OrderStatus.Gonderildi))
                        {
                            oOrder.OrderStatus = BLL.Enumarations.OrderStatus.Gonderildi;
                        }
                        break;
                    default:
                        break;
                }
                if (oOrder.Save() == 0)
                {
                    MessageBox.Show("İşlem başarılı");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Kayıt işlemi sırasında bir hata oluştu");
                }
            }

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnExamine_Click(object sender, RoutedEventArgs e)
        {
            var window = new W_ExamineProduct(this.PID)
            {
                Title = "Yay İnceleme Formu",
                ShowInTaskbar = false,
                Topmost = true,
                ResizeMode = ResizeMode.CanMinimize,
                Owner = Application.Current.MainWindow

            };
            if (window.ShowDialog() == false)
            {

            }
        }

        private void txtPaintedCount_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            AllowOnlyNumeric(e);
        }

        private void cmbOPStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((cmbOPStatus.SelectedItem as ComboBoxItem).Tag.ToString()=="6")
            {
                stkDelivered.Visibility = Visibility.Visible;
            }
            else
            {
                stkDelivered.Visibility = Visibility.Hidden;
            }
        }
    }
}
