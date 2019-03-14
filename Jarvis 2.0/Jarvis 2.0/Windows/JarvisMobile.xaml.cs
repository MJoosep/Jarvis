#region Imports

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Input;

#endregion
//clean

namespace Jarvis_2._0.Windows
{
    public partial class JarvisMobile : UserControl
    {
        #region Values

        static byte[] bytes = new byte[1024];

        static IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
        static IPAddress ipAddress = ipHostInfo.AddressList[0];
        static IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

        static Socket sender = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);

        #endregion
        //clean

        public JarvisMobile()
        {
            InitializeComponent();

            Thread listen = new Thread(Listen);
            listen.Start();
        }
        //clean

        public void Listen()
        {
            sender.Connect(remoteEP);

            while (true)
            {
                int bytesRec = sender.Receive(bytes);

                Input.Dispatcher.BeginInvoke(
                    (Action)(() => { Input.Text = Input.Text + ("\n {0}", Encoding.ASCII.GetString(bytes, 0, bytesRec)); }));
            }
        }
        //clean

        private void Output_KeyDown(object senders, KeyEventArgs ee)
        {
            if (ee.Key == Key.Return)
            {
                try
                {
                    byte[] msg = Encoding.ASCII.GetBytes(Output.Text + "I--I");

                    int bytesSent = sender.Send(msg);

                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }
            }
        }
        //clean
    }
}
