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
    /// Interaction logic for UCAddOperator.xaml
    /// </summary>
    public partial class UCAddOperator : UserControl
    {

        public UCAddOperator()
        {
            InitializeComponent();
            this.OPID = OPID;
            LoadData();
        }

        #region Fields
        int m_OPID = 0;
        #endregion

        #region Properties
        public int OPID
        {
            get { return m_OPID; }
            set { m_OPID = value; }
        }
        #endregion

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
             string strVal = ValidationMethot();
             if (strVal == "")
             {
                 BLL.Operator oOperator = new BLL.Operator();
                 if (this.OPID == 0)
                 {
                     oOperator.ID = 0;
                 }
                 else
                 {
                     oOperator.ID = OPID;
                 }

                 oOperator.Name = txtOperatorName.Text;
                 oOperator.LastName = txtOPLastName.Text; // kontrol yapilmali
                 oOperator.State = BLL.Enumarations.State.Aktif;

                 int nResult = oOperator.Save();
                 if (nResult == 0)
                 {
                     MessageBox.Show("Operatör başarılı bir şekilde kaydedildi");
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

            if (txtOperatorName.Text.Trim() == "")
            {
                ErrorMessage += "Operatör adı alanı boş bırakılamaz !" + Environment.NewLine + "";
            }
            return ErrorMessage;
        }
        private void LoadData()
        {
            LoadDDLs();
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
