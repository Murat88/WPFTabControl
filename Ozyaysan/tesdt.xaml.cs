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

namespace Ozyaysan
{
    /// <summary>
    /// Interaction logic for tesdt.xaml
    /// </summary>
    public partial class tesdt : Window
    {
        public tesdt()
        {
            InitializeComponent();
        }

        private void imgProduct_MouseDown(object sender, MouseButtonEventArgs e)
        {
              MenuMotion(frmIcoProduct,e);
              MakeFramesUnVisibleExcludingProduct();
        }
        #region Frame'sVisibility
         private void MakeFramesUnVisibleExcludingProduct()
        {           
            CCCustomer.Visibility = Visibility.Hidden;
            CCOrder.Visibility = Visibility.Hidden;     
            CCProduct.Visibility = Visibility.Visible;
        }
         private void MakeFramesUnVisibleExcludingCustomer()
         {
            CCProduct.Visibility = Visibility.Hidden;
            CCOrder.Visibility = Visibility.Hidden;
            CCCustomer.Visibility = Visibility.Visible;
         }
         private void MakeFramesUnVisibleExcludingOrder()
         {
            CCCustomer.Visibility = Visibility.Hidden;
            CCProduct.Visibility = Visibility.Hidden;
            CCOrder.Visibility = Visibility.Visible;
         }
        #endregion
       

        private void ChangeOtherFrameBGC(Frame frm)
        {
            var bc = new BrushConverter();
            frmIcoProduct.Background = (Brush)bc.ConvertFrom("#FF38577A");
            frmIcoCustomer.Background = (Brush)bc.ConvertFrom("#FF38577A");
            frmIcoOrder.Background = (Brush)bc.ConvertFrom("#FF38577A");
            frm.Background = (Brush)bc.ConvertFrom("#FFE8EBED");
        }
        private void MakeFramesUnVisible(Frame frm)
        {           
            frmIcoProduct.Visibility = Visibility.Hidden;
            frmIcoCustomer.Visibility = Visibility.Hidden;
            frmIcoOrder.Visibility = Visibility.Hidden;
            frm.Visibility = Visibility.Visible;
        }
        private void imgCustomer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MenuMotion(frmIcoCustomer,e);
            MakeFramesUnVisibleExcludingCustomer();
        }

        private void MenuMotion(Frame frm,MouseButtonEventArgs e)
        {
            
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                ChangeOtherFrameBGC(frm);
            }
        }

        private void imgIcoOrder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MenuMotion(frmIcoOrder, e);
            MakeFramesUnVisibleExcludingOrder();
        }

    }
}
