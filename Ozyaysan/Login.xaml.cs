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
using Ozyaysan.Windows;
using Microsoft.Win32;
using System.IO;
using System.Collections;
using System.Configuration;

namespace Ozyaysan
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //DataSet ds = BLL.User.VerifyPassword(txtUserName.Text, PwdLogin.Password);
            //if (ds != null)
            //{
            //    DataTable dt = ds.Tables[0];
            //    if (dt.Rows.Count == 1)
            //    {
            //        if (dt.Rows[0]["VerifyState"].ToString() == "1")
            //        {
            //            switch (Convert.ToInt32(dt.Rows[0]["UTID"]))
            //            {
            //                case ((int)(BLL.Enumarations.UserType.Owner)):
            //                    ApplicationState.SetValue("User", new BLL.User(Convert.ToInt32(dt.Rows[0]["UID"]), BLL.Enumarations.State.Aktif));
            //                    Start oStart = new Start();
            //                    oStart.Show();
            //                    Application.Current.MainWindow = oStart;
            //                    this.Close();
            //                    break;
            //                case ((int)(BLL.Enumarations.UserType.Master)):
            //                    ApplicationState.SetValue("User", new BLL.User(Convert.ToInt32(dt.Rows[0]["UID"]), BLL.Enumarations.State.Aktif));
            //                    MasterStart oStartM = new MasterStart();
            //                    oStartM.Show();
            //                    Application.Current.MainWindow = oStartM;
            //                    this.Close();
            //                    break;
            //                case ((int)(BLL.Enumarations.UserType.Painter)):
            //                    ApplicationState.SetValue("User", new BLL.User(Convert.ToInt32(dt.Rows[0]["UID"]), BLL.Enumarations.State.Aktif));
            //                    PainterStart oStartP = new PainterStart();
            //                    oStartP.Show();
            //                    Application.Current.MainWindow = oStartP;
            //                    this.Close();
            //                    break;
            //                default:
            //                    break;
            //            }
            //        }
            //        else
            //        {
            //            MessageBox.Show("Kullanıcı adı veya şifre hatalı !!");
            //        }
            //    }
            //}
            if (txtUserName.Text == "Muhasebe")
            {
                
              
                ApplicationState.SetValue("User", new BLL.User(1,BLL.Enumarations.State.Aktif));
                Start oStart = new Start();
                Application.Current.MainWindow = oStart;
                oStart.Show();
                this.Close();
            }
            else if (txtUserName.Text == "Usta")
            {
                ApplicationState.SetValue("User", new BLL.User(2, BLL.Enumarations.State.Aktif));
                MasterStart oStart = new MasterStart();                
                Application.Current.MainWindow = oStart;
                oStart.Show();
                this.Close();
            }
            else if (txtUserName.Text == "Boyahane")
            {
                ApplicationState.SetValue("User", new BLL.User(3, BLL.Enumarations.State.Aktif));
                PainterStart oStart = new PainterStart();
                Application.Current.MainWindow = oStart;
                oStart.Show();
                this.Close();
            }

        }
    }
}
