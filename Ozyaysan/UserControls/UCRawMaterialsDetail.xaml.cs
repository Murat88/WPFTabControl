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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BLL = OzyaysanBusinessEngine;
using System.Data;

namespace Ozyaysan.UserControls
{
    /// <summary>
    /// Interaction logic for UCRawMaterialsDetail.xaml
    /// </summary>
    public partial class UCRawMaterialsDetail : UserControl
    {
       

        #region Constructor
        public UCRawMaterialsDetail(int Id)
        {
            InitializeComponent();
            this.Id = Id;
            this.oRawMaterials = new BLL.RawMaterials(this.Id, BLL.Enumarations.State.Aktif);
            LoadData();
            LoadDetail();
        }
        #endregion

        #region CustomMethots
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
        private void LoadDetail()
        {
            txtWireDiameter.Text = this.oRawMaterials.WireDiameter.ToString();
            txtHardness.Text = this.oRawMaterials.Hardness.ToString();
            txtCompound.Text = this.oRawMaterials.Compound;
            txtAmount.Text = this.oRawMaterials.Amount.ToString();           
            cmbState.SelectedValue = (int)this.oRawMaterials.State;
            txtQuality.Text = this.oRawMaterials.Quality;
            txtSurfaceCondition.Text = this.oRawMaterials.SurfaceCondition;

        }

        private string ValidationMethot()
        {
            string ErrorMessage = "";

            if (txtWireDiameter.Text.Trim() == "")
            {
                ErrorMessage += "Tel çapı alanı boş bırakılamaz !" + Environment.NewLine + "";
            }
            if (txtHardness.Text.Trim() == "")
            {
                ErrorMessage += "Sertlik alanı boş bırakılamaz !" + Environment.NewLine + "";
            }
            if (txtQuality.Text.Trim() == "")
            {
                ErrorMessage += "Kalite alanı boş bırakılamaz !" + Environment.NewLine + "";
            }
            if (txtSurfaceCondition.Text.Trim() == "")
            {
                ErrorMessage += "Yüzey durumu alanı boş bırakılamaz !" + Environment.NewLine + "";
            }
            if (txtAmount.Text.Trim() == "")
            {
                ErrorMessage += "Miktar alanı boş bırakılamaz !" + Environment.NewLine + "";
            }
            return ErrorMessage;
        }
        private void LoadData()
        {
            DataTable dtState = BLL.Utility.GetStateListForDDLWithoutChose();
            cmbState.ItemsSource = dtState.DefaultView;
            cmbState.DisplayMemberPath = dtState.Columns["SName"].ToString();
            cmbState.SelectedValuePath = dtState.Columns["SID"].ToString();
            cmbState.SelectedIndex = 1;
        }
        #endregion


        #region Fields
        int m_Id;
        BLL.RawMaterials oRawMaterials;
        #endregion

        #region Properties
        public int Id
        {
            get { return m_Id; }
            set { m_Id = value; }
        }
        #endregion

        #region Events
        #region KeyDowns
         private void KeyDown_OnlyNumeric(object sender, KeyEventArgs e)
        {
            AllowOnlyNumeric(e);
        }
        #endregion
        #endregion

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string strVal = ValidationMethot();
            if (strVal == "")
            {
                this.oRawMaterials.WireDiameter = Int32.Parse(txtWireDiameter.Text);
                this.oRawMaterials.Hardness = Int32.Parse(txtHardness.Text);
                this.oRawMaterials.Compound = txtCompound.Text;
                this.oRawMaterials.Amount = Int32.Parse(txtAmount.Text);
                this.oRawMaterials.Quality = txtQuality.Text;
                this.oRawMaterials.SurfaceCondition = txtSurfaceCondition.Text;
                oRawMaterials.State = (BLL.Enumarations.State)cmbState.SelectedValue;
                int Result = -1;
                Result = this.oRawMaterials.Save();
                if (Result == 0)
                {
                    MessageBox.Show("Hammadde bilgisi başarılı bir şekilde güncellendi");
                }
                else
                {
                    MessageBox.Show("Hammadde bilgisi güncellenirken hata oluştu");
                }
            }
            else
            {
                MessageBox.Show(strVal);
            }
        }

       

    }
}
