using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Resources;
using System.Windows.Shapes;

namespace ImageStitcher
{
    /// <summary>
    /// DemoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DemoWindow : Window
    {
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;

        public DemoWindow()
        {
            InitializeComponent();
            folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
        }

        private void btnDownload_Click(object sender, RoutedEventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    SaveResourceTo("1-1.png", folderBrowserDialog.SelectedPath);
                    SaveResourceTo("1-2.png", folderBrowserDialog.SelectedPath);
                    SaveResourceTo("1-3.png", folderBrowserDialog.SelectedPath);
                    SaveResourceTo("2-1.png", folderBrowserDialog.SelectedPath);
                    SaveResourceTo("2-2.png", folderBrowserDialog.SelectedPath);
                    SaveResourceTo("2-3.png", folderBrowserDialog.SelectedPath);
                    SaveResourceTo("3-1.jpg", folderBrowserDialog.SelectedPath);
                    SaveResourceTo("3-2.jpg", folderBrowserDialog.SelectedPath);
                    SaveResourceTo("3-3.jpg", folderBrowserDialog.SelectedPath);
                    SaveResourceTo("4-1.jpg", folderBrowserDialog.SelectedPath);
                    SaveResourceTo("4-2.jpg", folderBrowserDialog.SelectedPath);
                    SaveResourceTo("4-3.jpg", folderBrowserDialog.SelectedPath);
                    MessageBox.Show("ok", "Save Result");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
        }

        private void SaveResourceTo(string resPath, string targetDir)
        {
            Uri uri = new Uri("Resources/" + resPath, UriKind.Relative);
            using(Stream input = Application.GetResourceStream(uri).Stream)
            {
                using(FileStream output = new FileStream(targetDir + "\\" + resPath, FileMode.Create))
                {
                    input.CopyTo(output);
                }
            }
        }


    }
}
