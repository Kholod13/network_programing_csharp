using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerApp
{
    internal class Program
    {
        const int port = 3300;
        enum type_answers
        {
            hi,
            bye,
            _do,
        }
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

                //створення бази данних відповідей
                //client mess
                List<string> messages_hi = ["hi", "hello", "hey", "hola"];
                List<string> messages_bye = ["bye", "goodbye", "end", "chao"];
                List<string> messages_do = ["what can you do", "what you do", "who is you", "how to work with you"];
                //server confessions
                List<string> confession_hi = ["Hello I'm Server!", "How can I help?", "Your server ready to work", "What will we do today?", "Wait to your answer..."];
                List<string> confession_bye = ["Good luck", "Goodbye", "See you soon"];
                List<string> confession_do = ["I'm your server, which so far can only answer a few of your questions."];

                //оприділлення відповіді
                Random random = new Random();
                string confession = string.Empty;
                string answer = message.ToLower();

                int size;
                if (messages_hi.Count > messages_bye.Count || messages_hi.Count > messages_do.Count)
                {
                    size = messages_hi.Count;
                }
                else if (messages_bye.Count > messages_hi.Count || messages_bye.Count > messages_do.Count)
                {
                    size = messages_bye.Count;
                }
                else
                {
                    size = messages_do.Count;
                }

                for (int i = 0; i < size; i++)
                {
                    if (answer == messages_hi[i])
                    {

                    }
                }

                // відправляємо відповідь клієнту
                server.Send(Encoding.UTF8.GetBytes(), clientIP);
            }
        }
    }
}