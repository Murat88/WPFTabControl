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
    /// Interaction logic for UCChangePassword.xaml
    /// </summary>
    public partial class UCChangePassword : UserControl
    {
        #region Constructer

        #endregion
        public UCChangePassword()
        {
            InitializeComponent();
            oCurrentUser = (ApplicationState.GetValue<object>("user") as BLL.User);
        }
        #region CustomMethots
        private string ValidationMethot()
        {
            string ErrorMessage = "";
            if (oCurrentUser.Password!=pwdOldPass.Password)
            {
                ErrorMessage += "Mevcut şifre hatalı girildi !" + Environment.NewLine + "";
            }
            if (pwdNewPass1.Password=="")
            {
                ErrorMessage += "Yeni şifre alanı boş bırakılamaz !" + Environment.NewLine + "";
            }
            if (pwdNewPass2.Password == "")
            {
                ErrorMessage += "Yeni şifre tekrar alanı boş bırakılamaz !" + Environment.NewLine + "";
            }
            if (pwdNewPass1.Password!=pwdNewPass2.Password)
            {
                 ErrorMessage += "Şifreler uyuşmuyor !" + Environment.NewLine + "";
            }
            return ErrorMessage;
        }
     
       

        
        #endregion


        #region Fields
        BLL.User oCurrentUser;
       
        #endregion

        #region Properties
       
        #endregion

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string strVal = ValidationMethot();
            if (strVal == "")
            {
                oCurrentUser.Password = pwdNewPass1.Password;

                int Result = -1;
                Result = oCurrentUser.Save();
                if (Result == 0)
                {
                    MessageBox.Show("Şifre bilgisi başarılı bir şekilde güncellendi");
                }
                else
                {
                    MessageBox.Show("Şifre bilgisi güncellenirken hata oluştu");
                }
            }
            else
            {
                MessageBox.Show(strVal);
            }
        }
    }
}
