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
        private static extern int SendMessage(int hWnd, int Msg, int wParam, ref COPYDATASTRUCT lParam);

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private extern static int FindWindow(string lpClassName, string lpWindowName);

        const int WM_COPYDATA = 0x004A;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSendMsg_Click(object sender, RoutedEventArgs e)
        {
            int hWnd = FindWindow(null, "Receiver");
            if (hWnd == 0)
            {
                MessageBox.Show("cannot find handler");
                return;
            }

            string s = "hELLO";
            byte[] sarr = System.Text.Encoding.Default.GetBytes(s);
            int len = sarr.Length;
            Console.WriteLine(len);
            COPYDATASTRUCT cds2 = new COPYDATASTRUCT();
            cds2.dwData = (IntPtr)0;
            cds2.cbData = len + 1;
            cds2.lpData = s;

            SendMessage(hWnd, WM_COPYDATA, 7614, ref cds2);
        }
    }

    
    public class COPYDATASTRUCT
    {
        public IntPtr dwData; // 任意值
        public int cbData;    // 指定lpData内存区域的字节数
        [MarshalAs(UnmanagedType.LPStr)]
        public string lpData; // 发送给目标窗口所在进程的数据
    }
    
}
