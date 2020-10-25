using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UdpBroadcastSpike.ServerConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			var Server = new UdpClient(8888);                                       //Generate UdpClient by specifying the listening port
			var ResponseData = Encoding.ASCII.GetBytes("SomeResponseData");         //Appropriate response data

			while (true)
			{
				var ClientEp = new IPEndPoint(IPAddress.Any, 0);                    //Client (communication partner) endpoint ClientEp creation (IP)/Port not specified)
				var ClientRequestData = Server.Receive(ref ClientEp);               //Packet reception from client, client endpoint information is entered in ClientEp
				var ClientRequest = Encoding.ASCII.GetString(ClientRequestData);

				Console.WriteLine("Recived {0} from {1}, sending response", ClientRequest, ClientEp.Address.ToString());    // ClientEp.Address: Client IP
				Server.Send(ResponseData, ResponseData.Length, ClientEp);           //Packet transmission to Client Ep containing client information
			}
		}
	}
}
