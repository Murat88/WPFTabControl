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
    /// Interaction logic for W_UseRawMaterials.xaml
    /// </summary>
    public partial class W_UseRawMaterials : Window
    {
        BLL.RawMaterials oPickedRawMaterial = null;
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
        public W_UseRawMaterials(int OID, int PID)
        {
            this.OID = OID;
            this.PID = PID;
            InitializeComponent();
            LoadDetail();
        }

        private void LoadDetail()
        {

            DataTable dt=BLL.Order.GetOrderDetailListByOrderID(this.OID);

            foreach (DataRow item in dt.Rows)
            {
                if (item["PID"].ToString()==this.PID.ToString())
                {
                    if (item["IsRawMaterialsUsed"].ToString()=="1")
                    {
                        this.oPickedRawMaterial = new BLL.RawMaterials(Int32.Parse(item["RawMaterialID"].ToString()), BLL.Enumarations.State.Aktif);
                        FillControls();
                        txtUsedAmount.Text = item["UsedRawMaterialsAmount"].ToString();
                        tblRawMaterialsUsed.Visibility = Visibility.Visible;
                        btnSave.Visibility = Visibility.Collapsed;
                        txtUsedAmount.IsEnabled = false;
                        btnCancel.Visibility = Visibility.Visible;
                        btnPickRawMaterial.IsEnabled = false;
                    }
                    else
                    {
                        btnSave.Visibility = Visibility.Visible;
                        btnCancel.Visibility = Visibility.Collapsed;
                        btnPickRawMaterial.IsEnabled = true;
                    }
                }
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

        private void ClearControls()
        {
            oPickedRawMaterial = null;
            txtCompound.Text = "";
            txtHardness.Text = "";
            txtQuality.Text = "";
            txtSurfaceCondition.Text = "";
            txtWireDiameter.Text = "";
            
        }
        private void FillControls()
        {
            txtAmount.Text = this.oPickedRawMaterial.Amount.ToString();
            txtCompound.Text = this.oPickedRawMaterial.Compound;
            txtHardness.Text = this.oPickedRawMaterial.Hardness.ToString();
            txtWireDiameter.Text = this.oPickedRawMaterial.WireDiameter.ToString();
            txtQuality.Text = this.oPickedRawMaterial.Quality;
            txtSurfaceCondition.Text = this.oPickedRawMaterial.SurfaceCondition;
        }

          private string ValidationMethot()
        {
            string ErrorMessage = "";          

            if (txtUsedAmount.Text.Trim() == "")
            {
                ErrorMessage += "Kullanılacak Hammadde Miktarı alanı boş bırakılamaz !" + Environment.NewLine + "";
            }
            if (txtUsedAmount.Text.Trim() != "")
            {
                if (Int32.Parse(txtUsedAmount.Text)>Int32.Parse(txtAmount.Text))
                {
                   ErrorMessage += "Kullanılacak hammadde miktarı toplam hammadde miktarından fazla olamaz !" + Environment.NewLine + "";
                }
            }
            if (this.oPickedRawMaterial==null)
            {
                ErrorMessage += "Lütfen hammadde seçimi yapınız !" + Environment.NewLine + "";

            }
            return ErrorMessage;
        }

        #endregion
          #region Events

              #region Click
                  private void btnPickRawMaterial_Click(object sender, RoutedEventArgs e)
                  {                     
                          var window = new W_PickRawMaterialsForOrder()
                          {
                              Title = "Hammadde Seç",
                              ShowInTaskbar = false,
                              Topmost = true,
                              ResizeMode = ResizeMode.CanMinimize,
                              Owner = Application.Current.MainWindow,
                          };
                          if (window.ShowDialog() == false)
                          {
                              if (window.SelectedRawMaterials.ID!=0)
                              {
                                  this.oPickedRawMaterial = window.SelectedRawMaterials;
                                  FillControls();
                              }
                              
                          }                      
                  }
                  private void btnSave_Click(object sender, RoutedEventArgs e)
                  {
                      string strval = ValidationMethot();
                      if (strval == "")
                      {
                          this.oPickedRawMaterial.Amount -= Int32.Parse(txtUsedAmount.Text);
                          if (oPickedRawMaterial.Save() == 0)
                          {
                              if (BLL.Order.UpdateOrderDetailForRawMaterials(this.OID, this.PID, 1, Int32.Parse(txtUsedAmount.Text.ToString()), this.oPickedRawMaterial.ID) == 0)
                              {
                                  MessageBox.Show("İşlem Başarılı");
                                  txtUsedAmount.IsEnabled = false;
                                  btnCancel.Visibility = Visibility.Visible;
                                  btnSave.Visibility = Visibility.Collapsed;
                                  tblRawMaterialsUsed.Visibility = Visibility.Visible;
                                  txtAmount.Text = oPickedRawMaterial.Amount.ToString();
                                  btnPickRawMaterial.IsEnabled = false;
                              }

                          }
                      }
                      else
                      {
                          MessageBox.Show(strval);
                      }
                  }
                  private void btnClose_Click(object sender, RoutedEventArgs e)
                  {
                      this.Close();
                  }
                  private void btnCancel_Click(object sender, RoutedEventArgs e)
                  {
                      this.oPickedRawMaterial.Amount += Int32.Parse(txtUsedAmount.Text);
                      if (oPickedRawMaterial.Save() == 0)
                      {
                          if (BLL.Order.UpdateOrderDetailForRawMaterials(this.OID, this.PID, 0, 0, this.oPickedRawMaterial.ID) == 0)
                          {
                              MessageBox.Show("İşlem Başarılı");
                              btnSave.Visibility = Visibility.Visible;
                              btnCancel.Visibility = Visibility.Collapsed;
                              txtUsedAmount.IsEnabled = true;
                              tblRawMaterialsUsed.Visibility = Visibility.Hidden;
                              txtAmount.Text = oPickedRawMaterial.Amount.ToString();
                              txtUsedAmount.Text = "0";
                              btnPickRawMaterial.IsEnabled = true;
                              ClearControls();
                          }

                      }
                  }

              #endregion

              #region KeyDowns
         private void KeyDown_OnlyNumeric(object sender, KeyEventArgs e)
        {
            AllowOnlyNumeric(e);
        }
        #endregion

         
        #endregion

        

    }
}
