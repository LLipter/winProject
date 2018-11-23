using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OpenCvSharp;

namespace ImageStitcher
{
    /// <summary>
    /// CropperWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CropperWindow : System.Windows.Window
    {

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private Mat croppedImage = null;

        public CropperWindow()
        {
            InitializeComponent();
            openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.Filter = "JPEG file(*.jpg)| *.jpg;*.jpeg | PNG file(*.png)| *.png";
            openFileDialog.FilterIndex = 0;

            saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.Filter = "image files(*.jpg)| *.jpg;*.jpeg | PNG file(*.png)| *.png";
            saveFileDialog.FilterIndex = 0;
        }

        private void btnChooseFile_Click(object sender, RoutedEventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lblFilePath.Content = openFileDialog.FileName;
                openFileDialog.FileName = "";
            }
        }

        private void btnSavePath_Click(object sender, RoutedEventArgs e)
        {
            if (croppedImage == null)
            {
                MessageBox.Show("Please crop image first", "Error");
                return;
            }

            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                croppedImage.SaveImage(saveFileDialog.FileName);
                saveFileDialog.FileName = "";
            }

            MessageBox.Show("ok", "Save Result");
        }

        private void btnCrop_Click(object sender, RoutedEventArgs e)
        {
            if((string)lblFilePath.Content == string.Empty)
            {
                MessageBox.Show("Please choose image first", "Error");
                return;
            }

            Cropper cropper = new Cropper((string)lblFilePath.Content);
            try
            {
                croppedImage = cropper.Show();
            }
            catch(Exception )
            {
                MessageBox.Show("File open error", "Error");
                return;
            }

            imgPreview.Source = OpenCvSharp.Extensions.BitmapSourceConverter.ToBitmapSource(croppedImage);
        }
    }
}
