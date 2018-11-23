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
    public partial class MainWindow : Window
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

        }


        static void mouse_callback(int event, int x, int y, int, void*)
{
	// When the left mouse button is pressed, record its position and save it in corner1
	if(event == EVENT_LBUTTONDOWN)

    {
            ldown = true;
            corner1.x = x;
            corner1.y = y;
            cout << "Corner 1 recorded at " << corner1 << endl;
        }
	// When the left mouse button is released, record its position and save it in corner2
	if(event == EVENT_LBUTTONUP)

    {
            // Also check if user selection is bigger than 20 pixels (jut for fun!)
            if (abs(x - corner1.x) > 20 && abs(y - corner1.y) > 20)
            {
                lup = true;
                corner2.x = x;
                corner2.y = y;
                cout << "Corner 2 recorded at " << corner2 << endl << endl;
            }
            else
            {
                cout << "Please select a bigger region" << endl;
                ldown = false;
            }
        }
	// Update the box showing the selected region as the user drags the mouse
	if(ldown == true && lup == false)
	{
		Point pt;
        pt.x = x;
		pt.y = y;
		Mat local_img = img.clone();
        rectangle(local_img, corner1, pt, Scalar(0, 0, 255));
		imshow("Cropping app", local_img);
    }
	// Define ROI and crop it out when both corners have been selected
	if(ldown == true && lup == true)
	{
		box.width = abs(corner1.x - corner2.x);
    box.height = abs(corner1.y - corner2.y);
    box.x = min(corner1.x, corner2.x);
    box.y = min(corner1.y, corner2.y);
    // Make an image out of just the selected ROI and display it in a new window
    Mat crop(img, box);
    namedWindow("Crop");
    imshow("Crop", crop);
    ldown = false;
		lup = false;
	}
}
    }
}
