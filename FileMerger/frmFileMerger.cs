using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileMerger
{
    public partial class frmFileMerger : Form
    {

        public frmFileMerger()
        {
            InitializeComponent();
        }

        private void btnChooseDir_Click(object sender, EventArgs e)
        {
            // folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            if(folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                lblCurrentDir.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(lblCurrentDir.Text))
            {
                string[] files = Directory.GetFiles(lblCurrentDir.Text, txtFileNamePtn.Text, SearchOption.AllDirectories);
                lsbfiles.Items.Clear();
                lsbfiles.Items.AddRange(files);
            }
        }

        private void btnAddtoSet_Click(object sender, EventArgs e)
        {
            if(lsbfiles.SelectedIndex != -1)
            {
                lsbSelectedFiles.Items.Add(lsbfiles.SelectedItem);
            }
        }

        private void btnClearSet_Click(object sender, EventArgs e)
        {
            lsbSelectedFiles.Items.Clear();
        }

        private void btnRemoveFromSet_Click(object sender, EventArgs e)
        {
            if (lsbSelectedFiles.SelectedIndex != -1)
            {
                lsbSelectedFiles.Items.RemoveAt(lsbSelectedFiles.SelectedIndex);
            }
        }

        private void btnAddSingleFile_Click(object sender, EventArgs e)
        {
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                lsbSelectedFiles.Items.AddRange(openFileDialog.FileNames);
            }
        }

        private void btnMoveUpward_Click(object sender, EventArgs e)
        {
            int index = lsbSelectedFiles.SelectedIndex;
            string filename = lsbSelectedFiles.SelectedItem.ToString();
            if (index > 0)
            {
                lsbSelectedFiles.Items[index] = lsbSelectedFiles.Items[index - 1];
                lsbSelectedFiles.Items[index - 1] = filename;
                lsbSelectedFiles.SetSelected(index - 1, true);
            }
        }

        private void btnMoveDownward_Click(object sender, EventArgs e)
        {
            int index = lsbSelectedFiles.SelectedIndex;
            string filename = lsbSelectedFiles.SelectedItem.ToString();
            if (index != -1 && index < lsbSelectedFiles.Items.Count - 1)
            {
                lsbSelectedFiles.Items[index] = lsbSelectedFiles.Items[index + 1];
                lsbSelectedFiles.Items[index + 1] = filename;
                lsbSelectedFiles.SetSelected(index + 1, true);
            }
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            // saveFileDialog.InitialDirectory = Environment.SpecialFolder.DesktopDirectory.ToString();

            if(saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string filepath = saveFileDialog.FileName;
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }

            FileStream fs_dest = new FileStream(filepath, FileMode.CreateNew, FileAccess.Write);
            FileStream fs_source = null;
            int buf_size = 1024 * 1024;
            byte[] data_buf = new byte[buf_size];
            byte[] filename_buf;
 
            foreach(Object file in lsbSelectedFiles.Items)
            {
                string path = file.ToString();
                FileInfo file_info = new FileInfo(path);

                // write file name
                filename_buf = Encoding.Default.GetBytes(file_info.Name);
                fs_dest.Write(filename_buf, 0, filename_buf.Length);
                fs_dest.WriteByte((byte)13);
                fs_dest.WriteByte((byte)10);

                // write file data
                fs_source = new FileStream(file_info.FullName, FileMode.Open, FileAccess.Read);
                int read_len = fs_source.Read(data_buf, 0, buf_size);
                while(read_len > 0)
                {
                    fs_dest.Write(data_buf, 0, read_len);
                    read_len = fs_source.Read(data_buf, 0, buf_size);
                }
                fs_dest.WriteByte((byte)13);
                fs_dest.WriteByte((byte)10);

            }

            fs_dest.Close();
        }
    }
}
