using OpenCvSharp;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageStitcher
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnStitcher_Click(object sender, RoutedEventArgs e)
        {
            Stitcher stitcher = new Stitcher();
            stitcher.ShowDialog();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Cropper cropper = new Cropper("C:\\Users\\99645\\Desktop\\1.png");
            try
            {
                cropper.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("File open error", "Error");
                return;
            }
            
        }


    }
}
