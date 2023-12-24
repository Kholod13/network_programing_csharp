using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
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

namespace code_region
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static int port = 8080;
        private static string address = "26.194.91.94";
        public MainWindow()
        {
           InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ipPoint);

            while (Input.Text != "")
            {
                try
                {
                    //send message to server
                    byte[] data = Encoding.Unicode.GetBytes(Input.Text);
                    socket.Send(data);

                    // получаем ответ
                    data = new byte[256]; // буфер для ответа
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0; // количество полученных байт

                    do
                    {
                        bytes = socket.Receive(data, data.Length, 0);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (socket.Available > 0);
                    Output.Text = builder.ToString();
                }
                catch (Exception ex)
                {
                    Output.Text = "Incorrect code";
                }
                finally
                {
                    // закрываем сокет
                    //socket.Shutdown(SocketShutdown.Both);
                    //socket.Disconnect(true);
                    //socket.Close();
                }
            }
        }
    }
}
