using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleServer
{
	class Program
	{
		static WebSocketServer wss;
		static void Main(string[] args)
		{
			wss = new WebSocketServer(8181, "http://localhost:8080", "ws://localhost:8181/chat");
			wss.ClientConnected += new WebSocketServer.ClientConnectedEventHandler(OnClientConnected);
			wss.Start();

			Console.WriteLine("Websocket server started!");
			KeepAlive();
		}

		static private void KeepAlive()
		{
			string r = Console.ReadLine();
			while (r != "quit")
			{
				r = Console.ReadLine();
			}
		}

		static void OnClientConnected(WebSocketConnection sender, EventArgs e)
		{
			sender.Disconnected += new WebSocketConnection.WebSocketDisconnectedEventHandler(OnClientDisconnected);
			sender.DataReceived += new WebSocketConnection.DataReceivedEventHandler(OnClientMessage);
			Console.WriteLine("Main(): A Client connected!");
		}

		static void OnClientMessage(WebSocketConnection sender, DataReceivedEventArgs e)
		{
			wss.SendToAllExceptOne(e.Data, sender);
			sender.Send(e.Data);

			Console.WriteLine("Main(): The client said: " + e.Data);
		}

		static void OnClientDisconnected(WebSocketConnection sender, EventArgs e)
		{
			try
			{
				wss.SendToAll("server: disconnected");
				Console.WriteLine("Main(): The client disconnected!");
			}
			catch (Exception exc)
			{
				Console.WriteLine("exception: " + exc.Message);
			}
		}
	}
}
