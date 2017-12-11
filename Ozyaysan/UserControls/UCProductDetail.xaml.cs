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
using System.IO;
using System.Diagnostics;
namespace Ozyaysan.UserControls
{
    /// <summary>
    /// Interaction logic for UCProductDetail.xaml
    /// </summary>
    public partial class UCProductDetail : UserControl
    {
        #region Contructors

        public UCProductDetail(int PID)
        {
            InitializeComponent();
            this.PID = PID;
            this.oProduct = new BLL.Product(this.PID, BLL.Enumarations.State.Aktif);
            LoadData();
            LoadDetail();

        }
        public UCProductDetail(int PID, bool onlyForExamine)
        {
            InitializeComponent();
            this.PID = PID;
            this.oProduct = new BLL.Product(this.PID, BLL.Enumarations.State.Aktif);
            LoadData();
            LoadDetail();
            #region SpecificAdjustments
            //brdTop.Margin = new Thickness(12, 12, 0, 0);
            btnUpdate.Visibility = Visibility.Hidden;
            imgChangePicture1.Visibility = Visibility.Hidden;
            imgChangePicture2.Visibility = Visibility.Hidden;
            imgChangePicture3.Visibility = Visibility.Hidden;
            imgChangePicture4.Visibility = Visibility.Hidden;
            cmbCategory.IsEnabled = false;
            cmbState.IsEnabled = false;
            imgChangeTecniquePicture.Visibility = Visibility.Hidden;
            txtDiameter.IsEnabled = false;
            txtWireDiameter.IsEnabled = false;
            txtExtraKod1.IsEnabled = false;
            txtExtraKod2.IsEnabled = false;
            txtExtraKod3.IsEnabled = false;
            txtExtraKod4.IsEnabled = false;
            //txtNumForms.IsEnabled = false;
            txtNumTurns.IsEnabled = false;
            txtCode.IsEnabled = false;
            txtRawMatarialCharacters.IsEnabled = false;
            txtStock.IsEnabled = false;
            txtStraighteningLength.IsEnabled = false;
            //txtPrice.IsEnabled = false;
            txtWeight.IsEnabled = false;

            #endregion
        }
        #endregion

        #region Fields
        int m_PID;
        BLL.Product oProduct;
        List<TempPicture> PictureList = new List<TempPicture>();
        RectangleGeometry ClipRctGeo = new RectangleGeometry();
        System.Windows.Shapes.Path ClipPath = new System.Windows.Shapes.Path();
        ScaleTransform ScaleTr = new ScaleTransform();
        bool StartModeOn = false;
        #endregion

        #region Properties
        public int PID
        {
            get { return m_PID; }
            set { m_PID = value; }
        }
        #endregion

        #region CustomMethots
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
            //if (txtStock.Text.Trim() == "")
            //{
            //    ErrorMessage += "Stok alanı boş bırakılamaz !" + Environment.NewLine + "";
            //}
            //if (cmbCategory.SelectedIndex == 0)
            //{
            //    ErrorMessage += "Lütfen bir kategori seçiniz !" + Environment.NewLine + "";
            //}
            //else if (cmbCategory.SelectedValue.ToString() == "1")
            //{
            //    if (txtHatve.Text.Trim() == "")
            //    {
            //        ErrorMessage += "Hatve arası boş bırakılamaz !" + Environment.NewLine + "";
            //    }
            //    if (txtToplamBoy.Text.Trim() == "")
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

