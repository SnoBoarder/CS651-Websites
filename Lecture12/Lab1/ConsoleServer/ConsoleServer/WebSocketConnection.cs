using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net.Sockets;
using System.IO;

namespace ConsoleServer
{
    class WebSocketConnection : IDisposable
    {
        private byte[] dataBuffer;                                  // buffer to hold the data we are reading
        bool readingData;                                           // are we in the proccess of reading data or not
        private StringBuilder dataString;                           // holds the currently accumulated data
        private enum WrapperBytes : byte { Start = 0, End = 255 };  // data passed between client and server are wrapped in start and end bytes according to the protocol (0x00, 0xFF)

        public Socket ConnectionSocket { get; private set; }
        public System.Guid GUID { get; private set; }

        public WebSocketConnection(Socket socket)
            : this(socket, 255)
        {
            Console.WriteLine("WebSocketConnection(socket): new!");
        }

        public WebSocketConnection(Socket socket, int bufferSize)
        {
            Console.WriteLine("WebSocketConnection(socket, buffersize): new!");
            ConnectionSocket = socket;
            dataBuffer = new byte[bufferSize];
            dataString = new StringBuilder();
            GUID = System.Guid.NewGuid();
            Listen();
        }

        public void Send(string str)
        {
            if (ConnectionSocket.Connected)
            {
                try
                {

                    ConnectionSocket.Send(CorrectlyFramed(Encoding.UTF8.GetBytes(str)));
                }
                catch
                {
                    if (Disconnected != null)
                        Disconnected(this, EventArgs.Empty);
                }
            }
        }

	// from WebSocket rfc
        public byte[] CorrectlyFramed(byte[] binary)
        {
            try
            {
                ulong headerLength = 2;
                byte[] data = binary;

                bool mask = false;
                byte[] maskKeys = null;

                if (mask)
                {
                    headerLength += 4;
                    data = (byte[])data.Clone();

                    Random random = new Random(Environment.TickCount);
                    maskKeys = new byte[4];
                    for (int i = 0; i < 4; ++i)
                    {
                        maskKeys[i] = (byte)random.Next(byte.MinValue, byte.MaxValue);
                    }

                    for (int i = 0; i < data.Length; ++i)
                    {
                        data[i] = (byte)(data[i] ^ maskKeys[i % 4]);
                    }
                }

                byte payload;
                if (data.Length >= 65536)
                {
                    headerLength += 8;
                    payload = 127;
                }
                else if (data.Length >= 126)
                {
                    headerLength += 2;
                    payload = 126;
                }
                else
                {
                    payload = (byte)data.Length;
                }

                byte[] header = new byte[headerLength];

                header[0] = 0x80 | 0x1;
                if (mask)
                {
                    header[1] = 0x80;
                }
                header[1] = (byte)(header[1] | payload & 0x40 | payload & 0x20 | payload & 0x10 | payload & 0x8 | payload & 0x4 | payload & 0x2 | payload & 0x1);

                if (payload == 126)
                {
                    byte[] lengthBytes = BitConverter.GetBytes((ushort)data.Length).Reverse().ToArray();
                    header[2] = lengthBytes[0];
                    header[3] = lengthBytes[1];

                    if (mask)
                    {
                        for (int i = 0; i < 4; ++i)
                        {
                            header[i + 4] = maskKeys[i];
                        }
                    }
                }
                else if (payload == 127)
                {
                    byte[] lengthBytes = BitConverter.GetBytes((ulong)data.Length).Reverse().ToArray();
                    for (int i = 0; i < 8; ++i)
                    {
                        header[i + 2] = lengthBytes[i];
                    }
                    if (mask)
                    {
                        for (int i = 0; i < 4; ++i)
                        {
                            header[i + 10] = maskKeys[i];
                        }
                    }
                }

                return header.Concat(data).ToArray();

            }
            catch (Exception ex)
            {
                Console.WriteLine("websocket transport protocol Send exception: " + ex.Message);
            }

            return null;
        }

