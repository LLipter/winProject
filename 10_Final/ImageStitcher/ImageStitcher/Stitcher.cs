using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using OpenCvSharp.XFeatures2D;

namespace ImageStitcher
{
    class Stitcher
    {
        private Mat src1Color;
        private Mat src2Color;

        public Stitcher(Mat src1Color, Mat src2Color)
        {
            this.src1Color = src1Color;
            this.src2Color = src2Color;
        }

        public Mat Stitch()
        {
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
            using (new OpenCvSharp.Window("SIFT matching", WindowMode.AutoSize, matchImg))
            {
                Cv2.WaitKey();
            }

            // Get homographic matrix that represents a transformation.
            // The size of such matrix is 3x3, which can represents every possible matrix transformation in 2-D plane.
            Mat homo = Cv2.FindHomography(InputArray.Create<Point2f>(imagePoints2), InputArray.Create<Point2f>(imagePoints1));

            // calculate the transformed location of the second image's conor
            // use this value to calculate the size of result image
            Point2f[] transfromedConors = transfromConors(src2Color.Size(), homo);

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

            // the position that the first image should be copied to in the final result
            int src1StartPositonY = 0;
            int src1StartPositonX = 0;

            // if still some X coordinate is less than 0, do shift operation along x-axis
            bool shouldShiftX = false;
            double shiftDistanceX = double.MinValue;
            for (int i = 0; i < 4; i++)
            {
                if (transfromedConors[i].X < 0)
                {
                    shouldShiftX = true;
                    shiftDistanceX = Math.Max(shiftDistanceX, -transfromedConors[i].X);
                }
            }
            if (shouldShiftX)
            {
                /*
                 * matrix for shifting algong x-axis
                 * 1 0 d
                 * 0 1 0
                 * 0 0 1
                 */
                Mat shiftMatrix = new Mat(3, 3, homo.Type());
                shiftMatrix.Set<double>(0, 0, 1);
                shiftMatrix.Set<double>(0, 1, 0);
                shiftMatrix.Set<double>(0, 2, shiftDistanceX);
                shiftMatrix.Set<double>(1, 0, 0);
                shiftMatrix.Set<double>(1, 1, 1);
                shiftMatrix.Set<double>(1, 2, 0);
                shiftMatrix.Set<double>(2, 0, 0);
                shiftMatrix.Set<double>(2, 1, 0);
                shiftMatrix.Set<double>(2, 2, 1);
                homo = shiftMatrix * homo;
                resultSize.Width = resultSize.Width + (int)shiftDistanceX;
                src1StartPositonX = (int)shiftDistanceX;
            }

            // if still some Y coordinate is less than 0, do shift operation along y-axis
            bool shouldShiftY = false;
            double shiftDistanceY = double.MinValue;
            for (int i = 0; i < 4; i++)
            {
                if (transfromedConors[i].Y < 0)
                {
                    shouldShiftY = true;
                    shiftDistanceY = Math.Max(shiftDistanceY, -transfromedConors[i].Y);
                }
            }
            if (shouldShiftY)
            {
                /*
                 * matrix for shifting algong y-axis
                 * 1 0 0
                 * 0 1 d
                 * 0 0 1
                 */
                Mat shiftMatrix = new Mat(3, 3, homo.Type());
                shiftMatrix.Set<double>(0, 0, 1);
                shiftMatrix.Set<double>(0, 1, 0);
                shiftMatrix.Set<double>(0, 2, 0);
                shiftMatrix.Set<double>(1, 0, 0);
                shiftMatrix.Set<double>(1, 1, 1);
                shiftMatrix.Set<double>(1, 2, shiftDistanceY);
                shiftMatrix.Set<double>(2, 0, 0);
                shiftMatrix.Set<double>(2, 1, 0);
                shiftMatrix.Set<double>(2, 2, 1);
                homo = shiftMatrix * homo;
                resultSize.Height = resultSize.Height + (int)shiftDistanceY;
                src1StartPositonY = (int)shiftDistanceY;
            }

            Mat result = new Mat();
            Cv2.WarpPerspective(src2Color, result, homo, resultSize);
            src1Color.CopyTo(new Mat(result, new OpenCvSharp.Rect(src1StartPositonX, src1StartPositonY, src1Gray.Cols, src1Gray.Rows)));

            return result;
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
