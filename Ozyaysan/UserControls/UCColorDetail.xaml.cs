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
    /// Interaction logic for UCColorDetail.xaml
    /// </summary>
    public partial class UCColorDetail : UserControl
    {
        #region Constructer

        #endregion
        public UCColorDetail(int CID)
        {
            InitializeComponent();
            this.CID = CID;
            this.oColor = new BLL.Color(this.CID, BLL.Enumarations.State.Aktif);
            LoadData();
            LoadDetail();
        }
        #region CustomMethots
        private string ValidationMethot()
        {
            string ErrorMessage = "";

            if (txtColorName.Text.Trim() == "")
            {
                ErrorMessage += "Renk kodu alanı boş bırakılamaz !" + Environment.NewLine + "";
            }
            return ErrorMessage;
        }
        private void LoadDetail()
        {
            txtColorName.Text = this.oColor.Name;
            cmbState.SelectedValue = (int)this.oColor.State;
            var bc = new BrushConverter();
            brdRGBColor.Background = (Brush)bc.ConvertFrom(this.oColor.RGBCode);
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
        int m_CID;
        BLL.Color oColor;
        #endregion

        #region Properties
        public int CID
        {
            get { return m_CID; }
            set { m_CID = value; }
        }
        #endregion

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
             string strVal = ValidationMethot();
             if (strVal == "")
             {
                 this.oColor.Name = txtColorName.Text;
                 int Result = -1;
                 Result = this.oColor.Save();
                 if (Result == 0)
                 {
                     MessageBox.Show("Renk bilgisi başarılı bir şekilde güncellendi");
                 }
                 else
                 {
                     MessageBox.Show("Renk bilgisi güncellenirken hata oluştu");
                 }
             }
             else
             {
                 MessageBox.Show(strVal);
             }
        }


    }
}
