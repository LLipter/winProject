using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace ImageStitcher
{
    class Cropper
    {
        private Mat baseImage;
        private Mat srcImage;
        private Mat currentImage = new Mat();
        private OpenCvSharp.Window CroppingWindow;
        private bool down = false;
        private bool up = false;
        private OpenCvSharp.Point conor1 = new OpenCvSharp.Point();
        private OpenCvSharp.Point conor2 = new OpenCvSharp.Point();
        private OpenCvSharp.Rect box = new OpenCvSharp.Rect();

        public Cropper(Mat image)
        {
            this.srcImage = image;
            this.baseImage = this.srcImage.Clone();
        }

        public Mat Show()
        {
            using (CroppingWindow = new OpenCvSharp.Window("Cropper", WindowMode.AutoSize, srcImage))
            {

                CvMouseCallback onMouse = new CvMouseCallback(mouseCallback);
                CroppingWindow.SetMouseCallback(onMouse);
                CvTrackbarCallback2 onZoom = new CvTrackbarCallback2(trackbarCallback);
                CvTrackbar zoom = CroppingWindow.CreateTrackbar2("Zoom", 100, 200, onZoom, null);
                Cv2.WaitKey();
            }
            // seems that srcImage will be released by GC, so I must return a copy of it
            return srcImage.Clone();
        }

        private void trackbarCallback(int pos, object data)
        {
            if (pos == 0)
                return;
            double factor = (double)pos / 100;
            srcImage = baseImage.Resize(new Size(0,0), factor, factor);
            CroppingWindow.ShowImage(srcImage);
        }

        private void mouseCallback(MouseEvent e, int x, int y, MouseEvent args, IntPtr data)
        {
            // When the left mouse button is pressed, record its position and save it in ltConor
            if (e == MouseEvent.LButtonDown)
            {
                down = true;
                conor1.X = Math.Max(0, x);
                conor1.Y = Math.Max(0, y);
            }

            // When the left mouse button is released, record its position and save it in rbConor
            if (e == MouseEvent.LButtonUp)
            {
                up = true;
                conor2.X = Math.Max(0, Math.Min(x, srcImage.Width - 1));
                conor2.Y = Math.Max(0, Math.Min(y, srcImage.Height - 1));
            }

            // Update the box showing the selected region as the user drags the mouse
            if (down == true && up == false)
            {
                conor2.X = Math.Min(x, srcImage.Width - 1);
                conor2.Y = Math.Min(y, srcImage.Height - 1);
                srcImage.CopyTo(currentImage);
                if (conor1.X <= conor2.X && conor1.Y <= conor2.Y)
                    Cv2.Rectangle(currentImage, conor1, conor2, new Scalar(0, 0, 255), 3);
                else if (conor1.X >= conor2.X && conor1.Y >= conor2.Y)
                    Cv2.Rectangle(currentImage, conor2, conor1, new Scalar(0, 0, 255), 3);
                else if (conor1.X >= conor2.X && conor1.Y <= conor2.Y)
                    Cv2.Rectangle(currentImage, new OpenCvSharp.Point(conor2.X, conor1.Y), new OpenCvSharp.Point(conor1.X, conor2.Y), new Scalar(0, 0, 255), 3);
                else
                    Cv2.Rectangle(currentImage, new OpenCvSharp.Point(conor1.X, conor2.Y), new OpenCvSharp.Point(conor2.X, conor1.Y), new Scalar(0, 0, 255), 3);
                CroppingWindow.ShowImage(currentImage);
            }

            // Define ROI and crop it out when both corners have been selected
            // ROI : region of interest
            if (down == true && up == true)
            {
                down = false;
                up = false;
                box.Width = Math.Abs(conor1.X - conor2.X);
                box.Height = Math.Abs(conor1.Y - conor2.Y);
                box.X = Math.Min(conor1.X, conor2.X);
                box.Y = Math.Min(conor1.Y, conor2.Y);

                // region is too small
                if (box.Width < 20 || box.Height < 20)
                {
                    CroppingWindow.ShowImage(srcImage);
                    return;
                }

                // Make an image out of just the selected ROI and display it in a new window
                Mat croppedImage = new Mat(srcImage, box);
                srcImage = croppedImage;
                baseImage = srcImage.Clone();
                CroppingWindow.ShowImage(srcImage);
            }
        }
    }
}
