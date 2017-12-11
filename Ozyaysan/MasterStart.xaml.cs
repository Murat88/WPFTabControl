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
namespace Ozyaysan
{
    /// <summary>
    /// Interaction logic for test2.xaml
    /// </summary>
    public partial class MasterStart : Window
    {
        public MasterStart()
        {
            InitializeComponent();
            BLL.User oCurrentUser = (ApplicationState.GetValue<object>("user") as BLL.User);
            tblLoginName.Text = oCurrentUser.Name;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void StockSearch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                GrdContentSearchStock.Visibility = Visibility.Visible;
                GrdContentAddStock.Visibility = Visibility.Hidden;
                GrdContentStockDetail.Visibility = Visibility.Hidden;
                Ozyaysan.UserControls.UCSearchStock oSearchStock = new UserControls.UCSearchStock();
                GrdContentSearchStock.Children.Clear();
                GrdContentSearchStock.Children.Add(oSearchStock);
            }
        }
        private void StockAdd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                GrdContentSearchStock.Visibility = Visibility.Hidden;
                GrdContentAddStock.Visibility = Visibility.Visible;
                GrdContentStockDetail.Visibility = Visibility.Hidden;
                Ozyaysan.UserControls.UCAddStock oWindowAddStock = new UserControls.UCAddStock();
                GrdContentAddStock.Children.Clear();
                GrdContentAddStock.Children.Add(oWindowAddStock);
            }
        }
        private void ChangePassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                GrdContentChangePass.Visibility = Visibility.Visible;
                Ozyaysan.UserControls.UCChangePassword oPasswordChange = new UserControls.UCChangePassword();
                GrdContentChangePass.Children.Clear();
                GrdContentChangePass.Children.Add(oPasswordChange);
            }
        }
        private void OrderAdd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                GrdContentSearchOrder.Visibility = Visibility.Hidden;
                GrdContentAddOrder.Visibility = Visibility.Visible;
                GrdContentOrderDetail.Visibility = Visibility.Hidden;
                Ozyaysan.UserControls.UCAddOrder oWindowAddOrder = new UserControls.UCAddOrder();
                GrdContentAddOrder.Children.Clear();
                GrdContentAddOrder.Children.Add(oWindowAddOrder);
            }
        }

        private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

        private void OrderSearch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                GrdContentSearchOrder.Visibility = Visibility.Visible;
                GrdContentAddOrder.Visibility = Visibility.Hidden;
                GrdContentOrderDetail.Visibility = Visibility.Hidden;
                Ozyaysan.UserControls.UCSearchOrder oSearchOrder = new UserControls.UCSearchOrder();
                GrdContentSearchOrder.Children.Clear();
                GrdContentSearchOrder.Children.Add(oSearchOrder);
            }
        }
        

    }
}
