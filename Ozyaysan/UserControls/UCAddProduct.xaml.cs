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
using System.Text.RegularExpressions;

namespace Ozyaysan
{
    /// <summary>
    /// Interaction logic for UCAddProduct.xaml
    /// </summary>
    public partial class UCAddProduct : UserControl
    {

        #region Constructors
        public UCAddProduct()
        {
            InitializeComponent();
            LoadData();
        }
        public UCAddProduct(int PID)
        {
            InitializeComponent();
            this.PID = PID;
            LoadData();
        }
        #endregion

        #region Fields
        int m_PID = 0;
        List<TempPicture> PictureList = new List<TempPicture>();
        #endregion

        #region Properties
        public int PID
        {
            get { return m_PID; }
            set { m_PID = value; }
        }
        #endregion

        #region Events

        #region KeyDowns
        private void KeyDown_OnlyNumeric(object sender, KeyEventArgs e)
        {
            AllowOnlyNumeric(e);
        }
        private void KeyDown_OnlyNumericAndDecimal(object sender, KeyEventArgs e)
        {
            AllowOnlyNumericAndDecimal(e);
        }
        #endregion

        #region Click
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
           
            string strVal = ValidationMethot();
            if (strVal == "")
            {
                BLL.Product oProduct = new BLL.Product();
                if (this.PID == 0)
                {
                    oProduct.ID = 0;
                }
                else
                {
                    oProduct.ID = PID;
                }
                oProduct.Code = txtCode.Text;
                oProduct.ExtraCode1 = txtExtraKod1.Text;
                oProduct.ExtraCode2 = txtExtraKod2.Text;
                oProduct.ExtraCode3 = txtExtraKod3.Text;
                oProduct.ExtraCode4 = txtExtraKod4.Text;
                oProduct.Picture1 = "None.png";
                oProduct.Picture2 = "None.png";
                oProduct.Picture3 = "None.png";
                oProduct.Picture4 = "None.png";
                oProduct.TecniquePicture = "None.png";
                foreach (TempPicture item in PictureList)
                {
                    switch (item.PictureIndex)
                    {
                        case 1:
                            oProduct.Picture1 = item.SavedFileName;
                            break;
                        case 2:
                            oProduct.Picture2 = item.SavedFileName;
                            break;
                        case 3:
                            oProduct.Picture3 = item.SavedFileName;
                            break;
                        case 4:
                            oProduct.Picture4 = item.SavedFileName;
                            break;
                        case 5:
                            oProduct.TecniquePicture = item.SavedFileName;
                            break;
                        default:
                            break;
                    }
                }
                oProduct.Diameter = float.Parse((txtDiameter.Text.Trim()=="")?0.ToString():txtDiameter.Text.Trim());
                oProduct.WireDiameter = float.Parse((txtWireDiameter.Text.Trim() == "") ? 0.ToString() : txtWireDiameter.Text.Trim()); 
                oProduct.Name = txtCode.Text;
                //oProduct.NumberOfForm = Int32.Parse(txtNumForms.Text);
                oProduct.NumberOfTurns = Int32.Parse((txtNumTurns.Text.Trim() == "") ? 0.ToString() : txtNumTurns.Text.Trim());
                oProduct.RawMaterialsCharacteristic = txtRawMatarialCharacters.Text;
                oProduct.State = BLL.Enumarations.State.Aktif;
                oProduct.StraighteningLength = float.Parse((txtStraighteningLength.Text.Trim() == "") ? 0.ToString() : txtStraighteningLength.Text.Trim());
                oProduct.Price = 0;// kaldırıldı
                oProduct.Weight = float.Parse((txtWeight.Text.Trim() == "") ? 0.ToString() : txtWeight.Text.Trim());
                BLL.Category oCategory = new BLL.Category();
                oProduct.Category = oCategory;
                oProduct.Category.ID = Int32.Parse(cmbCategory.SelectedValue.ToString());
                //if ( oProduct.Category.ID==1)
                //{
                //    oProduct.HatveArasi = Int32.Parse(txtHatve.Text);
                //    oProduct.ToplamBoy = Int32.Parse(txtToplamBoy.Text);
                //}
                //else if (oProduct.Category.ID==2)
                //{
                //    oProduct.KancaArasi = Int32.Parse(txtKancaArasi.Text);
                //    oProduct.Ictenice = Int32.Parse(txtIctenice.Text);
                //    oProduct.KancaYonu = txtkancaYon.Text;
                //}
               // oProduct.Stock = Int32.Parse(txtStock.Text);
                int nResult = oProduct.Save();
                if (nResult == 0)
                {
                    foreach (TempPicture item in PictureList)
                    {
                        if (item.IsTecniqueImage)
                        {
                            CopyTecniqueImageToSharedImageFolder(item.FilePath, item.FileName, item.SavedFileName);
                        }
                        else
                        {
                            CopyImageToSharedImageFolder(item.FilePath, item.FileName, item.SavedFileName);
                        }

                    }

                    MessageBox.Show("Yeni yay ekleme işlemi başarılı bir şekilde tamamlandı.Şimdi yay için üretim aşaması ekleme formuna yönlendiriliyorsunuz.");
                    var window = new W_AddProductionPhase(oProduct.ID)
                    {
                        Title = "Üretim Aşaması Ekle",
                        ShowInTaskbar = false,
                        Topmost = true,
                        ResizeMode = ResizeMode.CanMinimize,
                        Owner = Application.Current.MainWindow,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen
                    };
                    if (window.ShowDialog() == false)
                    {
                        (((Start)((Grid)(((TabControl)(((TabItem)((Grid)((Grid)(this.Parent)).Parent).Parent).Parent)).Parent)).Parent)).GrdContentSearchProduct.Visibility = Visibility.Hidden;
                        (((Start)((Grid)(((TabControl)(((TabItem)((Grid)((Grid)(this.Parent)).Parent).Parent).Parent)).Parent)).Parent)).GrdContentAddProduct.Visibility = Visibility.Hidden;
                        (((Start)((Grid)(((TabControl)(((TabItem)((Grid)((Grid)(this.Parent)).Parent).Parent).Parent)).Parent)).Parent)).GrdContentProductDetail.Visibility = Visibility.Visible;
                        Ozyaysan.UserControls.UCProductDetail oWindowsProductDetail = new UserControls.UCProductDetail(oProduct.ID);
                        (((Start)((Grid)(((TabControl)(((TabItem)((Grid)((Grid)(this.Parent)).Parent).Parent).Parent)).Parent)).Parent)).GrdContentProductDetail.Children.Clear();
                        (((Start)((Grid)(((TabControl)(((TabItem)((Grid)((Grid)(this.Parent)).Parent).Parent).Parent)).Parent)).Parent)).GrdContentProductDetail.Children.Add(oWindowsProductDetail);
                    }

                }
                else if (nResult == 3)
                {
                    MessageBox.Show("Bu OEM kodlu yay daha önce eklenmiş !!");
                }
                else
                {
                    MessageBox.Show("Kayıt esnasında bir hata oluştu !!");
                }
            }
            else
            {
                MessageBox.Show(strVal);
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());  
            }
        }

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }
        // Use the DataObject.Pasting Handler 
        private void TextBoxPasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        #endregion

        #region MouseDown

        private void imgPicture_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                Random rnd = new Random();
                int number = rnd.Next(0, 20000);
                string SavedFileName = number.ToString() + "_" + DateTime.Now.ToShortDateString() + System.IO.Path.GetExtension(dlg.SafeFileName);
                //CopyImageToSharedImageFolder(dlg.FileName.Substring(0, dlg.FileName.LastIndexOf("\\")), dlg.SafeFileName);
                ((Image)sender).IsEnabled = false;
                switch (((Image)sender).Uid)
                {
                    case "1":
                        PictureList.Add(new TempPicture(Int32.Parse(((Image)sender).Uid), dlg.SafeFileName, dlg.FileName.Substring(0, dlg.FileName.LastIndexOf("\\")), SavedFileName, false));
                        imgOk1.Visibility = Visibility.Visible;
                        break;
                    case "2":
                        PictureList.Add(new TempPicture(Int32.Parse(((Image)sender).Uid), dlg.SafeFileName, dlg.FileName.Substring(0, dlg.FileName.LastIndexOf("\\")), SavedFileName, false));
                        imgOk2.Visibility = Visibility.Visible;
                        break;
                    case "3":
                        PictureList.Add(new TempPicture(Int32.Parse(((Image)sender).Uid), dlg.SafeFileName, dlg.FileName.Substring(0, dlg.FileName.LastIndexOf("\\")), SavedFileName, false));
                        imgOk3.Visibility = Visibility.Visible;
                        break;
                    case "4":
                        PictureList.Add(new TempPicture(Int32.Parse(((Image)sender).Uid), dlg.SafeFileName, dlg.FileName.Substring(0, dlg.FileName.LastIndexOf("\\")), SavedFileName, false));
                        imgOk4.Visibility = Visibility.Visible;
                        break;
                    case "5":
                        PictureList.Add(new TempPicture(Int32.Parse(((Image)sender).Uid), dlg.SafeFileName, dlg.FileName.Substring(0, dlg.FileName.LastIndexOf("\\")), SavedFileName, true));
                        imgOkTecnique.Visibility = Visibility.Visible;
                        break;
                    default:
                        break;
                }

            }


        }



        #endregion

        #region Changed
        private void cmbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (cmbCategory.SelectedValue.ToString()=="1")
            //{
            //    brdTorsiyon.Visibility = Visibility.Visible;
            //    brdCekme.Visibility = Visibility.Hidden;
            //}
            //else if (cmbCategory.SelectedValue.ToString() == "2")
            //{
            //    brdTorsiyon.Visibility = Visibility.Hidden;
            //    brdCekme.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    brdTorsiyon.Visibility = Visibility.Hidden;
            //    brdCekme.Visibility = Visibility.Hidden;
            //}
        }

        #endregion
        #endregion

        #region Custom Methots
        private string ValidationMethot()
        {
            string ErrorMessage = "";
            if (txtCode.Text.Trim() == "")
            {
                ErrorMessage += "OEM Kod alanı boş bırakılamaz !" + Environment.NewLine + "";
            }
            //if (txtNumForms.Text.Trim() == "")
            //{
            //    ErrorMessage += "Form sayısı alanı boş bırakılamaz !" + Environment.NewLine + "";
            //}
            //if (txtStraighteningLength.Text.Trim() == "")
            //{
            //    ErrorMessage += "Doğrultma boyu alanı boş bırakılamaz !" + Environment.NewLine + "";
            //}
            if (txtWeight.Text.Trim() == "")
            {
                ErrorMessage += "Ağırlık alanı boş bırakılamaz !" + Environment.NewLine + "";
            }
            //if (txtDiameter.Text.Trim() == "")
            //{
            //    ErrorMessage += "Yay çapı alanı boş bırakılamaz !" + Environment.NewLine + "";
            //}
            //if (txtWireDiameter.Text.Trim() == "")
            //{
            //    ErrorMessage += "Tel çapı alanı boş bırakılamaz !" + Environment.NewLine + "";
            //}
            //if (txtPrice.Text.Trim() == "")
            //{
            //    ErrorMessage += "Birim fiyatı alanı boş bırakılamaz !" + Environment.NewLine + "";
            //}
            //if (txtNumTurns.Text.Trim() == "")
            //{
            //    ErrorMessage += "Sarım sayısı alanı boş bırakılamaz !" + Environment.NewLine + "";
            //}
            //if (cmbCategory.SelectedIndex == 0)
            //{
            //    ErrorMessage += "Lütfen bir kategori seçiniz !" + Environment.NewLine + "";
            //}
            //else if(cmbCategory.SelectedValue.ToString() =="1")
            //{
            //    if (txtHatve.Text.Trim()=="")
            //    {
            //       ErrorMessage += "Hatve arası boş bırakılamaz !" + Environment.NewLine + "";
            //    }
            //    if (txtToplamBoy.Text.Trim()=="")
            //    {
            //        ErrorMessage += "Toplam boy boş bırakılamaz !" + Environment.NewLine + "";
            //    }
            //}
            //else if (cmbCategory.SelectedValue.ToString() == "2")
            //{
            //    if (txtKancaArasi.Text.Trim() == "")
            //    {
            //        ErrorMessage += "Kanca arası boş bırakılamaz !" + Environment.NewLine + "";
            //    }
            //    if (txtIctenice.Text.Trim() == "")
            //    {
            //        ErrorMessage += "İçten içe boy boş bırakılamaz !" + Environment.NewLine + "";
            //    }
            //    if (txtkancaYon.Text.Trim() == "")
            //    { 
            //        ErrorMessage += "Kanca yönü boş bırakılamaz !" + Environment.NewLine + "";
            //    }
            //}
            return ErrorMessage;
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
        private void AllowOnlyNumericAndDecimal(KeyEventArgs e)
        {

            switch (e.Key)
            {
                case Key.Decimal:
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
        private void CopyImageToSharedImageFolder(string _Path, string SafeFileName, string SavedFileName)
        {

            string fileName = SafeFileName;
            string sourcePath = _Path;         
            string targetPath = Properties.Settings.Default.TargetPath;
            string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
            string destFile = System.IO.Path.Combine(targetPath, SavedFileName);
            if (!System.IO.Directory.Exists(targetPath))
            {
                System.IO.Directory.CreateDirectory(targetPath);
            }
            System.IO.File.Copy(sourceFile, destFile, true);
            if (System.IO.Directory.Exists(sourcePath))
            {
                string[] files = System.IO.Directory.GetFiles(sourcePath);
                foreach (string s in files)
                {

                    fileName = System.IO.Path.GetFileName(s);
                    if (SafeFileName == fileName)
                    {
                        destFile = System.IO.Path.Combine(targetPath, SavedFileName);
                        System.IO.File.Copy(s, destFile, true);
                    }
                }
            }
            else
            {

            }
        }
        private void CopyTecniqueImageToSharedImageFolder(string _Path, string SafeFileName, string SavedFileName)
        {

            string fileName = SafeFileName;
            string sourcePath = _Path;
            string targetPath = Properties.Settings.Default.TargetTecniquePath;            
            string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
            string destFile = System.IO.Path.Combine(targetPath, SavedFileName);
            if (!System.IO.Directory.Exists(targetPath))
            {
                System.IO.Directory.CreateDirectory(targetPath);
            }
            System.IO.File.Copy(sourceFile, destFile, true);
            if (System.IO.Directory.Exists(sourcePath))
            {
                string[] files = System.IO.Directory.GetFiles(sourcePath);
                foreach (string s in files)
                {

                    fileName = System.IO.Path.GetFileName(s);
                    if (SafeFileName == fileName)
                    {
                        destFile = System.IO.Path.Combine(targetPath, SavedFileName);
                        System.IO.File.Copy(s, destFile, true);
                    }
                }
            }
            else
            {

            }
        }
        private void LoadData()
        {
            LoadDDLs();
            if (this.PID != 0)
            {
                BLL.Product oProduct = new BLL.Product(PID, BLL.Enumarations.State.Aktif);
                txtCode.Text = oProduct.Code;
                txtDiameter.Text = oProduct.Diameter.ToString();
                txtWireDiameter.Text = oProduct.WireDiameter.ToString();
                //txtNumForms.Text = oProduct.NumberOfForm.ToString();
                txtNumTurns.Text = oProduct.NumberOfTurns.ToString();
                txtRawMatarialCharacters.Text = oProduct.RawMaterialsCharacteristic;
                txtStraighteningLength.Text = oProduct.StraighteningLength.ToString();
                //txtPrice.Text = oProduct.Price.ToString();
                txtWeight.Text = oProduct.Weight.ToString();
                cmbCategory.SelectedValue = oProduct.CategoryID;
                //cmbState.SelectedValue = (int)oProduct.State;


            }
        }

        private void LoadDDLs()
        {
            DataTable dtCategories = BLL.Category.GetProductCategoryListForDDL();
            cmbCategory.ItemsSource = dtCategories.DefaultView;
            cmbCategory.DisplayMemberPath = dtCategories.Columns["CAName"].ToString();
            cmbCategory.SelectedValuePath = dtCategories.Columns["CAID"].ToString();
            cmbCategory.SelectedIndex = 0;

           // DataTable dtState = BLL.Utility.GetStateListForDDLWithoutChose();
            //cmbState.ItemsSource = dtState.DefaultView;
           // cmbState.DisplayMemberPath = dtState.Columns["SName"].ToString();
           // cmbState.SelectedValuePath = dtState.Columns["SID"].ToString();
           // cmbState.SelectedIndex = 1;

        }

        #endregion


        #region InnerClasses
        public class TempPicture
        {
            public TempPicture(int PictureIndex, string FileName, string FilePath, string SavedFileName, bool IsTecniqueImage)
            {
                this.PictureIndex = PictureIndex;
                this.FileName = FileName;
                this.FilePath = FilePath;
                this.SavedFileName = SavedFileName;
                this.IsTecniqueImage = IsTecniqueImage;
            }

            bool m_IsTecniqueImage = false;

            public bool IsTecniqueImage
            {
                get { return m_IsTecniqueImage; }
                set { m_IsTecniqueImage = value; }
            }
            int m_PictureIndex;
            public int PictureIndex
            {
                get { return m_PictureIndex; }
                set { m_PictureIndex = value; }
            }
            string m_FileName;
            public string FileName
            {
                get { return m_FileName; }
                set { m_FileName = value; }
            }
            string m_FilePath;
            public string FilePath
            {
                get { return m_FilePath; }
                set { m_FilePath = value; }
            }
            string m_SavedFileName;

            public string SavedFileName
            {
                get { return m_SavedFileName; }
                set { m_SavedFileName = value; }
            }
        }
        #endregion

        
       



    }
}
