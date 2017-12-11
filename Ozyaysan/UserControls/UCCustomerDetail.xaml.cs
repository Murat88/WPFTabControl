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
    /// Interaction logic for UCCustomerDetail.xaml
    /// </summary>
    public partial class UCCustomerDetail : UserControl
    {

        #region Constructer

        #endregion
        public UCCustomerDetail(int CID)
        {
            InitializeComponent();
            this.CID = CID;
            this.oCustomer = new BLL.Customer(this.CID, BLL.Enumarations.State.Aktif);
            LoadData();
            LoadDetail();
        }
        #region CustomMethots
        private void LoadDetail()
        {
            txtCustomerName.Text = this.oCustomer.Name;
            cmbState.SelectedValue = (int)this.oCustomer.State;
            txtCountry.Text = this.oCustomer.Country;
            txtCity.Text = this.oCustomer.City;
            txtEmail.Text = this.oCustomer.Email;
            txtAddress.Text = this.oCustomer.Address;
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
        BLL.Customer oCustomer;
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
                this.oCustomer.Name = txtCustomerName.Text;
                this.oCustomer.Country = txtCountry.Text;
                this.oCustomer.City = txtCity.Text;
                this.oCustomer.Email = txtEmail.Text;
                this.oCustomer.Address = txtAddress.Text;
                this.oCustomer.State =  (BLL.Enumarations.State)(Int32.Parse(cmbState.SelectedValue.ToString()));
                int Result = -1;
                Result = this.oCustomer.Save();
                if (Result == 0)
                {
                    MessageBox.Show("Müşteri bilgisi başarılı bir şekilde güncellendi");
                }
                else
                {
                    MessageBox.Show("Müşteri bilgisi güncellenirken hata oluştu");
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
            if (txtCustomerName.Text.Trim() == "")
            {
                ErrorMessage += "Müşteri adı alanı boş bırakılamaz !" + Environment.NewLine + "";
            }

            return ErrorMessage;
        }
        #endregion

    }
}
