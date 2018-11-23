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
        static private Mat currentImage = new Mat();
        static private OpenCvSharp.Window CroppingWindow;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (CroppingWindow = new OpenCvSharp.Window("Cropping", WindowMode.AutoSize, image))
            {

                CvMouseCallback onMouse = new CvMouseCallback(mouseCallback);
                CroppingWindow.SetMouseCallback(onMouse);
                Cv2.WaitKey();
            }
        }

        static private bool down = false;
        static private bool up = false;
        static private OpenCvSharp.Point conor1 = new OpenCvSharp.Point();
        static private OpenCvSharp.Point conor2 = new OpenCvSharp.Point();
        static private OpenCvSharp.Point currentPt = new OpenCvSharp.Point();
        static private OpenCvSharp.Rect box = new OpenCvSharp.Rect();
        private static void mouseCallback(MouseEvent e, int x, int y, MouseEvent args, IntPtr data)
        {
            // When the left mouse button is pressed, record its position and save it in ltConor
            if (e == MouseEvent.LButtonDown)
            {
                down = true;
                conor1.X = x;
                conor1.Y = y;
            }

            // When the left mouse button is released, record its position and save it in rbConor
            if (e == MouseEvent.LButtonUp)
            {
                up = true;
                conor2.X = Math.Min(x, image.Width - 1);
                conor2.Y = Math.Min(x, image.Height - 1);
            }

            // Update the box showing the selected region as the user drags the mouse
            if (down == true && up == false)
            {
                currentPt.X = Math.Min(x, image.Width - 1);
                currentPt.Y = Math.Min(x, image.Height - 1);
                image.CopyTo(currentImage);
                if (conor1.X <= currentPt.X && conor1.Y <= currentPt.Y)
                    Cv2.Rectangle(currentImage, conor1, currentPt, new Scalar(0, 0, 255), 3);
                else if (conor1.X >= currentPt.X && conor1.Y >= currentPt.Y)
                    Cv2.Rectangle(currentImage, currentPt, conor1, new Scalar(0, 0, 255), 3);
                else if (conor1.X >= currentPt.X && conor1.Y <= currentPt.Y)
                    Cv2.Rectangle(currentImage, new OpenCvSharp.Point(currentPt.X, conor1.Y), new OpenCvSharp.Point(conor1.X, currentPt.Y), new Scalar(0, 0, 255), 3);
                else
                    Cv2.Rectangle(currentImage, new OpenCvSharp.Point(conor1.X, currentPt.Y), new OpenCvSharp.Point(currentPt.X, conor1.Y), new Scalar(0, 0, 255), 3);
                CroppingWindow.ShowImage(currentImage);
            }

            // Define ROI and crop it out when both corners have been selected
            // ROI : region of interest
            if (down == true && up == true)
            {
                box.Width = Math.Abs(conor1.X - conor2.X);
                box.Height = Math.Abs(conor1.Y - conor2.Y);
                box.X = Math.Min(conor1.X, conor2.X);
                box.Y = Math.Min(conor1.Y, conor2.Y);
                // Make an image out of just the selected ROI and display it in a new window
                Mat croppedImage = new Mat(image, box);
                image = croppedImage;
                CroppingWindow.ShowImage(image);
                down = false;
                up = false;
            }
        }
    }
}
