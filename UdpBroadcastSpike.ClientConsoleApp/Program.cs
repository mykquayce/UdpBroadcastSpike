using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UdpBroadcastSpike.ClientConsoleApp
{
	class Program
	{
		static void Main(string[] args)
        {
			var Client = new UdpClient();                           //Create UdpClient (port number is assigned appropriately)
			var RequestData = Encoding.ASCII.GetBytes("Request");   //Appropriate request data
			var ServerEp = new IPEndPoint(IPAddress.Any, 0);        //Server (communication partner) endpoint ServerEp creation (IP)/Port not specified)

			Client.EnableBroadcast = true;                          //Broadcast enabled
			Client.Send(RequestData, RequestData.Length, new IPEndPoint(IPAddress.Broadcast, 8888)); //Broadcast to port 8888

			//The person who received the transmitted data should have known the endpoint information of himself / herself (client).
			//Wait for the packet to be sent to it
			var ServerResponseData = Client.Receive(ref ServerEp);  //Packet reception from the server, server endpoint information is entered in ServerEp
			var ServerResponse = Encoding.ASCII.GetString(ServerResponseData);
			// ServerEp.Address / ServerEp.IP of the server on Port/Get port number
			Console.WriteLine("Recived {0} from {1}:{2}", ServerResponse, ServerEp.Address.ToString(), ServerEp.Port.ToString());

			Client.Close();
		}
	}
}
