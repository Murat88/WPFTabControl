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

namespace Ozyaysan.Windows
{
    /// <summary>
    /// Interaction logic for W_ExamineProduct.xaml
    /// </summary>
    public partial class W_ExamineProduct : Window
    {
        public W_ExamineProduct(int PID)
        {
            InitializeComponent();
            this.Content = new Ozyaysan.UserControls.UCProductDetail(PID,true);
        }
    }
}
