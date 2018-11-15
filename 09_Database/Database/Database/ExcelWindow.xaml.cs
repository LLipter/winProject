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
using System.Windows.Forms;
using System.IO;

namespace Database
{
    /// <summary>
    /// ExcelWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ExcelWindow : Window
    {
        private OpenFileDialog openFileDialog;
        private string openedFilePath;
        private string openedFileExtention;

        public ExcelWindow()
        {
            InitializeComponent();
            openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "D:\\workspace\\vs\\winProject\\09_Database";
            openFileDialog.Filter = "excel files(*.xls)| *.xls";
            openFileDialog.FilterIndex = 0;
            openFileDialog.RestoreDirectory = false;
        }

        private string getConnectionStr(string filepath, string extension)
        {
            string connectionString = string.Empty;
            switch (extension)
            {
                case ".xls":
                    connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties='Excel 8.0;HDR=yes;'";
                    break;
                case ".xlsx":
                    connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filepath + ";Extended Properties='Excel 12.0;HDR=yes;'";
                    break;
                default:
                    connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties='Excel 8.0;HDR=yes;'";
                    break;
            }
            return connectionString;
        }

        private void btnChooseFile_Click(object sender, RoutedEventArgs e)
        {
            // Window.DialogResult
            // System.Windows.Forms.DialogResult
            // Note that there're two class with the same, please use full name to distinguish them
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lblFilePath.Content = openFileDialog.FileName;
            }
        }

        private void btnLoadSheet_Click(object sender, RoutedEventArgs e)
        {
            string filepath = (string)lblFilePath.Content;
            if (filepath == "")
            {
                System.Windows.MessageBox.Show("File path required!");
                return;
            }
            FileInfo file = new FileInfo(filepath);
            if (!file.Exists)
            {
                System.Windows.MessageBox.Show("File doesn't exist!");
                return;
            }

            string sheetName = txtSheetName.Text;
            if (sheetName == "")
            {
                System.Windows.MessageBox.Show("Sheet name required!");
                return;
            }

            string connectionString = getConnectionStr(filepath, file.Extension);
            System.Data.DataTable dt = new System.Data.DataTable();
            using (System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection(connectionString))
            {
                string sql = string.Format("SELECT * FROM [{0}$]", sheetName);
                System.Data.OleDb.OleDbDataAdapter apt = new System.Data.OleDb.OleDbDataAdapter(sql, conn);
                try
                {
                    apt.Fill(dt);
                }
                catch (Exception) {
                    System.Windows.MessageBox.Show("no such sheet!");
                    return;
                }
                dt.TableName = sheetName;
                grdData.ItemsSource = dt.DefaultView;
            }

            openedFilePath = filepath;
            openedFileExtention = file.Extension;
        }

        private void btnRemoveLine_Click(object sender, RoutedEventArgs e)
        {
            if (grdData.SelectedItem == null)
            {
                System.Windows.MessageBox.Show("Please select a line");
                return;
            }
            System.Data.DataRowView drv = (System.Data.DataRowView)grdData.SelectedItem;
            drv.Delete();

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // remove original file
            try
            {
                File.Delete(openedFilePath);
            }catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return;
            }
            
            string connectionString = getConnectionStr(openedFilePath, openedFileExtention);
            using (System.Data.OleDb.OleDbConnection con = new System.Data.OleDb.OleDbConnection(connectionString))
            {
                con.Open();
                StringBuilder strSQL = new StringBuilder();
                System.Data.OleDb.OleDbCommand cmd;
                System.Data.DataTable dt = ((System.Data.DataView)grdData.ItemsSource).Table;

                // create sheet/table
                strSQL.Append("CREATE TABLE ").Append("[" + dt.TableName + "]");
                strSQL.Append("(");
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    strSQL.Append("[" + dt.Columns[i].ColumnName + "] text,");
                }
                strSQL = strSQL.Remove(strSQL.Length - 1, 1);
                strSQL.Append(")");
                cmd = new System.Data.OleDb.OleDbCommand(strSQL.ToString(), con);
                cmd.ExecuteNonQuery();

                // insert data line by line
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if(dt.Rows[i].RowState == System.Data.DataRowState.Deleted)
                        continue;
                    strSQL.Clear();
                    StringBuilder strvalue = new StringBuilder();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        strvalue.Append("'" + dt.Rows[i][j].ToString() + "'");
                        if (j != dt.Columns.Count - 1)
                            strvalue.Append(",");
                    }
                    cmd.CommandText = strSQL.Append(" insert into [" + dt.TableName + "] values (").Append(strvalue).Append(")").ToString();
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }

            System.Windows.MessageBox.Show("save successfully");
        }
    }
}