        private void LoadDetail()
        {
            #region Pictures
            if (oProduct.Picture1 != "")
            {
                BitmapImage Picture1 = new BitmapImage();
                Picture1.BeginInit();
                Picture1.UriSource = new Uri(Properties.Settings.Default.TargetPath + "/" + oProduct.Picture1 + "");
                if (File.Exists(Properties.Settings.Default.TargetPath + "/" + oProduct.Picture1 + ""))
                {
                    Picture1.EndInit();
                    imgPicture1.Source = Picture1;
                }

            }

            if (oProduct.Picture2 != "")
            {
                BitmapImage Picture2 = new BitmapImage();
                Picture2.BeginInit();
                Picture2.UriSource = new Uri(Properties.Settings.Default.TargetPath + "/" + oProduct.Picture2 + "");
                if (File.Exists(Properties.Settings.Default.TargetPath + "/" + oProduct.Picture2 + ""))
                {
                    Picture2.EndInit();
                    imgPicture2.Source = Picture2;

                }
            }
            if (oProduct.Picture3 != "")
            {
                BitmapImage Picture3 = new BitmapImage();
                Picture3.BeginInit();
                Picture3.UriSource = new Uri(Properties.Settings.Default.TargetPath + "/" + oProduct.Picture3 + "");
                if (File.Exists(Properties.Settings.Default.TargetPath + "/" + oProduct.Picture3 + ""))
                {
                    Picture3.EndInit();
                    imgPicture3.Source = Picture3;
                }
            }

            if (oProduct.Picture4 != "")
            {
                BitmapImage Picture4 = new BitmapImage();
                Picture4.BeginInit();
                Picture4.UriSource = new Uri(Properties.Settings.Default.TargetPath + "/" + oProduct.Picture4 + "");
                if (File.Exists(Properties.Settings.Default.TargetPath + "/" + oProduct.Picture4 + ""))
                {
                    Picture4.EndInit();
                    imgPicture4.Source = Picture4;
                }
            }

            #endregion

            #region TextBoxes
            txtDiameter.Text = oProduct.Diameter.ToString();
            txtWireDiameter.Text = oProduct.WireDiameter.ToString();
            txtExtraKod1.Text = oProduct.ExtraCode1;
            txtExtraKod2.Text = oProduct.ExtraCode2;
            txtExtraKod3.Text = oProduct.ExtraCode3;
            txtExtraKod4.Text = oProduct.ExtraCode4;
           // txtNumForms.Text = oProduct.NumberOfForm.ToString();
            txtNumTurns.Text = oProduct.NumberOfTurns.ToString();
            txtCode.Text = oProduct.Code;
           
            txtRawMatarialCharacters.Text = oProduct.RawMaterialsCharacteristic;
            txtStraighteningLength.Text = oProduct.StraighteningLength.ToString();
            //txtPrice.Text = oProduct.Price.ToString();
            txtWeight.Text = oProduct.Weight.ToString();
            //txtStock.Text = oProduct.Stock.ToString();
            //txtHatve.Text = oProduct.HatveArasi.ToString();
            //txtToplamBoy.Text = oProduct.ToplamBoy.ToString();
            //txtKancaArasi.Text = oProduct.KancaArasi.ToString();
            //txtIctenice.Text = oProduct.Ictenice.ToString();
            //txtkancaYon.Text = oProduct.KancaYonu;
            #endregion

            #region Comboboxes
           
            cmbCategory.SelectedValue = oProduct.Category.ID;           
            cmbState.SelectedValue = (int)oProduct.State;
            #endregion

           // lblTitle.Content = oProduct.Code;

            //if (oProduct.Category.ID==1)
            //{
            //    brdTorsiyon.Visibility = Visibility.Visible;
            //    brdCekme.Visibility = Visibility.Hidden;
            //}
            //else if (oProduct.Category.ID==2)
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

        private void LoadData()
        {
            DataTable dtCategories = BLL.Category.GetProductCategoryListForDDL();
            cmbCategory.ItemsSource = dtCategories.DefaultView;
            cmbCategory.DisplayMemberPath = dtCategories.Columns["CAName"].ToString();
            cmbCategory.SelectedValuePath = dtCategories.Columns["CAID"].ToString();


            DataTable dtState = BLL.Utility.GetStateListForDDLWithoutChose();
            cmbState.ItemsSource = dtState.DefaultView;
            cmbState.DisplayMemberPath = dtState.Columns["SName"].ToString();
            cmbState.SelectedValuePath = dtState.Columns["SID"].ToString();

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
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string strVal = ValidationMethot();
            if (strVal == "")
            {
                oProduct.Name = txtCode.Text;
                oProduct.Code = txtCode.Text;
                oProduct.ExtraCode1 = txtExtraKod1.Text;
                oProduct.ExtraCode2 = txtExtraKod2.Text;
                oProduct.ExtraCode3 = txtExtraKod3.Text;
                oProduct.ExtraCode4 = txtExtraKod4.Text;
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
                oProduct.Diameter = float.Parse(txtDiameter.Text);
                oProduct.WireDiameter = float.Parse(txtWireDiameter.Text);
                oProduct.Name = txtCode.Text;
               // oProduct.NumberOfForm = Int32.Parse(txtNumForms.Text);
                oProduct.NumberOfTurns = Int32.Parse(txtNumTurns.Text);
                oProduct.RawMaterialsCharacteristic = txtRawMatarialCharacters.Text;
                oProduct.State = BLL.Enumarations.State.Aktif;
                oProduct.StraighteningLength = float.Parse(txtStraighteningLength.Text);
                oProduct.Price = 0;// kaldırıldı
                oProduct.Weight = float.Parse(txtWeight.Text);
                BLL.Category oCategory = new BLL.Category();
                oProduct.Category = oCategory;
                oProduct.Category.ID = Int32.Parse(cmbCategory.SelectedValue.ToString());
                //if (oProduct.Category.ID == 1)
                //{
                //    oProduct.HatveArasi = Int32.Parse(txtHatve.Text);
                //    oProduct.ToplamBoy = Int32.Parse(txtToplamBoy.Text);
                //}
                //else if (oProduct.Category.ID == 2)
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

                        LoadDetail();

                    }
                    MessageBox.Show("İşlem başarılı");
                }
                else if (nResult == 3)
                {
                    MessageBox.Show("Bu OEM kodu daha önce kullanılmış");
                }
                else
                {
                    MessageBox.Show("Kayıt işlemi esnasında bir hata oluştu.");
                }
            }
            else
            {
                MessageBox.Show(strVal);
            }
        }
        #region MouseDown
        private void lblTecniqueExamine_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (oProduct.TecniquePicture == "None.png" || oProduct.TecniquePicture == "" || oProduct.TecniquePicture == null)
            {
                MessageBox.Show("Teknik resim bulunamadı !");
            }
            else
            {
                var fileToOpen = Properties.Settings.Default.TargetTecniquePath + "\\" + oProduct.TecniquePicture;
                var process = new Process();
                process.StartInfo = new ProcessStartInfo()
                {
                    UseShellExecute = true,
                    FileName = fileToOpen
                };

                process.Start();

            }
        }

        private void lblChange_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                Random rnd = new Random();
                int number = rnd.Next(0, 20000);
                string SavedFileName = number.ToString() + "_" + DateTime.Now.ToShortDateString() + System.IO.Path.GetExtension(dlg.SafeFileName);
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
            //if (cmbCategory.SelectedValue!=null)
            //{
            //    if (cmbCategory.SelectedValue.ToString() == "1")
            //    {
            //        brdTorsiyon.Visibility = Visibility.Visible;
            //        brdCekme.Visibility = Visibility.Hidden;
            //    }
            //    else if (cmbCategory.SelectedValue.ToString() == "2")
            //    {
            //        brdTorsiyon.Visibility = Visibility.Hidden;
            //        brdCekme.Visibility = Visibility.Visible;
            //    }
            //    else
            //    {
            //        brdTorsiyon.Visibility = Visibility.Hidden;
            //        brdCekme.Visibility = Visibility.Hidden;
            //    }
            //}
        }
        #endregion
        #endregion

        #region InnerClasses
        private class TempPicture
        {
            public TempPicture(int PictureIndex, string FileName, string FilePath, string SavedFileName, bool IsTecniqueImage)
            {
                this.IsTecniqueImage = IsTecniqueImage;
                this.PictureIndex = PictureIndex;
                this.FileName = FileName;
                this.FilePath = FilePath;
                this.SavedFileName = SavedFileName;
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

        #region Magnifier Related Part
        private void ClipGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            ClipGrid.Visibility = System.Windows.Visibility.Hidden;
        }

        private void ClipGrid_MouseMove(object sender, MouseEventArgs e)
        {
            MoveMagnifier(e);
        }

        private void MainGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            MoveMagnifier(e);

            if (ClipGrid.Visibility == System.Windows.Visibility.Hidden)
            {
                ClipGrid.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void MoveMagnifier(MouseEventArgs e)
        {
            double mouseX = e.GetPosition(ClipGrid).X;
            double mouseY = e.GetPosition(ClipGrid).Y;

            ScaleTr.CenterX = mouseX;
            ScaleTr.CenterY = mouseY;

            // Set the location and dimensions of the
            // clipping rectangle.
            ClipRctGeo.Rect = new Rect((mouseX - 40), (mouseY - 40), 80, 80);
        }
        private void UserControl_Initialized(object sender, EventArgs e)
        {
            // Set the position and dimensions of
            // the clipping rectangle that defines
            // the magnification region.
            ClipRctGeo.Rect = new Rect(0, 0, 80, 80);

            ClipPath.Stroke = Brushes.Gainsboro;
            ClipPath.StrokeThickness = 2;
            ClipPath.Data = ClipRctGeo;

            ClipGrid.Children.Add(ClipPath);
            ClipGrid.Clip = ClipRctGeo;

            // Set magnification to 150%.
            ScaleTr.ScaleX = 1.5;
            ScaleTr.ScaleY = 1.5;

            ClipGrid.RenderTransform = ScaleTr;
            ClipGrid.Visibility = System.Windows.Visibility.Hidden;
        }
        #endregion

        private void imgPicture1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ClipImage.Source = imgPicture1.Source;
            MainImage.Source = imgPicture1.Source;
            brdImage.Visibility = Visibility.Visible;
        }
        private void imgPicture2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ClipImage.Source = imgPicture2.Source;
            MainImage.Source = imgPicture2.Source;
            brdImage.Visibility = Visibility.Visible;
        }
        private void imgPicture3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ClipImage.Source = imgPicture3.Source;
            MainImage.Source = imgPicture3.Source;
            brdImage.Visibility = Visibility.Visible;
        }
        private void imgPicture4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ClipImage.Source = imgPicture4.Source;
            MainImage.Source = imgPicture4.Source;
            brdImage.Visibility = Visibility.Visible;
        }






    }
}
