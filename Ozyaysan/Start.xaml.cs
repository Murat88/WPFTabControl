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
    public partial class Start : Window
    {
        public Start()
        {
            InitializeComponent();
            BLL.User oCurrentUser = (ApplicationState.GetValue<object>("user") as BLL.User);
            tblLoginName.Text = oCurrentUser.Name;
            /*Yay arama sayfası açılsın*/
            GrdContentSearchProduct.Visibility = Visibility.Visible;
            GrdContentAddProduct.Visibility = Visibility.Hidden;
            GrdContentProductDetail.Visibility = Visibility.Hidden;
            Ozyaysan.UserControls.UCSearchProduct oWinbdowsSearcProduct = new UserControls.UCSearchProduct();
            GrdContentSearchProduct.Children.Clear();
            GrdContentSearchProduct.Children.Add(oWinbdowsSearcProduct);
            ManageLabels(lblSearchProduct);

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lblNewProduct_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GrdContentSearchProduct.Visibility = Visibility.Hidden;
            GrdContentAddProduct.Visibility = Visibility.Visible;
            GrdContentProductDetail.Visibility = Visibility.Hidden;
            UCAddProduct oWindowsAddProduct = new UCAddProduct();
            GrdContentAddProduct.Children.Clear();
          //  oWindowsAddProduct.Margin = margin;
            GrdContentAddProduct.Children.Add(oWindowsAddProduct);
            ManageLabels(sender);
        }

        private void ProductSearch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                GrdContentSearchProduct.Visibility = Visibility.Visible;
                GrdContentAddProduct.Visibility = Visibility.Hidden;
                GrdContentProductDetail.Visibility = Visibility.Hidden;
                Ozyaysan.UserControls.UCSearchProduct oWinbdowsSearcProduct = new UserControls.UCSearchProduct();
                GrdContentSearchProduct.Children.Clear();
                GrdContentSearchProduct.Children.Add(oWinbdowsSearcProduct);
                ManageLabels(sender);
            }
        }

        private void CustomerSearch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                GrdContentSearchCustomer.Visibility = Visibility.Visible;
                GrdContentAddCustomer.Visibility = Visibility.Hidden;
                GrdContentCustomerDetail.Visibility = Visibility.Hidden;
                Ozyaysan.UserControls.UCSearchCustomer oWindowSearchCustomer = new UserControls.UCSearchCustomer();
                GrdContentSearchCustomer.Children.Clear();
                GrdContentSearchCustomer.Children.Add(oWindowSearchCustomer);
                ManageLabels(sender);
            }
        }

        private void CustomerAdd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                GrdContentSearchCustomer.Visibility = Visibility.Hidden;
                GrdContentAddCustomer.Visibility = Visibility.Visible;
                GrdContentCustomerDetail.Visibility = Visibility.Hidden;
                Ozyaysan.UserControls.UCAddCustomer oWindowSearchCustomer = new UserControls.UCAddCustomer();
                GrdContentAddCustomer.Children.Clear();
                GrdContentAddCustomer.Children.Add(oWindowSearchCustomer);
                ManageLabels(sender);
            }
        }
        private void RawMaterialsAdd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                GrdContentSearchRawMaterials.Visibility = Visibility.Hidden;
                GrdContentAddRawMaterials.Visibility = Visibility.Visible;
                GrdContentRawMaterialsDetail.Visibility = Visibility.Hidden;
                Ozyaysan.UserControls.UCAddRawMaterials oWindowSearchRawMaterials = new UserControls.UCAddRawMaterials();
                GrdContentAddRawMaterials.Children.Clear();
                GrdContentAddRawMaterials.Children.Add(oWindowSearchRawMaterials);
                ManageLabels(sender);
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
                ManageLabels(sender);
            }
        }

        private void ColorAdd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                GrdContentSearchColor.Visibility = Visibility.Hidden;
                GrdContentAddColor.Visibility = Visibility.Visible;
                GrdContentColorDetail.Visibility = Visibility.Hidden;
                Ozyaysan.UserControls.UCAddColor oWindowAddColor = new UserControls.UCAddColor();
                GrdContentAddColor.Children.Clear();
                GrdContentAddColor.Children.Add(oWindowAddColor);
                ManageLabels(sender);
            }
        }

        private void OperatorAdd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                GrdContentSearchOperator.Visibility = Visibility.Hidden;
                GrdContentAddOperator.Visibility = Visibility.Visible;
                GrdContentOperatorDetail.Visibility = Visibility.Hidden;
                Ozyaysan.UserControls.UCAddOperator oWindowAddOperator = new UserControls.UCAddOperator();
                GrdContentAddOperator.Children.Clear();
                GrdContentAddOperator.Children.Add(oWindowAddOperator);
                ManageLabels(sender);
            }
        }

        private void MachineAdd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                GrdContentSearchMachine.Visibility = Visibility.Hidden;
                GrdContentAddMachine.Visibility = Visibility.Visible;
                GrdContentMachineDetail.Visibility = Visibility.Hidden;
                Ozyaysan.UserControls.UCAddMachine oWindowAddMachine = new UserControls.UCAddMachine();
                GrdContentAddMachine.Children.Clear();
                GrdContentAddMachine.Children.Add(oWindowAddMachine);
                ManageLabels(sender);
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
                ManageLabels(sender);
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
                ManageLabels(sender);
            }
        }
        private void RawMaterialsSearch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                GrdContentSearchRawMaterials.Visibility = Visibility.Visible;
                GrdContentAddRawMaterials.Visibility = Visibility.Hidden;
                GrdContentRawMaterialsDetail.Visibility = Visibility.Hidden;
                Ozyaysan.UserControls.UCSearchRawMaterials oSearchRawMaterials = new UserControls.UCSearchRawMaterials();
                GrdContentSearchRawMaterials.Children.Clear();
                GrdContentSearchRawMaterials.Children.Add(oSearchRawMaterials);
                ManageLabels(sender);
            }
        }
        private void SellSearch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                GrdContentSearchSell.Visibility = Visibility.Visible;              
                GrdContentSellDetail.Visibility = Visibility.Hidden;
                Ozyaysan.UserControls.UCSearchSell oSearchSell = new UserControls.UCSearchSell();
                GrdContentSearchSell.Children.Clear();
                GrdContentSearchSell.Children.Add(oSearchSell);
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
                ManageLabels(sender);
            }
        }
        private void ColorSearch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                GrdContentSearchColor.Visibility = Visibility.Visible;
                GrdContentAddColor.Visibility = Visibility.Hidden;
                GrdContentColorDetail.Visibility = Visibility.Hidden;
                Ozyaysan.UserControls.UCSearchColor oSearchColor = new UserControls.UCSearchColor();
                GrdContentSearchColor.Children.Clear();
                GrdContentSearchColor.Children.Add(oSearchColor);
                ManageLabels(sender);
            }
        }

        private void OperatorSearch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                GrdContentSearchOperator.Visibility = Visibility.Visible;
                GrdContentAddOperator.Visibility = Visibility.Hidden;
                GrdContentOperatorDetail.Visibility = Visibility.Hidden;
                Ozyaysan.UserControls.UCSearchOperator oSearchOperator = new UserControls.UCSearchOperator();
                GrdContentSearchOperator.Children.Clear();
                GrdContentSearchOperator.Children.Add(oSearchOperator);
                ManageLabels(sender);
            }
        }

        private void MachineSearch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                GrdContentSearchMachine.Visibility = Visibility.Visible;
                GrdContentAddMachine.Visibility = Visibility.Hidden;
                GrdContentMachineDetail.Visibility = Visibility.Hidden;
                Ozyaysan.UserControls.UCSearchMachine oSearchMachine = new UserControls.UCSearchMachine();
                GrdContentSearchMachine.Children.Clear();
                GrdContentSearchMachine.Children.Add(oSearchMachine);
                ManageLabels(sender);
            }
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
                ManageLabels(sender);
            }
        }

        private void Image_ImageFailed_1(object sender, ExceptionRoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

      

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            #region Test
            //switch (TabControlMain.SelectedIndex)
            //{
            //    case 0:
            //        GrdContentSearchProduct.Visibility = Visibility.Visible;
            //        GrdContentAddProduct.Visibility = Visibility.Hidden;
            //        GrdContentProductDetail.Visibility = Visibility.Hidden;
            //        Ozyaysan.UserControls.UCSearchProduct oWinbdowsSearcProduct = new UserControls.UCSearchProduct();
            //        GrdContentSearchProduct.Children.Clear();
            //        GrdContentSearchProduct.Children.Add(oWinbdowsSearcProduct);
            //        break;
            //    case 1:
            //        GrdContentSearchCustomer.Visibility = Visibility.Visible;
            //        GrdContentAddCustomer.Visibility = Visibility.Hidden;
            //        GrdContentCustomerDetail.Visibility = Visibility.Hidden;
            //        Ozyaysan.UserControls.UCSearchCustomer oWindowSearchCustomer = new UserControls.UCSearchCustomer();
            //        GrdContentSearchCustomer.Children.Clear();
            //        GrdContentSearchCustomer.Children.Add(oWindowSearchCustomer);
            //        break;
            //    case 2:
            //        GrdContentSearchOrder.Visibility = Visibility.Visible;
            //        GrdContentAddOrder.Visibility = Visibility.Hidden;
            //        GrdContentOrderDetail.Visibility = Visibility.Hidden;
            //        Ozyaysan.UserControls.UCSearchOrder oSearchOrder = new UserControls.UCSearchOrder();
            //        GrdContentSearchOrder.Children.Clear();
            //        GrdContentSearchOrder.Children.Add(oSearchOrder);
            //        break;
            //    case 3:
            //        GrdContentSearchColor.Visibility = Visibility.Visible;
            //        GrdContentAddColor.Visibility = Visibility.Hidden;
            //        GrdContentColorDetail.Visibility = Visibility.Hidden;
            //        Ozyaysan.UserControls.UCSearchColor oSearchColor = new UserControls.UCSearchColor();
            //        GrdContentSearchColor.Children.Clear();
            //        GrdContentSearchColor.Children.Add(oSearchColor);
            //        break;
            //    case 4:
            //        GrdContentSearchOperator.Visibility = Visibility.Visible;
            //        GrdContentAddOperator.Visibility = Visibility.Hidden;
            //        GrdContentOperatorDetail.Visibility = Visibility.Hidden;
            //        Ozyaysan.UserControls.UCSearchOperator oSearchOperator = new UserControls.UCSearchOperator();
            //        GrdContentSearchOperator.Children.Clear();
            //        GrdContentSearchOperator.Children.Add(oSearchOperator);
            //        break;
            //    case 5:
            //        GrdContentSearchMachine.Visibility = Visibility.Visible;
            //        GrdContentAddMachine.Visibility = Visibility.Hidden;
            //        GrdContentMachineDetail.Visibility = Visibility.Hidden;
            //        Ozyaysan.UserControls.UCSearchMachine oSearchMachine = new UserControls.UCSearchMachine();
            //        GrdContentSearchMachine.Children.Clear();
            //        GrdContentSearchMachine.Children.Add(oSearchMachine);
            //        break;
            //    default:
            //        break;
            //}
          
            #endregion          
        }

        private void ManageLabels(object label)
        {
            lblSearchProduct.FontWeight = FontWeights.Normal;
            lblSearchProduct.Foreground = Brushes.Black;

            lblAddCustomer.FontWeight = FontWeights.Normal;
            lblAddCustomer.Foreground = Brushes.Black;

            lblChangePass.FontWeight = FontWeights.Normal;
            lblChangePass.Foreground = Brushes.Black;

            lblExamineStock.FontWeight = FontWeights.Normal;
            lblExamineStock.Foreground = Brushes.Black;

            lblNewColor.FontWeight = FontWeights.Normal;
            lblNewColor.Foreground = Brushes.Black;

            lblNewMachine.FontWeight = FontWeights.Normal;
            lblNewMachine.Foreground = Brushes.Black;

            lblNewOperator.FontWeight = FontWeights.Normal;
            lblNewOperator.Foreground = Brushes.Black;

            lblNewOrder.FontWeight = FontWeights.Normal;
            lblNewOrder.Foreground = Brushes.Black;

            lblNewProduct.FontWeight = FontWeights.Normal;
            lblNewProduct.Foreground = Brushes.Black;

            lblRawMaterialEnterance.FontWeight = FontWeights.Normal;
            lblRawMaterialEnterance.Foreground = Brushes.Black;

            lblSearchColor.FontWeight = FontWeights.Normal;
            lblSearchColor.Foreground = Brushes.Black;

            lblSearchCustomer.FontWeight = FontWeights.Normal;
            lblSearchCustomer.Foreground = Brushes.Black;

            lblSearchMachine.FontWeight = FontWeights.Normal;
            lblSearchMachine.Foreground = Brushes.Black;

            lblSearchOperator.FontWeight = FontWeights.Normal;
            lblSearchOperator.Foreground = Brushes.Black;

            lblSearchOrder.FontWeight = FontWeights.Normal;
            lblSearchOrder.Foreground = Brushes.Black;

            lblSearchProduct.FontWeight = FontWeights.Normal;
            lblSearchProduct.Foreground = Brushes.Black;

            lblSearchrawMaterials.FontWeight = FontWeights.Normal;
            lblSearchrawMaterials.Foreground = Brushes.Black;

            lblStockEnterance.FontWeight = FontWeights.Normal;
            lblStockEnterance.Foreground = Brushes.Black;

            ((Label)label).FontWeight = FontWeights.Bold;
            ((Label)label).Foreground = Brushes.Brown;
        }

    }
}
