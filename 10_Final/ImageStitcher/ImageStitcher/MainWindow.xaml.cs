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

        static private Mat image = new Mat("C:\\Users\\99645\\Desktop\\1.png");
        static private Mat currentImage = new Mat("C:\\Users\\99645\\Desktop\\1.png");
        static private OpenCvSharp.Window CroppingWindow;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (CroppingWindow = new OpenCvSharp.Window("Cropping", WindowMode.Normal, image))
            {

                CvMouseCallback onMouse = new CvMouseCallback(mouseCallback);
                CroppingWindow.SetMouseCallback(onMouse);
                Cv2.WaitKey();
            }
        }

        static private bool down = false;
        static private bool up = false;
        static private OpenCvSharp.Point ltConor = new OpenCvSharp.Point();
        static private OpenCvSharp.Point rbConor = new OpenCvSharp.Point();
        static private OpenCvSharp.Point currentPt = new OpenCvSharp.Point();
        static private OpenCvSharp.Rect box = new OpenCvSharp.Rect();
        private static void mouseCallback(MouseEvent e, int x, int y, MouseEvent args, IntPtr data)
        {
            // When the left mouse button is pressed, record its position and save it in ltConor
            if (e == MouseEvent.LButtonDown)
            {
                down = true;
                ltConor.X = x;
                ltConor.Y = y;
            }

            // When the left mouse button is released, record its position and save it in rbConor
            if (e == MouseEvent.LButtonUp)
            {
                up = true;
                rbConor.X = x;
                rbConor.Y = y;
            }

            // Update the box showing the selected region as the user drags the mouse
            if (down == true && up == false)
            {
                currentPt.X = x;
                currentPt.Y = y;
                image.CopyTo(currentImage);
                Cv2.Rectangle(currentImage, ltConor, currentPt, new Scalar(0, 0, 255));
                CroppingWindow.ShowImage(currentImage);
            }

            // Define ROI and crop it out when both corners have been selected
            // ROI : region of interest
            if (down == true && up == true)
            {
                box.Width = Math.Abs(ltConor.X - rbConor.X);
                box.Height = Math.Abs(ltConor.Y - rbConor.Y);
                box.X = Math.Min(ltConor.X, rbConor.X);
                box.Y = Math.Min(ltConor.Y, rbConor.Y);
                // Make an image out of just the selected ROI and display it in a new window
                Mat croppedImage = new Mat(image, box);
                CroppingWindow.ShowImage(croppedImage);
                down = false;
                up = false;
            }
        }
    }
}
