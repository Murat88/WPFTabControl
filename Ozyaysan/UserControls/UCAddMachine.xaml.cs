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
    /// Interaction logic for UCAddMachine.xaml
    /// </summary>
    public partial class UCAddMachine : UserControl
    {

        public UCAddMachine()
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
                 BLL.Machine oMachine = new BLL.Machine();
                 if (this.MID == 0)
                 {
                     oMachine.ID = 0;
                 }
                 else
                 {
                     oMachine.ID = MID;
                 }

                 oMachine.Name = txtMachineCode.Text;
                 oMachine.State = BLL.Enumarations.State.Aktif;

                 int nResult = oMachine.Save();
                 if (nResult == 0)
                 {
                     MessageBox.Show("Makine başarılı bir şekilde kaydedildi");
                 }
                 else
                 {
                     MessageBox.Show("Makine ekleme sırasında bir hata oluştu.");
                 }
             }
             else
             {
                 MessageBox.Show(strVal);
             }
        }
        #region Custom Methots
        private string ValidationMethot()
        {
            string ErrorMessage = "";

            if (txtMachineCode.Text.Trim() == "")
            {
                ErrorMessage += "Makine kodu alanı boş bırakılamaz !" + Environment.NewLine + "";
            }
            return ErrorMessage;
        }
        private void LoadDDLs()
        {

            DataTable dtState = BLL.Utility.GetStateListForDDLWithoutChose();
            cmbState.ItemsSource = dtState.DefaultView;
            cmbState.DisplayMemberPath = dtState.Columns["SName"].ToString();
            cmbState.SelectedValuePath = dtState.Columns["SID"].ToString();
            cmbState.SelectedIndex = 1;

        }
        #endregion

    }
}
