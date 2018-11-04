using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace Sender
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, ref COPYDATASTRUCT lParam);

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        const int WM_COPYDATA = 0x004a;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSendMsg_Click(object sender, RoutedEventArgs e)
        {
            IntPtr hWnd = FindWindow(null, "Receiver");
            if (hWnd == IntPtr.Zero)
            {
                MessageBox.Show("cannot find handler");
                return;
            }
            string s = txtMsg.Text;
            byte[] bytes = Encoding.Default.GetBytes(s);
            int length = bytes.Length;
            COPYDATASTRUCT cds;
            cds.dwData = (IntPtr)0;
            cds.cbData = length + 1;
            cds.lpData = s;
            SendMessage(hWnd, WM_COPYDATA, (IntPtr)0, ref cds);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct COPYDATASTRUCT
    {
        public IntPtr dwData;
        public int cbData;
        [MarshalAs(UnmanagedType.LPStr)]
        public string lpData;
    }

}
