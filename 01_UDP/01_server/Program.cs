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
                Reply reply = new Reply();
                server.Send(Encoding.UTF8.GetBytes(reply.ReplyToRequest(message)), clientIP);
            }
        }
    }
    enum Answers
    {
        hi,
        bye,
        who_are_you,
        fact,
    }
    public class Reply
    {
        public string ReplyToRequest(string message)
        {
            string reply = string.Empty;
            message = message.ToLower();
            Answers type_answer = new Answers();
            //списки з запитаннями
            List<string> msg_hi = new List<string> { "hi", "hello", "hola", "whats up", "privet" };
            List<string> msg_bye = new List<string> { "bye", "good bye", "chao", "paka" };
            List<string> msg_whoAreYou = new List<string> { "who are you", "what can you do", "what are you made for" };
            List<string> msg_fact = new List<string> { "fact", "tell me the fact", "interesting fact" };

            //оприділення запитання
            foreach (string msg in msg_hi)
            {
                if (msg == message)
                {
                    type_answer = Answers.hi;
                    break;
                }
            }
            foreach (string msg in msg_bye)
            {
                if (msg == message)
                {
                    type_answer = Answers.bye;
                    break;
                }
            }
            foreach (string msg in msg_whoAreYou)
            {
                if (msg == message)
                {
                    type_answer = Answers.who_are_you;
                    break;
                }
            }
            foreach (string msg in msg_fact)
            {
                if (msg == message)
                {
                    type_answer = Answers.fact;
                    break;
                }
            }

            //генерування відповіді
            if(type_answer == Answers.hi)
            {
                reply = "Hello, I'm Server!";
            }
            if (type_answer == Answers.bye)
            {
                reply = "Bye, glad to help";
            }
            if (type_answer == Answers.who_are_you)
            {
                reply = "I am your server created to answer some of your questions, my name is GOVORUN";
            }
            if (type_answer == Answers.fact)
            {
                reply = "I'm made for homework";
            }

            return reply;
        }
    }
}
