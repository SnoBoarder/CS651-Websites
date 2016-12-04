using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net.Sockets;
using System.Net;
using System.IO;

using System.Timers;

namespace ConsoleServer
{
    class WebSocketServer
    {
        Timer t;

        public List<WebSocketConnection> Connections { get; private set; }
        public Socket ListenerSocket { get; private set; }
        public int Port { get; private set; }

        public delegate void ClientConnectedEventHandler(WebSocketConnection sender, EventArgs e);
        public event ClientConnectedEventHandler ClientConnected;
        void ClientDisconnected(WebSocketConnection sender, EventArgs e)
        {
            Connections.Remove(sender);
        }
        void DataReceivedFromClient(WebSocketConnection sender, DataReceivedEventArgs e)
        {
            Console.WriteLine("WebSocketServer(): " + DateTime.Now + "> data from " + sender.ConnectionSocket.LocalEndPoint);
            Console.WriteLine("WebSocketServer(): " + e.Data + "\n" + e.Size + " bytes");
        }

        private string webSocketOrigin;     // location for the protocol handshake
        private string webSocketLocation;   // location for the protocol handshake
        public WebSocketServer(int port, string origin, string location)
        {
            Port = port;
            Connections = new List<WebSocketConnection>();
            webSocketOrigin = origin;
            webSocketLocation = location;

            t = new Timer { Interval = 10000 };
            t.Elapsed += (e, s) =>
            {
                try
                {
                    // debugging
                    //t.Stop();

                    SendToAll("i love you!"); 
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            };
        }

        public void Start()
        {
            // create the main server socket, bind it to the local ip address and start listening for clients
            ListenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            IPEndPoint ipLocal = new IPEndPoint(IPAddress.Loopback, Port);
            ListenerSocket.Bind(ipLocal);
            ListenerSocket.Listen(100);
            ListenForClients();
            t.Start();
        }

        private void ListenForClients()
        {
            ListenerSocket.BeginAccept(new AsyncCallback(OnClientConnect), null);
        }

        private void OnClientConnect(IAsyncResult asyn)
        {
            Console.WriteLine("WebSocketServer(): in OnClientConnect()..");

            // create a new socket for the connection
            var clientSocket = ListenerSocket.EndAccept(asyn);

            // shake hands to give the new client a warm welcome
            ShakeHands(clientSocket);

            // keep track of the new guy
            var clientConnection = new WebSocketConnection(clientSocket);
            Connections.Add(clientConnection);
            clientConnection.Disconnected += new WebSocketConnection.WebSocketDisconnectedEventHandler(ClientDisconnected);

            // invoke the connection event
            if (ClientConnected != null)
                ClientConnected(clientConnection, EventArgs.Empty);

            clientConnection.DataReceived += new WebSocketConnection.DataReceivedEventHandler(DataReceivedFromClient);

            // listen for more clients
            ListenForClients();
        }

        private void ShakeHands(Socket conn)
        {
            // correct handshake from Websocket rfc
            if (conn != null)
            {
                byte[] buffer = new byte[1024];
                var i = conn.Receive(buffer);
                string headerResponse = (System.Text.Encoding.UTF8.GetString(buffer)).Substring(0, i);
                // write received data to the console
                Console.WriteLine(headerResponse);

                var key = headerResponse.Replace("ey:", "`")
                          .Split('`')[1]                     // dGhlIHNhbXBsZSBub25jZQ== \r\n .......
                          .Replace("\r", "").Split('\n')[0]  // dGhlIHNhbXBsZSBub25jZQ==
                          .Trim();

                // key should now equal dGhlIHNhbXBsZSBub25jZQ==
                var test1 = AcceptKey(ref key);

                var newLine = "\r\n";

                var response = "HTTP/1.1 101 Switching Protocols" + newLine
                     + "Upgrade: websocket" + newLine
                     + "Connection: Upgrade" + newLine
                     + "Sec-WebSocket-Accept: " + test1 + newLine + newLine
                    //+ "Sec-WebSocket-Protocol: chat, superchat" + newLine
                    //+ "Sec-WebSocket-Version: 13" + newLine
                     ;

                conn.Send(System.Text.Encoding.UTF8.GetBytes(response));
            }
        }

        static private string guid = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";
        private static string AcceptKey(ref string key)
        {
            string longKey = key + guid;
            byte[] hashBytes = ComputeHash(longKey);
            return Convert.ToBase64String(hashBytes);
        }

        static System.Security.Cryptography.SHA1 sha1 = System.Security.Cryptography.SHA1CryptoServiceProvider.Create();
        private static byte[] ComputeHash(string str)
        {
            return sha1.ComputeHash(System.Text.Encoding.ASCII.GetBytes(str));
        }

        public void SendToAll(string data)
        {
            Connections.ForEach(a => a.Send(data));
        }

        public void SendToAllExceptOne(string data, WebSocketConnection indifferent)
        {
            foreach (var client in Connections)
            {
                if (client != indifferent)
                    client.Send(data);
            }
        }
    }
}
