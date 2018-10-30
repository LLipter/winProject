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

        const int WM_COPYDATA = 0x004A;

        /*
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
        */

        IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            /*
            if (msg == WM_COPYDATA)
            {
                COPYDATASTRUCT cds = (COPYDATASTRUCT)Marshal.PtrToStructure(lParam, typeof(COPYDATASTRUCT)); // 接收封装的消息
                string rece = cds.lpData; // 获取消息内容
                MessageBox.Show(rece);
            }
            return hwnd;
            */
            MessageBox.Show(wParam.ToString());
            return (IntPtr)7614;

        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            IntPtr hwnd = new WindowInteropHelper(this).Handle;
            HwndSource hs = HwndSource.FromHwnd(hwnd);
            hs.AddHook(new HwndSourceHook(WndProc));
            
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
