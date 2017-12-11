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
using System.Data;
using BLL = OzyaysanBusinessEngine;
namespace Ozyaysan.Windows
{
    /// <summary>
    /// Interaction logic for W_AddProductionPhase.xaml
    /// </summary>
    public partial class W_AddProductionPhase : Window
    {
        #region Constructors

          public W_AddProductionPhase(int PID)
        {

            InitializeComponent();
            LoadData();
            this.PID = PID;

        }

        #endregion

        #region Fields
        int m_PID;
        #endregion

        #region Properties
        public int PID
        {
            get { return m_PID; }
            set { m_PID = value; }
        }
        #endregion

        #region Events

        #region txtKeyDowns
        private void txtProductionTime1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            AllowOnlyNumeric(e);
        }
        private void txtProductionTime2_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            AllowOnlyNumeric(e);
        }
        private void txtProductionTime3_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            AllowOnlyNumeric(e);
        }
        private void txtProductionTime4_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            AllowOnlyNumeric(e);
        }
        private void txtProductionTime5_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            AllowOnlyNumeric(e);
        }
        private void txtProductionTime6_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            AllowOnlyNumeric(e);
        }
        private void txtProductionTime7_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            AllowOnlyNumeric(e);
        }
        #endregion

        #region chcChecked

        private void chcPhase1_Checked(object sender, RoutedEventArgs e)
        {
            if (txtProductionTime1.Text == "")
            {
                lblWarning.Content = "1. aşama için üretim süresinin giriniz";
                chcPhase1.IsChecked = false;
            }
            if (cmbMachine1.SelectedIndex == 0)
            {
                lblWarning.Content = "1. aşama için makina seçiniz";
                chcPhase1.IsChecked = false;
            }
            if (txtProductionTime1.Text != "" && cmbMachine1.SelectedIndex != 0)
            {
                grdPhases2.IsEnabled = true;
                lblWarning.Content = "";
            }
        }

        private void chcPhase2_Checked(object sender, RoutedEventArgs e)
        {
            if (txtProductionTime2.Text == "")
            {
                lblWarning.Content = "2. aşama için üretim süresinin giriniz";
                chcPhase2.IsChecked = false;
            }
            if (cmbMachine2.SelectedIndex == 0)
            {
                lblWarning.Content = "2. aşama için makina seçiniz";
                chcPhase2.IsChecked = false;
            }
            if (txtProductionTime2.Text != "" && cmbMachine2.SelectedIndex != 0)
            {
                grdPhases3.IsEnabled = true;
                lblWarning.Content = "";
            }
        }

        private void chcPhase3_Checked_1(object sender, RoutedEventArgs e)
        {
            if (txtProductionTime3.Text == "")
            {
                lblWarning.Content = "3. aşama için üretim süresinin giriniz";
                chcPhase3.IsChecked = false;
            }
            if (cmbMachine3.SelectedIndex == 0)
            {
                lblWarning.Content = "3. aşama için makina seçiniz";
                chcPhase3.IsChecked = false;
            }
            if (txtProductionTime3.Text != "" && cmbMachine3.SelectedIndex != 0)
            {
                grdPhases4.IsEnabled = true;
                lblWarning.Content = "";
            }
        }

        private void chcPhase4_Checked(object sender, RoutedEventArgs e)
        {

            if (txtProductionTime4.Text == "")
            {
                lblWarning.Content = "4. aşama için üretim süresinin giriniz";
                chcPhase4.IsChecked = false;
            }
            if (cmbMachine4.SelectedIndex == 0)
            {
                lblWarning.Content = "4. aşama için makina seçiniz";
                chcPhase4.IsChecked = false;
            }
            if (txtProductionTime4.Text != "" && cmbMachine4.SelectedIndex != 0)
            {
                grdPhases5.IsEnabled = true;
                lblWarning.Content = "";
            }
        }

        private void chcPhase5_Checked(object sender, RoutedEventArgs e)
        {
            if (txtProductionTime5.Text == "")
            {
                lblWarning.Content = "5. aşama için üretim süresinin giriniz";
                chcPhase5.IsChecked = false;
            }
            if (cmbMachine5.SelectedIndex == 0)
            {
                lblWarning.Content = "5. aşama için makina seçiniz";
                chcPhase5.IsChecked = false;
            }
            if (txtProductionTime5.Text != "" && cmbMachine5.SelectedIndex != 0)
            {
                grdPhases6.IsEnabled = true;
                lblWarning.Content = "";
            }
        }

        private void chcPhase6_Checked(object sender, RoutedEventArgs e)
        {
            if (txtProductionTime6.Text == "")
            {
                lblWarning.Content = "6. aşama için üretim süresinin giriniz";
                chcPhase6.IsChecked = false;
            }
            if (cmbMachine6.SelectedIndex == 0)
            {
                lblWarning.Content = "6. aşama için makina seçiniz";
                chcPhase6.IsChecked = false;
            }
            if (txtProductionTime6.Text != "" && cmbMachine6.SelectedIndex != 0)
            {
                grdPhases7.IsEnabled = true;
                lblWarning.Content = "";
            }
        }

        private void chcPhase7_Checked(object sender, RoutedEventArgs e)
        {
            if (txtProductionTime7.Text == "")
            {
                lblWarning.Content = "7. aşama için üretim süresinin giriniz";
                chcPhase7.IsChecked = false;
            }
            if (cmbMachine7.SelectedIndex == 0)
            {
                lblWarning.Content = "7. aşama için makina seçiniz";
                chcPhase7.IsChecked = false;
            }
            if (txtProductionTime7.Text != "" && cmbMachine7.SelectedIndex != 0)
            {                
                lblWarning.Content = "";
            }

        }


        #endregion

        #region Click

          private void btnAddPhase_Click(object sender, RoutedEventArgs e)
        {
            int Result = 0;
            if (chcPhase1.IsChecked==true)
            {
                Result = BLL.Machine.SaveProductionPhase(this.PID, Int32.Parse(cmbMachine1.SelectedValue.ToString()), float.Parse(txtProductionTime1.Text));
            }
            if (chcPhase2.IsChecked == true)
            {
                Result = BLL.Machine.SaveProductionPhase(this.PID, Int32.Parse(cmbMachine2.SelectedValue.ToString()), float.Parse(txtProductionTime2.Text));
            }
            if (chcPhase3.IsChecked==true)
            {
                Result = BLL.Machine.SaveProductionPhase(this.PID, Int32.Parse(cmbMachine3.SelectedValue.ToString()), float.Parse(txtProductionTime3.Text));
            }
            if (chcPhase4.IsChecked == true)
            {
                Result = BLL.Machine.SaveProductionPhase(this.PID, Int32.Parse(cmbMachine4.SelectedValue.ToString()), float.Parse(txtProductionTime4.Text));
            }
            if (chcPhase5.IsChecked == true)
            {
                Result = BLL.Machine.SaveProductionPhase(this.PID, Int32.Parse(cmbMachine5.SelectedValue.ToString()), float.Parse(txtProductionTime5.Text));
            }
            if (chcPhase6.IsChecked == true)
            {
                Result = BLL.Machine.SaveProductionPhase(this.PID, Int32.Parse(cmbMachine6.SelectedValue.ToString()), float.Parse(txtProductionTime6.Text));
            }
            if (chcPhase7.IsChecked == true)
            {
                Result = BLL.Machine.SaveProductionPhase(this.PID, Int32.Parse(cmbMachine7.SelectedValue.ToString()), float.Parse(txtProductionTime7.Text));
            }
            if (Result != 0)
            {
                //Hata oluştu
            }
            else
            {
                MessageBox.Show("İşlem başarılı");
                this.Close();
            }

        }
        
        #endregion

        #endregion

        #region CustomMethots

          private void LoadData()
        {
            //Comboboxes
            DataTable dtMachine = BLL.Machine.GetMachineListForDDL();
            cmbMachine1.ItemsSource = dtMachine.DefaultView;
            cmbMachine1.DisplayMemberPath = dtMachine.Columns["MCode"].ToString();
            cmbMachine1.SelectedValuePath = dtMachine.Columns["MID"].ToString();
            cmbMachine1.SelectedIndex = 0;

            cmbMachine2.ItemsSource = dtMachine.DefaultView;
            cmbMachine2.DisplayMemberPath = dtMachine.Columns["MCode"].ToString();
            cmbMachine2.SelectedValuePath = dtMachine.Columns["MID"].ToString();
            cmbMachine2.SelectedIndex = 0;

            cmbMachine3.ItemsSource = dtMachine.DefaultView;
            cmbMachine3.DisplayMemberPath = dtMachine.Columns["MCode"].ToString();
            cmbMachine3.SelectedValuePath = dtMachine.Columns["MID"].ToString();
            cmbMachine3.SelectedIndex = 0;

            cmbMachine4.ItemsSource = dtMachine.DefaultView;
            cmbMachine4.DisplayMemberPath = dtMachine.Columns["MCode"].ToString();
            cmbMachine4.SelectedValuePath = dtMachine.Columns["MID"].ToString();
            cmbMachine4.SelectedIndex = 0;

            cmbMachine5.ItemsSource = dtMachine.DefaultView;
            cmbMachine5.DisplayMemberPath = dtMachine.Columns["MCode"].ToString();
            cmbMachine5.SelectedValuePath = dtMachine.Columns["MID"].ToString();
            cmbMachine5.SelectedIndex = 0;

            cmbMachine6.ItemsSource = dtMachine.DefaultView;
            cmbMachine6.DisplayMemberPath = dtMachine.Columns["MCode"].ToString();
            cmbMachine6.SelectedValuePath = dtMachine.Columns["MID"].ToString();
            cmbMachine6.SelectedIndex = 0;

            cmbMachine7.ItemsSource = dtMachine.DefaultView;
            cmbMachine7.DisplayMemberPath = dtMachine.Columns["MCode"].ToString();
            cmbMachine7.SelectedValuePath = dtMachine.Columns["MID"].ToString();
            cmbMachine7.SelectedIndex = 0;


        }

          private void AllowOnlyNumeric(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D0:
                case Key.D1:
                case Key.D2:
                case Key.D3:
                case Key.D4:
                case Key.D5:
                case Key.D6:
                case Key.D7:
                case Key.D8:
                case Key.D9:
                case Key.NumLock:
                case Key.NumPad0:
                case Key.NumPad1:
                case Key.NumPad2:
                case Key.NumPad3:
                case Key.NumPad4:
                case Key.NumPad5:
                case Key.NumPad6:
                case Key.NumPad7:
                case Key.NumPad8:
                case Key.NumPad9:
                case Key.Back:
                    break;
                default:
                    e.Handled = true;
                    break;
            }
        }

        #endregion

    }
}
