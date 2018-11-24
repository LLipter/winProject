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
        private Mat image = null;
        private Mat croppedImage = null;
        Cropper cropper = null;

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
                try
                {
                    image = new Mat(openFileDialog.FileName);
                    croppedImage = image;
                    cropper = new Cropper(image);
                    openFileDialog.FileName = "";
                }
                catch (Exception)
                {
                    MessageBox.Show("Image open error", "Error");
                    return;
                }
                imgPreview.Source = OpenCvSharp.Extensions.BitmapSourceConverter.ToBitmapSource(image);
                lblSize.Content = string.Format("{0} x {1}", image.Width, image.Height);
            }
        }

        private void btnSavePath_Click(object sender, RoutedEventArgs e)
        {
            if (croppedImage == null)
            {
                MessageBox.Show("Please load image first", "Error");
                return;
            }

            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    croppedImage.SaveImage(saveFileDialog.FileName);
                    saveFileDialog.FileName = "";
                }
                catch (Exception)
                {
                    MessageBox.Show("Image write error", "Error");
                    return;
                }
                MessageBox.Show("ok", "Save Result");
            }
            
        }

        private void btnCrop_Click(object sender, RoutedEventArgs e)
        {
            if(image == null)
            {
                MessageBox.Show("Please load image first", "Error");
                return;
            }
            this.IsEnabled = false;
            croppedImage = cropper.Show();
            this.IsEnabled = true;
            imgPreview.Source = OpenCvSharp.Extensions.BitmapSourceConverter.ToBitmapSource(croppedImage);
            lblSize.Content = string.Format("{0} x {1}", croppedImage.Width, croppedImage.Height);
        }
    }
}
