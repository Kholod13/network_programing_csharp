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

namespace _01_server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UdpClient client = new UdpClient();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // витягуємо дані з текстових блоків на вікні
            string ip = ipTB.Text;
            int port = int.Parse(portTB.Text);

            string message = msgTB.Text;

            // створюємо endpoint, який містить адресу (ip + port) сервера
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(ip), port);

            // виконуємо відправку пакету даних на адресу сервера
            client.Send(Encoding.UTF8.GetBytes(message), endpoint);
            dialogList.Items.Add($"You: {message}");

            // TODO: виконати очікування відповіді від сервера в окремому потоці

            // отримуємо від сервера відповідь
            IPEndPoint? server = null;
            var data = client.Receive(ref server);

            // конвертуємо отримані байти в рядок
            var response = Encoding.UTF8.GetString(data);
            // додаємо відповідь в список
            dialogList.Items.Add($"Server: {response}");
        }
    }
}
