using System.Net.Sockets;
using System.Net;
using System.Text;

namespace _01_server
{
    public class Program
    {
        const int port = 3300;

        static void Main(string[] args)
        {
            // створюємо UDP сокет для відправки та отримання пакетів даних
            UdpClient server = new UdpClient(port);

            IPEndPoint clientIP = null;

            while (true)
            {
                // очікуємо дані від якогось клієнта
                Console.WriteLine("\t...Waiting new requests...");
                var data = server.Receive(ref clientIP);

                // конвертуємо дані в рядок та відображаємо їх
                var message = Encoding.UTF8.GetString(data);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Request: {message} at {DateTime.Now} from {clientIP}");
                Console.ResetColor();



                // відправляємо відповідь клієнту
                server.Send(Encoding.UTF8.GetBytes("OK"), clientIP);
            }
        }
    }
    public class GetAnswerToRequest
    {
        private static string hello = "Hello!";
        private static string bye = "Bye!";
        private static string another = "I received your message";
        public static string GetAnswer(string request)
        {
            if (request == "Hello" || request == "Hi")
                return hello;
            else if (request == "Good bye" || request == "Bye")
                return bye;
            else
                return another;
        }
    }
}
