using System.Net;
using System.Net.Sockets;
using System.Text;

const int port = 3300;
const string JOIN_CMD = "<<join_cmd>>";
const string LEAVE_CMD = "<<leave_cmd>>";

UdpClient server = new UdpClient(port);

HashSet<IPEndPoint> members = new();

IPEndPoint? clientIP = null;

do
{
    Console.WriteLine($"Chat status: {members.Count} members");
    var data = server.Receive(ref clientIP);

	string command = Encoding.UTF8.GetString(data);
	Console.WriteLine($"Client command: {command} from {clientIP}");

	switch (command)
	{
		case JOIN_CMD:
			members.Add(clientIP);
			break;

		case LEAVE_CMD:
			members.Remove(clientIP);
			break;

		default:
            foreach (var member in members)
            {
				server.Send(data, member);
            }
			break;
	}
} while (true);