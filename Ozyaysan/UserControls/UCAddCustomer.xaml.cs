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
    /// Interaction logic for UCAddCustomer.xaml
    /// </summary>
    public partial class UCAddCustomer : UserControl
    {
        public UCAddCustomer()
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

        private void LoadData()
        {
            LoadDDLs();            
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        { 
            string strVal = ValidationMethot();
            if (strVal == "")
            {
                BLL.Customer oCustomer = new BLL.Customer();
                if (this.CID == 0)
                {
                    oCustomer.ID = 0;
                }
                else
                {
                    oCustomer.ID = CID;
                }

                oCustomer.Name = txtCustomerName.Text;
                oCustomer.Country = txtCountry.Text;
                oCustomer.City = txtCity.Text;
                oCustomer.Email = txtEmail.Text;
                oCustomer.Address = txtAddress.Text;
                oCustomer.State = BLL.Enumarations.State.Aktif;

                int nResult = oCustomer.Save();
                if (nResult == 0)
                {
                    MessageBox.Show("Müşteri başarılı bir şekilde kaydedildi");
                }
            }
            else
            {
                MessageBox.Show(strVal);
            }
        }
        private void LoadDDLs()
        {           

            DataTable dtState = BLL.Utility.GetStateListForDDLWithoutChose();
            cmbState.ItemsSource = dtState.DefaultView;
            cmbState.DisplayMemberPath = dtState.Columns["SName"].ToString();
            cmbState.SelectedValuePath = dtState.Columns["SID"].ToString();
            cmbState.SelectedIndex = 1;

        }

        #region Custom Methots
            private string ValidationMethot()
        {
            string ErrorMessage = "";
            if ( txtCustomerName.Text.Trim() == "")
            {
                ErrorMessage += "Müşteri adı alanı boş bırakılamaz !" + Environment.NewLine + "";
            }
          
            return ErrorMessage;
        }
        #endregion
    }
}
