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
    /// Interaction logic for UCOperatorDetail.xaml
    /// </summary>
    public partial class UCOperatorDetail : UserControl
    {

        #region Constructor
        public UCOperatorDetail(int CID)
        {
            InitializeComponent();
            this.CID = CID;
            this.oOperator = new BLL.Operator(this.CID, BLL.Enumarations.State.Aktif);
            LoadData();
            LoadDetail();
        }
        #endregion

        #region CustomMethots
        private void LoadDetail()
        {
            txtOPName.Text = this.oOperator.Name;
            txtOPLastName.Text = this.oOperator.LastName;
            cmbState.SelectedValue = (int)this.oOperator.State;

        }

        private string ValidationMethot()
        {
            string ErrorMessage = "";

            if (txtOPName.Text.Trim() == "")
            {
                ErrorMessage += "Operatör adı alanı boş bırakılamaz !" + Environment.NewLine + "";
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
        int m_CID;
        BLL.Operator oOperator;
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
                this.oOperator.Name = txtOPName.Text;
                this.oOperator.LastName = txtOPLastName.Text;
                int Result = -1;
                Result = this.oOperator.Save();
                if (Result == 0)
                {
                    MessageBox.Show("Operatör bilgisi başarılı bir şekilde güncellendi");
                }
                else
                {
                    MessageBox.Show("Operatör bilgisi güncellenirken hata oluştu");
                }
            }
            else
            {
                MessageBox.Show(strVal);
            }
        }

    }
}
