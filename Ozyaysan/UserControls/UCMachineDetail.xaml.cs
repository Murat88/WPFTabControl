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
    /// Interaction logic for UCMachineDetail.xaml
    /// </summary>
    public partial class UCMachineDetail : UserControl
    {
        #region Constructer

        #endregion
        public UCMachineDetail(int MID)
        {
            InitializeComponent();
            this.MID = MID;
            this.oMachine = new BLL.Machine(this.MID, BLL.Enumarations.State.Aktif);
            LoadData();
            LoadDetail();
        }
        #region CustomMethots
        private string ValidationMethot()
        {
            string ErrorMessage = "";

            if (txtMachineCode.Text.Trim() == "")
            {
                ErrorMessage += "Makine kodu alanı boş bırakılamaz !" + Environment.NewLine + "";
            }
            return ErrorMessage;
        }
        private void LoadDetail()
        {
            txtMachineCode.Text = this.oMachine.Name;          
            cmbState.SelectedValue = (int)this.oMachine.State;
         
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
        int m_MID;
        BLL.Machine oMachine;
        #endregion

        #region Properties
        public int MID
        {
            get { return m_MID; }
            set { m_MID = value; }
        }
        #endregion

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string strVal = ValidationMethot();
            if (strVal == "")
            {
                this.oMachine.Name = txtMachineCode.Text;

                int Result = -1;
                Result = this.oMachine.Save();
                if (Result == 0)
                {
                    MessageBox.Show("Makine bilgisi başarılı bir şekilde güncellendi");
                }
                else
                {
                    MessageBox.Show("Makine bilgisi güncellenirken hata oluştu");
                }
            }
            else
            {
                MessageBox.Show(strVal);
            }
        }

    }
}
