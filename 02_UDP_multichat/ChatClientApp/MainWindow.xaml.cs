using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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

namespace ChatClientApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UdpClient client = new();
        const string JOIN_CMD = "<<join_cmd>>";
        const string LEAVE_CMD = "<<leave_cmd>>";

        public MainWindow()
        {
            InitializeComponent();

            
        }

        private async Task SendCommand(string text)
        {
            // витягуємо дані з текстових блоків на вікні
            string ip = ipTB.Text;
            int port = int.Parse(portTB.Text);

            // створюємо endpoint, який містить адресу (ip + port) сервера
            IPEndPoint serverIP = new IPEndPoint(IPAddress.Parse(ip), port);

            var data = Encoding.UTF8.GetBytes(text);
            await client.SendAsync(data, serverIP); // 2s
        }

        private async void ReceiveMessages()
        {
            while (true)
            {
                var data = await client.ReceiveAsync();
                var message = Encoding.UTF8.GetString(data.Buffer);

                dialogList.Items.Add(message);
            }
        }

        private async void JoinBtnClick(object sender, RoutedEventArgs e)
        {
            await SendCommand(JOIN_CMD);

            ReceiveMessages();
        }

        private async void LeaveBtnClick(object sender, RoutedEventArgs e)
        {
            await SendCommand(LEAVE_CMD);
        }

        private async void SendBtnClick(object sender, RoutedEventArgs e)
        {
            var message = msgTB.Text;

            // string.IsNullOrEmpty() - check for [null] and [""]
            // string.IsNullOrWhiteSpace() - check for [null] and [""] and ["       "]
            if (string.IsNullOrWhiteSpace(message)) return; // close method

            await SendCommand(message);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
