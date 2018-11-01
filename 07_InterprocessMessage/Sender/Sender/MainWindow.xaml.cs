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
        private static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        const int WM_COPYDATA = 0x004a;

        public MainWindow()
        {
            InitializeComponent();
        }

        const int MAX_LENGTH = 100;
        private void btnSendMsg_Click(object sender, RoutedEventArgs e)
        {
            IntPtr hWnd = FindWindow(null, "Receiver");
            if (hWnd == IntPtr.Zero)
            {
                MessageBox.Show("cannot find handler");
                return;
            }
            // MessageBox.Show(IntPtr.Size.ToString());
            
            COPYDATASTRUCT data = new COPYDATASTRUCT();
            byte[] bytes = Encoding.Default.GetBytes(txtMsg.Text);
            // MessageBox.Show(bytes.Length.ToString());
            data.data = new byte[MAX_LENGTH];
            int len = MAX_LENGTH < bytes.Length ? MAX_LENGTH : bytes.Length;
            for (int i = 0; i < len; i++)
                data.data[i] = bytes[i];

            int nSize = Marshal.SizeOf(data);
            IntPtr ptr = Marshal.AllocHGlobal(nSize);
            Marshal.StructureToPtr(data, ptr, true);
            int res = SendMessage(hWnd, WM_COPYDATA, new IntPtr(bytes.Length), ptr);
            // MessageBox.Show(res.ToString());
            

            /*
            int res = SendMessage(hWnd, WM_COPYDATA, new IntPtr(0), Marshal.StringToHGlobalAnsi(txtMsg.Text));
            */

        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public class COPYDATASTRUCT
    {
        const int MAX_LENGTH = 100;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_LENGTH)]
        public byte[] data;
    }
    
}
