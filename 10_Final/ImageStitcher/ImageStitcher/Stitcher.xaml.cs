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
    public partial class Stitcher : System.Windows.Window
    {
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;

        public Stitcher()
        {
            InitializeComponent();
            openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.Filter = "JPEG file(*.jpg)| *.jpg;*.jpeg | PNG file(*.png)| *.png";
            openFileDialog.FilterIndex = 0;

            saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.Filter = "image files(*.jpg)| *.jpg;*.jpeg";
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
            if((string)lblPath1.Content == string.Empty)
            {
                MessageBox.Show("Please choose the first image");
                return;
            }

            if ((string)lblPath2.Content == string.Empty)
            {
                MessageBox.Show("Please choose the second image");
                return;
            }

            if ((string)lblSavePath.Content == string.Empty)
            {
                MessageBox.Show("Please choose save location");
                return;
            }

            Mat src1Color, src2Color;
            try
            {
                src1Color = new Mat((string)lblPath1.Content, ImreadModes.Color);
                src2Color = new Mat((string)lblPath2.Content, ImreadModes.Color);
            }catch(Exception ex){
                MessageBox.Show(ex.Message);
                return;
            }
            Mat src1Gray = new Mat();
            Mat src2Gray = new Mat();
            Cv2.CvtColor(src1Color, src1Gray, ColorConversionCodes.BGR2GRAY);
            Cv2.CvtColor(src2Color, src2Gray, ColorConversionCodes.BGR2GRAY);

            // Setting hyperparameters
            int numBestMatch = 100;

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
            Point2f[] imagePoints1 = new Point2f[numBestMatch];
            Point2f[] imagePoints2 = new Point2f[numBestMatch];
            DMatch[] bestMatches = new DMatch[numBestMatch];
            for (int i = 0; i < numBestMatch; i++)
            {
                imagePoints1[i] = keypoints1[matches[i].QueryIdx].Pt;
                imagePoints2[i] = keypoints2[matches[i].TrainIdx].Pt;
                bestMatches[i] = matches[i];
            }

            // visiualize match result
            /*
            Mat matchImg = new Mat();
            Cv2.DrawMatches(src1, keypoints1, src2, keypoints2, bestMatches, matchImg, Scalar.All(-1), Scalar.All(-1), null, DrawMatchesFlags.NotDrawSinglePoints);
            using (new OpenCvSharp.Window("SIFT matching", WindowMode.AutoSize, matchImg))
            {
                Cv2.WaitKey();
            }
            */

            // Get homographic matrix that represents a transformation.
            // The size of such matrix is 3x3, which can represents every possible matrix transformation in 2-D plane.
            Mat homo = Cv2.FindHomography(InputArray.Create<Point2f>(imagePoints2), InputArray.Create<Point2f>(imagePoints1));

            // calculate the transformed position of the second image's right bottom conor
            // use this value to calculate the size of result image
            Mat rightBottomConor = new Mat(3, 1, MatType.CV_64FC1);
            rightBottomConor.Set<double>(0, 0, src2Gray.Cols);
            rightBottomConor.Set<double>(1, 0, src2Gray.Rows);
            rightBottomConor.Set<double>(2, 0, 1);
            Mat transformedConor = homo * rightBottomConor;
            // ??????
            // Why transformedConor.Get<double>(2, 0) is not 1 ????
            Point2d transformedPoint = new Point2d(transformedConor.Get<double>(0, 0) / transformedConor.Get<double>(2, 0),
                                                    transformedConor.Get<double>(0, 1) / transformedConor.Get<double>(2, 0));
            OpenCvSharp.Size resultSize = new OpenCvSharp.Size(Math.Max(src1Gray.Cols, transformedPoint.X), Math.Max(src1Gray.Rows, transformedPoint.Y));


            Mat result = new Mat();
            Cv2.WarpPerspective(src2Color, result, homo, resultSize);
            src1Color.CopyTo(new Mat(result, new OpenCvSharp.Rect(0, 0, src1Gray.Cols, src1Gray.Rows)));
            result.SaveImage((string)lblSavePath.Content);

            MessageBox.Show("ok");
        }
    }
}
