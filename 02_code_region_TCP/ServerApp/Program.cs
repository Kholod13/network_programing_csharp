using System.Net.Sockets;
using System.Net;
using System.Text;

namespace ServerApp
{
    public class Program
    {
        static int port = 8080; // порт для приема входящих запросов

        static void Main(string[] args)
        {
            List<string> regions = new List<string>
        {
            "Volynska",
            "Rivnenska",
            "Zhitomyrska",
            "Kyivska",
            "Kyivska",
            "Chernigivska",
            "Sumska",
            "Kharhivska",
            "Luganska",
            "Lvivska",
            "Ternopilska",
            "Hmelnitska",
            "Vynnitska",
            "Cherkaska",
            "Kirovogradska",
            "Dniproptervska",
            "Donetska",
            "Zakarpatska",
            "Frankivska",
            "Chernovitska",
            "Odeska",
            "Mykolaivska",
            "Khersonska",
            "Zaprizka",
            "Crimea",
            "Sevastopol",
        };
            List<string> regions_code = new List<string>
        {
            "AC",
            "BK",
            "AM",
            "AA",
            "AI",
            "CB",
            "BM",
            "AX",
            "BB",
            "BC",
            "BO",
            "BO",
            "AB",
            "CA",
            "BA",
            "AE",
            "AH",
            "AO",
            "AT",
            "CE",
            "BH",
            "BE",
            "BT",
            "AP",
            "AK",
            "CH",
        };
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
                    Console.WriteLine($"{handler.RemoteEndPoint} - {builder} at {DateTime.Now.ToShortTimeString()}");

                    // отправляем ответ
                    string message = builder.ToString();

                    for (int i = 0; i < regions_code.Count; i++)
                    {
                        if (regions_code[i] == message)
                        {
                            message = regions[i];
                            break;
                        }
                    }


                    data = Encoding.Unicode.GetBytes(message);
                    Console.WriteLine($"Answer: {message}");
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