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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PublisherSubscriber
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>

    /*
     * In this demonstration, class MainWindow is both a publisher and a subscriber.
     * Publisher define details of a specifical event including the name of event and the function signature its handler requires.
     * In C sharp, keyword delegate is used to define a special object that can specify a function signature.
     * In concept, what delegate defines in C sharp is similar to a function pointer in Cpp.
     * Subcriber define specified event handler, then subscribe the corresponding event.
     * After finishing all above, we can invoke the event and all subcriber will execute its handler.
     */

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // define the event
        private event SendMessage OnMessageSent;

        // invoke the event, thus all subscribed event handler will be invoked
        private void btnInvoke_Click(object sender, RoutedEventArgs e)
        {
            if(OnMessageSent == null)
            {
                MessageBox.Show("No Subscriber Detected");
                return;
            }
            MyEventArgs myArgs = new MyEventArgs(txtMsg.Text);
            OnMessageSent(this, myArgs);
        }

        // define event handler
        private void MessageEventHandler(object sender, MyEventArgs e)
        {
            lblResult.Content = "Result of Event Handler: " + e.message;
        }

        // Subscribe the event
        private void btnSubscribe_Click(object sender, RoutedEventArgs e)
        {
            if (OnMessageSent != null)
            {
                MessageBox.Show("Already Subscribed");
                return;
            }
            this.OnMessageSent += new SendMessage(MessageEventHandler);
            MessageBox.Show("Subscribe Successfully");
        }

        // Unsubscribe the event
        private void btnUnsubscribe_Click(object sender, RoutedEventArgs e)
        {
            if (OnMessageSent == null)
            {
                MessageBox.Show("Haven't Subscribed Yet");
                return;
            }
            this.OnMessageSent -= new SendMessage(MessageEventHandler);
            MessageBox.Show("Unsubscribe Successfully");
        }
    }

    // similar to a function pointer, thus, invokable
    public delegate void SendMessage(object sender, MyEventArgs e);

    // Define customized arguments 
    public class MyEventArgs : EventArgs
    {
        public string message;
        public MyEventArgs(string message)
        {
            this.message = message;
        }
    }

}
