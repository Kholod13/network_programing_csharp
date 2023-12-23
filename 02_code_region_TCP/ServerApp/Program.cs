using System.Net.Sockets;
using System.Net;
using System.Text;

namespace ServerApp
{
    public class Program
    {
        static int port = 8080; // порт для приема входящих запросов
        enum Region
        {
            Volynska_AC,
            Rivnenska_BK,
            Zhitomyrska_AM,
            Kyivska_AA,
            Kyivska_AI,
            Chernigivska_CB,
            Sumska_BM,
            Kharhivska_AX,
            Luganska_BB,
            Lvivska_BC,
            Ternopilska_BO,
            Hmelnitska_BO,
            Vynnitska_AB,
            Cherkaska_CA,
            Kirovogradska_BA,
            Dniproptervska_AE,
            Donetska_AH,
            Zakarpatska_AO,
            Frankivska_AT,
            Chernovitska_CE,
            Odeska_BH,
            Mykolaivska_BE,
            Khersonska_BT,
            Zaprizka_AP,
            Crimea_AK,
            Sevastopol_CH,
        }

        static void Main(string[] args)
        {
            // получаем адреса для запуска сокета
            IPAddress iPAddress = IPAddress.Parse("26.194.91.94");//Dns.GetHostEntry("localhost").AddressList[1]; //localhost
            IPEndPoint ipPoint = new IPEndPoint(iPAddress, port);

            // создаем сокет
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                // связываем сокет с локальной точкой, по которой будем принимать данные
                listenSocket.Bind(ipPoint);

                // начинаем прослушивание
                listenSocket.Listen(10);

                Console.WriteLine("Server started! Waiting for connection...");
                Socket handler = listenSocket.Accept();

                while (true)
                {
                    // handler.Receive(); - get data from client
                    // handler.Send();    - sent data to client

                    // получаем сообщение
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0; // количество полученных байтов
                    byte[] data = new byte[256]; // буфер для получаемых данных

                    do
                    {
                        bytes = handler.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (handler.Available > 0);

                    Console.WriteLine("Request received");
                    Console.WriteLine($"{handler.RemoteEndPoint} - {builder.ToString()} at {DateTime.Now.ToShortTimeString()}");

                    // отправляем ответ
                    string message = builder.ToString();


                    data = Encoding.Unicode.GetBytes(message);
                    handler.Send(data);
                    Console.WriteLine("Answer was send");
                    break;

                }
                // закрываем сокет
                //handler.Shutdown(SocketShutdown.Both);
                //handler.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}