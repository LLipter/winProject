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

        private void btnChooseImage1_Click(object sender, RoutedEventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lblPath1.Content = openFileDialog.FileName;
                openFileDialog.FileName = "";
            }
        }

        private void btnChooseImage2_Click(object sender, RoutedEventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lblPath2.Content = openFileDialog.FileName;
                openFileDialog.FileName = "";
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lblSavePath.Content = saveFileDialog.FileName;
                saveFileDialog.FileName = "";
            }
        }

        private void btnStich_Click(object sender, RoutedEventArgs e)
        {
            if ((string)lblPath1.Content == string.Empty)
            {
                MessageBox.Show("Please choose the first image", "Error");
                return;
            }

            if ((string)lblPath2.Content == string.Empty)
            {
                MessageBox.Show("Please choose the second image", "Error");
                return;
            }

            if ((string)lblSavePath.Content == string.Empty)
            {
                MessageBox.Show("Please choose save location", "Error");
                return;
            }

            Mat src1Color, src2Color;
            try
            {
                src1Color = new Mat((string)lblPath1.Content, ImreadModes.Color);
                src2Color = new Mat((string)lblPath2.Content, ImreadModes.Color);
            }
            catch (Exception)
            {
                MessageBox.Show("File open error", "Error");
                return;
            }
            Mat src1Gray = new Mat();
            Mat src2Gray = new Mat();
            Cv2.CvtColor(src1Color, src1Gray, ColorConversionCodes.BGR2GRAY);
            Cv2.CvtColor(src2Color, src2Gray, ColorConversionCodes.BGR2GRAY);

            // Setting hyperparameters
            int numBestMatch = 10;

            // Detect the keypoints and generate their descriptors using SIFT
            SIFT sift = SIFT.Create();
            KeyPoint[] keypoints1, keypoints2;
            MatOfFloat descriptors1 = new MatOfFloat();
            MatOfFloat descriptors2 = new MatOfFloat();
            sift.DetectAndCompute(src1Gray, null, out keypoints1, descriptors1);
            sift.DetectAndCompute(src2Gray, null, out keypoints2, descriptors2);

            // Matching descriptor vectors with a brute force matcher
            BFMatcher matcher = new BFMatcher();
            DMatch[] matches = matcher.Match(descriptors1, descriptors2);

            // Sort the match points
            Comparison<DMatch> DMatchComparison = delegate (DMatch match1, DMatch match2)
            {
                if (match1 < match2)
                    return -1;
                else
                    return 1;
            };
            Array.Sort(matches, DMatchComparison);

            // Get the best n match points
            int n = Math.Min(numBestMatch, keypoints1.Length);
            Point2f[] imagePoints1 = new Point2f[n];
            Point2f[] imagePoints2 = new Point2f[n];
            DMatch[] bestMatches = new DMatch[n];
            for (int i = 0; i < n; i++)
            {
                imagePoints1[i] = keypoints1[matches[i].QueryIdx].Pt;
                imagePoints2[i] = keypoints2[matches[i].TrainIdx].Pt;
                bestMatches[i] = matches[i];
            }

            // visiualize match result
            Mat matchImg = new Mat();
            Cv2.DrawMatches(src1Color, keypoints1, src2Color, keypoints2, bestMatches, matchImg, Scalar.All(-1), Scalar.All(-1), null, DrawMatchesFlags.NotDrawSinglePoints);
            using (new OpenCvSharp.Window("SIFT matching", WindowMode.Normal, matchImg))
            {
                Cv2.WaitKey();
            }

            // Get homographic matrix that represents a transformation.
            // The size of such matrix is 3x3, which can represents every possible matrix transformation in 2-D plane.
            Mat homo = Cv2.FindHomography(InputArray.Create<Point2f>(imagePoints2), InputArray.Create<Point2f>(imagePoints1));

            // calculate the transformed position of the second image's conor
            // use this value to calculate the size of result image
            Point2f[] transfromedConors = transfromConors(src2Color.Size(), homo);

            // if the second image is on the left or up side of the first image
            // exchange them and recompute the homography map matrix
            for (int i = 0; i < 4; i++)
            {
                if (transfromedConors[i].X < 0 || transfromedConors[i].Y < 0)
                {
                    Mat temp;
                    temp = src1Color;
                    src1Color = src2Color;
                    src2Color = temp;
                    temp = src1Gray;
                    src1Gray = src2Gray;
                    src2Gray = temp;
                    homo = Cv2.FindHomography(InputArray.Create<Point2f>(imagePoints1), InputArray.Create<Point2f>(imagePoints2));
                    transfromedConors = transfromConors(src2Color.Size(), homo);
                    break;
                }

            }

            // make sure the result image is large enough
            double maxWidth = src1Color.Width;
            double maxHeight = src1Color.Height;
            for (int i = 0; i < 4; i++)
            {
                if (transfromedConors[i].X > maxWidth)
                    maxWidth = transfromedConors[i].X;
                if (transfromedConors[i].Y > maxHeight)
                    maxHeight = transfromedConors[i].Y;
            }
            OpenCvSharp.Size resultSize = new OpenCvSharp.Size(maxWidth, maxHeight);

            Mat result = new Mat();
            Cv2.WarpPerspective(src2Color, result, homo, resultSize);
            src1Color.CopyTo(new Mat(result, new OpenCvSharp.Rect(0, 0, src1Gray.Cols, src1Gray.Rows)));
            result.SaveImage((string)lblSavePath.Content);

            using (new OpenCvSharp.Window("Stitch Result", WindowMode.Normal, result))
            {
                MessageBox.Show("ok", "Save result");
            }


        }

        private static Point2f[] transfromConors(OpenCvSharp.Size size, Mat homo)
        {
            Point2f leftTop = new Point2f(0, 0);
            Point2f leftBottom = new Point2f(0, size.Height - 1);
            Point2f rightTop = new Point2f(size.Width - 1, 0);
            Point2f rightBottom = new Point2f(size.Width - 1, size.Height - 1);
            Point2f[] conors = { leftTop, leftBottom, rightTop, rightBottom };
            return Cv2.PerspectiveTransform(conors, homo);
        }
    }
}
