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

namespace ImageStitcher
{
    /// <summary>
    /// Stitcher.xaml 的交互逻辑
    /// </summary>
    public partial class Stitcher : Window
    {
        private System.Windows.Forms.OpenFileDialog openFileDialog;

        public Stitcher()
        {
            InitializeComponent();
            openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.Filter = "image files(*.jpg)| *.jpg;*.jpeg";
            openFileDialog.FilterIndex = 0;
        }

        private void btnChooseImage1_Click(object sender, RoutedEventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lblPath1.Content = openFileDialog.FileName;
            }
        }

        private void btnChooseImage2_Click(object sender, RoutedEventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lblPath2.Content = openFileDialog.FileName;
            }
        }
    }
}
