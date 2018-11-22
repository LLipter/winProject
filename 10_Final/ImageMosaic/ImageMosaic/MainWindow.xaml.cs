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
using OpenCvSharp;


namespace ImageMosaic
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Mat src = new Mat("C:\\Users\\99645\\Desktop\\1.png", ImreadModes.GrayScale);
            // Mat src = Cv2.ImRead("lenna.png", ImreadModes.GrayScale);
            Mat dst = new Mat();

            Cv2.Canny(src, dst, 50, 200);
            using (new OpenCvSharp.Window("src image", src))
            using (new OpenCvSharp.Window("dst image", dst))
            {
                Cv2.WaitKey();
            }
        }
    }
}
