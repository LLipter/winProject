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
using OpenCvSharp.XFeatures2D;

namespace ImageStitcher
{
    /// <summary>
    /// Stitcher.xaml 的交互逻辑
    /// </summary>
    public partial class StitcherWindow : System.Windows.Window
    {
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private Mat firstImage = null;
        private Mat secondImage = null;
        private Mat resultImage = null;

        public StitcherWindow()
        {
            InitializeComponent();
            openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.Filter = "JPEG file(*.jpg)| *.jpg;*.jpeg | PNG file(*.png)| *.png";
            openFileDialog.FilterIndex = 0;

            saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.Filter = "image files(*.jpg)| *.jpg;*.jpeg | PNG file(*.png)| *.png";
            saveFileDialog.FilterIndex = 0;
        }

        private void btnLoadImage1_Click(object sender, RoutedEventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    firstImage = new Mat(openFileDialog.FileName, ImreadModes.Color);
                    openFileDialog.FileName = "";
                }
                catch (Exception)
                {
                    MessageBox.Show("Image open error", "Error");
                    return;
                }
                imgImage1.Source = OpenCvSharp.Extensions.BitmapSourceConverter.ToBitmapSource(firstImage);
                lblSize1.Content = string.Format("{0} x {1}", firstImage.Width, firstImage.Height);
            }
        }

        private void btnLoadImage2_Click(object sender, RoutedEventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    secondImage = new Mat(openFileDialog.FileName, ImreadModes.Color);
                    openFileDialog.FileName = "";
                }
                catch (Exception)
                {
                    MessageBox.Show("Image open error", "Error");
                    return;
                }
                imgImage2.Source = OpenCvSharp.Extensions.BitmapSourceConverter.ToBitmapSource(secondImage);
                lblSize2.Content = string.Format("{0} x {1}", secondImage.Width, secondImage.Height);
            }
        }

        private void btnSaveResult_Click(object sender, RoutedEventArgs e)
        {
            if(resultImage == null)
            {
                MessageBox.Show("Please stitch images first", "Error");
                return;
            }

            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    resultImage.SaveImage(saveFileDialog.FileName);
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

        private void btnStich_Click(object sender, RoutedEventArgs e)
        {
            if (firstImage == null)
            {
                MessageBox.Show("Please load the first image", "Error");
                return;
            }

            if (secondImage == null)
            {
                MessageBox.Show("Please load the second image", "Error");
                return;
            }

            Stitcher stitcher = new Stitcher(firstImage, secondImage);
            this.Hide();
            resultImage = stitcher.stitch();
            imgResult.Source = OpenCvSharp.Extensions.BitmapSourceConverter.ToBitmapSource(resultImage);
            lblSizeResult.Content = string.Format("{0} x {1}", imgResult.Width, imgResult.Height);
            this.Show();

        }

        private void btnCrop1_Click(object sender, RoutedEventArgs e)
        {
            if (firstImage == null)
            {
                MessageBox.Show("Please load the first image", "Error");
                return;
            }

            Cropper cropper = new Cropper(firstImage);
            this.Hide();
            firstImage = cropper.Show();
            imgImage1.Source = OpenCvSharp.Extensions.BitmapSourceConverter.ToBitmapSource(firstImage);
            lblSize1.Content = string.Format("{0} x {1}", firstImage.Width, firstImage.Height);
            this.Show();
        }

        private void btnCrop2_Click(object sender, RoutedEventArgs e)
        {
            if (secondImage == null)
            {
                MessageBox.Show("Please load the second image", "Error");
                return;
            }

            Cropper cropper = new Cropper(secondImage);
            this.Hide();
            secondImage = cropper.Show();
            imgImage2.Source = OpenCvSharp.Extensions.BitmapSourceConverter.ToBitmapSource(secondImage);
            lblSize2.Content = string.Format("{0} x {1}", secondImage.Width, secondImage.Height);
            this.Show();
        }

        private void btnCropResult_Click(object sender, RoutedEventArgs e)
        {
            if (resultImage == null)
            {
                MessageBox.Show("Please stitch images first", "Error");
                return;
            }

            Cropper cropper = new Cropper(resultImage);
            this.Hide();
            resultImage = cropper.Show();
            imgResult.Source = OpenCvSharp.Extensions.BitmapSourceConverter.ToBitmapSource(resultImage);
            lblSizeResult.Content = string.Format("{0} x {1}", imgResult.Width, imgResult.Height);
            this.Show();
        }

        private void btnSave1_Click(object sender, RoutedEventArgs e)
        {
            if (firstImage == null)
            {
                MessageBox.Show("Please load the first image", "Error");
                return;
            }

            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    firstImage.SaveImage(saveFileDialog.FileName);
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

        private void btnSave2_Click(object sender, RoutedEventArgs e)
        {
            if (secondImage == null)
            {
                MessageBox.Show("Please load the second image", "Error");
                return;
            }

            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    secondImage.SaveImage(saveFileDialog.FileName);
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
    }
}
