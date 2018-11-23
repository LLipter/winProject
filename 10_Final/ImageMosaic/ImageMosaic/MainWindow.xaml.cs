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
using OpenCvSharp.XFeatures2D;


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
            Mat src1 = new Mat("C:\\Users\\99645\\Desktop\\11.jpg", ImreadModes.GrayScale);
            Mat src2 = new Mat("C:\\Users\\99645\\Desktop\\22.jpg", ImreadModes.GrayScale);
            /*
            using (new OpenCvSharp.Window("1", src1))
            using (new OpenCvSharp.Window("2", src2))
            {
                Cv2.WaitKey();
            }
            */

            // Setting hyperparameters
            int numBestMatch = 100;
            
            // Detect the keypoints and generate their descriptors using SIFT
            SIFT sift = SIFT.Create();
            KeyPoint[] keypoints1, keypoints2;
            MatOfFloat descriptors1 = new MatOfFloat();
            MatOfFloat descriptors2 = new MatOfFloat();
            sift.DetectAndCompute(src1, null, out keypoints1, descriptors1);
            sift.DetectAndCompute(src2, null, out keypoints2, descriptors2);

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
            // The size of such matrix is 3x3, which can represents enery possible matrix transformation in 2-D plane.
            Mat homo = Cv2.FindHomography(InputArray.Create<Point2f>(imagePoints2), InputArray.Create<Point2f>(imagePoints1));

            
            Mat result = new Mat();
            //OpenCvSharp.Size resultSize = new OpenCvSharp.Size()
            Cv2.WarpPerspective(src2, result, homo, new OpenCvSharp.Size(2 * src2.Cols, src2.Rows));
            result.SaveImage("C:\\Users\\99645\\Desktop\\result11.jpg");
            src1.CopyTo( new Mat(result, new OpenCvSharp.Rect(0, 0, src1.Cols, src1.Rows)));
            result.SaveImage("C:\\Users\\99645\\Desktop\\result12.jpg");







        }
    }
}
