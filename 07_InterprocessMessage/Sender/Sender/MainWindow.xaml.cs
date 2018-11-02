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

            // Solution 1 (failed to pass message larger than 8 bytes)
            /*
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
            Marshal.FreeHGlobal(ptr);
            */


            // Solution 2 (failed to pass message larger than 4 bytes)
            /*
            int res = SendMessage(hWnd, WM_COPYDATA, new IntPtr(0), Marshal.StringToHGlobalAnsi(txtMsg.Text));
            */

            // Solution 3 (also failed to pass message larger than 4 bytes)
            /*
            byte[] bytes = Encoding.Default.GetBytes(txtMsg.Text);
            IntPtr ptr = Marshal.AllocHGlobal(bytes.Length);
            Marshal.Copy(bytes, 0, ptr, bytes.Length);
            SendMessage(hWnd, WM_COPYDATA, (IntPtr)bytes.Length, ptr);
            Marshal.FreeHGlobal(ptr);
            */

            // Solution 4, try solution 1-3 multiplu times. Only pass a small piece of message in a single message
            // failed again, and I don't know why
            /*
            byte[] bytes = Encoding.Default.GetBytes(txtMsg.Text);
            // signal of starting transmitting
            SendMessage(hWnd, WM_COPYDATA, (IntPtr)999, (IntPtr)bytes.Length);
            int index = 0;
            while(index < bytes.Length)
            {
                IntPtr ptr = Marshal.AllocHGlobal(4);
                if (index + 4 < bytes.Length)
                {
                    Marshal.Copy(bytes, index, ptr, 4);
                    SendMessage(hWnd, WM_COPYDATA, (IntPtr)4, ptr);
                    index += 4;
                }
                else
                {
                    int diff = bytes.Length - index;
                    Marshal.Copy(bytes, index, ptr, diff);
                    SendMessage(hWnd, WM_COPYDATA, (IntPtr)diff, ptr);
                    index += diff;
                }
                Marshal.FreeHGlobal(ptr);
            }
            // signal of ending transmitting
            SendMessage(hWnd, WM_COPYDATA, (IntPtr)999, (IntPtr)999);
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