        public delegate void DataReceivedEventHandler(WebSocketConnection sender, DataReceivedEventArgs e);
        public delegate void WebSocketDisconnectedEventHandler(WebSocketConnection sender, EventArgs e);

        public event DataReceivedEventHandler DataReceived;
        public event WebSocketDisconnectedEventHandler Disconnected;

        protected virtual void OnDataReceived(DataReceivedEventArgs e)
        {
            if (DataReceived != null)
                DataReceived(this, e);
        }

        private void Listen()
        {
            ConnectionSocket.BeginReceive(dataBuffer, 0, dataBuffer.Length, 0, Read, null);
        }

	// from Websocket rfc
        private string Decode2(byte[] array)
        {
            int i;
            byte[] mask = new byte[4];
            int packet_length = 0;

            /* Expect a finished text frame. */
            //assert(array[0] == '\x81');
            packet_length = ((char)array[1]) & 0x7f;

            if (packet_length <= 125)
            {
                mask[0] = array[2];
                mask[1] = array[3];
                mask[2] = array[4];
                mask[3] = array[5];

                for (i = 0; i < packet_length; i++)
                    array[6 + i] ^= mask[i % 4];
                return System.Text.Encoding.UTF8.GetString(array).Substring(6, packet_length);
            }
            else if (packet_length == 126)
            {
                // the following 2 bytes give the length of data to be read,
                // The mask is contained in the following 4 bytes (after the length). 
                // The message to be decoded follows this.
                packet_length = array[2];
                packet_length |= array[3] << 8;

                mask[0] = array[4];
                mask[1] = array[5];
                mask[2] = array[6];
                mask[3] = array[7];

                for (i = 0; i < packet_length; i++)
                    array[8 + i] ^= mask[i % 4];
                return System.Text.Encoding.UTF8.GetString(array).Substring(8, packet_length);
            }
            else if (packet_length == 127)
            {
                // the following 8 bytes give the length of data to be read,
                // The mask is contained in the following 4 bytes (after the length). 
                // The message to be decoded follows this.
                packet_length = array[2];
                packet_length |= array[3] << 8;
                packet_length |= array[4] << 16;
                packet_length |= array[5] << 24;
                packet_length |= array[6] << 32;
                packet_length |= array[7] << 40;
                packet_length |= array[8] << 48;
                packet_length |= array[9] << 56;

                mask[0] = array[10];
                mask[1] = array[11];
                mask[2] = array[12];
                mask[3] = array[13];

                for (i = 0; i < packet_length; i++)
                    array[14 + i] ^= mask[i % 4];
                return System.Text.Encoding.UTF8.GetString(array).Substring(14, packet_length);
            }
            else
            {
                return "??";
            }
        }
        ulong ConvertLittleEndian(byte[] array)
        {
            int pos = 0;
            ulong result = 0;
            foreach (byte by in array)
            {
                result |= (ulong)(by << pos);
                pos += 8;
            }
            return result;
        }
        private void Read(IAsyncResult ar)
        {
            int sizeOfReceivedData = ConnectionSocket.EndReceive(ar);
            if (sizeOfReceivedData > 0)
            {
                OnDataReceived(new DataReceivedEventArgs(sizeOfReceivedData, Decode2(dataBuffer)));
                Send("hi dere!"); 

                // continue listening for more data
                Listen();
            }
            else // the socket is closed
            {
                if (Disconnected != null)
                    Disconnected(this, EventArgs.Empty);
            }
        }

        public void Close()
        {
            Console.WriteLine("WebSocketConnection(): Close()!");
            ConnectionSocket.Close();
        }
        public void Dispose()
        {
            Close();
        }
    }

    public class DataReceivedEventArgs : EventArgs 
    {
        public int Size { get; private set; }
        public string Data { get; private set; }
        public DataReceivedEventArgs(int size, string data)
        {
            Size = size;
            Data = data;
        }
    }
}
