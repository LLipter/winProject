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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Receiver
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

        const int WM_COPYDATA = 0x004a;

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            HwndSource hwndSource = PresentationSource.FromVisual(this) as HwndSource;
            if (hwndSource != null)
            {
                IntPtr handle = hwndSource.Handle;
                hwndSource.AddHook(new HwndSourceHook(WndProc));
            }
        }

        private byte[] bytes;
        private int index;

        IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            Console.WriteLine(msg.ToString());
            if (msg == WM_COPYDATA)
            {
                // Solution 1 (failed to pass message larger than 8 bytes)
                /*
                COPYDATASTRUCT data = (COPYDATASTRUCT)Marshal.PtrToStructure(lParam, typeof(COPYDATASTRUCT)); 
                // MessageBox.Show(wParam.ToString());
                string str = Encoding.Default.GetString(data.data,0,(int)wParam);
                MessageBox.Show(str);
                lblMsg.Content = "Received Message: " + str;
                */

                // Solution 2 (failed to pass message larger than 4 bytes)
                /*
                string data = Marshal.PtrToStringAnsi(lParam);
                lblMsg.Content = "Received Message: " + data;
                MessageBox.Show(data);
                */

                // Solution 3 (also failed to pass message larger than 4 bytes)
                /*
                byte[] bytes = new Byte[(int)wParam];
                Marshal.Copy(lParam, bytes, 0, (int)wParam);
                string data = Encoding.Default.GetString(bytes);
                lblMsg.Content = "Received Message: " + data;
                MessageBox.Show(data);
                */

                // Solution 4, try solution 1-3 multiplu times. Only pass a small piece of message in a single message
                // failed again, and I don't know why
                /*
                // signal of starting transmitting
                if (wParam == (IntPtr)999 && lParam != IntPtr.Zero)
                {
                    bytes = new byte[(int)lParam];
                    index = 0;
                }
                // signal of ending transmitting
                else if (wParam == (IntPtr)999 && lParam == (IntPtr)999)
                {
                    string data = Encoding.Default.GetString(bytes);
                    lblMsg.Content = "Received Message: " + data;
                    MessageBox.Show(data);
                }
                else
                {
                    Marshal.Copy(lParam, bytes, index, (int)wParam);
                    index += (int)wParam;
                }
                */



            }
            return hwnd;

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
