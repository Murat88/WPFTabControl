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

namespace Ozyaysan.UserControls
{
    /// <summary>
    /// Interaction logic for UCAddColor.xaml
    /// </summary>
    public partial class UCAddColor : UserControl
    {

        public UCAddColor()
        {
            InitializeComponent();
            this.CID = CID;
            LoadData();
        }       
     
        #region Fields
        int m_CID=0;       
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
                 BLL.Color oColor = new BLL.Color();
                 if (this.CID == 0)
                 {
                     oColor.ID = 0;
                 }
                 else
                 {
                     oColor.ID = CID;
                 }

                 oColor.Name = txtColorName.Text;
                 oColor.RGBCode = txtRGBCode.Text; // kontrol yapilmali
                 oColor.State = BLL.Enumarations.State.Aktif;

                 int nResult = oColor.Save();
                 if (nResult == 0)
                 {
                     MessageBox.Show("Renk başarılı bir şekilde kaydedildi");
                 }
             }
             else
             {
                 MessageBox.Show(strVal);
             }
        }
       
        #region Custom Methots
        private void LoadDDLs()
        {

            DataTable dtState = BLL.Utility.GetStateListForDDLWithoutChose();
            cmbState.ItemsSource = dtState.DefaultView;
            cmbState.DisplayMemberPath = dtState.Columns["SName"].ToString();
            cmbState.SelectedValuePath = dtState.Columns["SID"].ToString();
            cmbState.SelectedIndex = 1;

        }
        private void LoadData()
        {
            LoadDDLs();
        }
          private string ValidationMethot()
        {
            string ErrorMessage = "";
            
            if ( txtColorName.Text.Trim() == "")
            {
                ErrorMessage += "Renk kodu alanı boş bırakılamaz !" + Environment.NewLine + "";
            }
            return ErrorMessage;
        }
        #endregion
    }
}
