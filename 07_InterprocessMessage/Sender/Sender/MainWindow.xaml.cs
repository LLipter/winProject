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
        private static extern int SendMessage(int hWnd, int Msg, int wParam, IntPtr lParam);

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private extern static int FindWindow(string lpClassName, string lpWindowName);

        const int WM_COPYDATA = 0x004A;

        public MainWindow()
        {
            InitializeComponent();
        }

        const int MAX_LENGTH = 100;

        private void btnSendMsg_Click(object sender, RoutedEventArgs e)
        {
            int hWnd = FindWindow(null, "Receiver");
            if (hWnd == 0)
            {
                MessageBox.Show("cannot find handler");
                return;
            }

            COPYDATASTRUCT data = new COPYDATASTRUCT();
            string msg = "llIPTER";
            byte[] bytes = Encoding.ASCII.GetBytes(msg);
            data.data = new byte[MAX_LENGTH];
            for (int i = 0; i < MAX_LENGTH; i++)
                data.data[i] = i < bytes.Length ? bytes[i] : (byte)0;

            int nSize = Marshal.SizeOf(data);
            IntPtr ptr = Marshal.AllocHGlobal(nSize);
            Marshal.StructureToPtr(data, ptr, false);
            SendMessage(hWnd, WM_COPYDATA, 7614, ptr);
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
