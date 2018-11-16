using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Database
{
    /// <summary>
    /// MysqlWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MysqlWindow : Window
    {
        private string connStr = string.Empty;

        public MysqlWindow()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            if(txtHost.Text == "")
            {
                System.Windows.MessageBox.Show("Host required!");
                return;
            }
            if (txtPort.Text == "")
            {
                System.Windows.MessageBox.Show("Port required!");
                return;
            }
            if (txtUsername.Text == "")
            {
                System.Windows.MessageBox.Show("Username required!");
                return;
            }
            if (txtDatabase.Text == "")
            {
                System.Windows.MessageBox.Show("Database required!");
                return;
            }
            if (txtPassword.Password == "")
            {
                System.Windows.MessageBox.Show("Password required!");
                return;
            }
            if (txtTable.Text == "")
            {
                System.Windows.MessageBox.Show("Table required!");
                return;
            }

            connStr = string.Format("Database={0};Data Source={1};User Id={2};Password={3};port={4};Charset=utf8",
                txtDatabase.Text,
                txtHost.Text,
                txtUsername.Text,
                txtPassword.Password,
                txtPort.Text);

            using(MySqlConnection conn = new MySqlConnection(connStr))
            {
                string sql = "select * from " + txtTable.Text;
                DataTable dt = new DataTable();
                MySqlDataAdapter apt = new MySqlDataAdapter(sql, conn);
                try
                {
                    apt.Fill(dt);
                }catch(Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                    return;
                }
                dt.TableName = txtTable.Text;
                grdData.ItemsSource = dt.DefaultView;

            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (grdData.SelectedItem == null)
            {
                System.Windows.MessageBox.Show("Please select a line");
                return;
            }
            System.Data.DataRowView drv = (System.Data.DataRowView)grdData.SelectedItem;
            drv.Delete();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if(connStr == string.Empty)
            {
                System.Windows.MessageBox.Show("No database connected");
                return;
            }
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                string sql = "select * from " + txtTable.Text;
                MySqlDataAdapter apt = new MySqlDataAdapter(sql, conn);
                apt.SelectCommand = new MySqlCommand(sql, conn);
                MySqlCommandBuilder cb = new MySqlCommandBuilder(apt);
                DataTable dt = ((DataView)grdData.ItemsSource).Table;
                try
                {
                    apt.Update(dt);
                }catch(Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                    return;
                }
               
            }

            System.Windows.MessageBox.Show("update successfully");
        }
    }
}
