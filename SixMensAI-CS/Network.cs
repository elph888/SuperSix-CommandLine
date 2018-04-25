using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;

namespace SixMensAI_CS
{

    class Network
    {
        #region private members
        private TcpListener Listener;
        private Thread ListenThread;
        private TcpClient connectedClient;
        #endregion
        public string outMove;

        public void Host()
        {
            ListenThread = new Thread(new ThreadStart(ListenRequest));
            ListenThread.IsBackground = true;
            ListenThread.Start();
        }
        private void ListenRequest()
        {
            try
            {
                Listener = new TcpListener(52018);
                Listener.Start();
                Console.WriteLine("Listening at port 52018...");
                Byte[] bytes = new Byte[1024];
                while (true)
                {
                    using (connectedClient = Listener.AcceptTcpClient())
                    {
                        Console.WriteLine("Client Connected");
                        using (NetworkStream stream = connectedClient.GetStream())
                        {
                            int length;
                            while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                            {
                                Console.WriteLine("Reading in Opponent move.");
                                var incommingBytes = new byte[length];
                                Array.Copy(bytes, 0, incommingBytes, 0, length);
                                string incommingMove = Encoding.ASCII.GetString(incommingBytes);
                                Console.WriteLine("Move recieved " + incommingMove);

                            }
                        }
                    }

                }
            }
            catch (SocketException sockerErr)
            {
                Console.WriteLine("SocketException: " + sockerErr);
            }
        }

        private void SendMessage()
        {
            if (connectedClient == null)
            {
                Console.WriteLine("No client connected at the moment.");
                return;
            }

            try
            {
                NetworkStream stream = connectedClient.GetStream();
                if (stream.CanWrite)
                {
                    byte[] outBytes = Encoding.ASCII.GetBytes(outMove);
                    stream.Write(outBytes, 0, outBytes.Length);
                    Console.WriteLine("Server sent move" + outMove);
                }
            }
            catch (SocketException socketErr)
            {
                Console.WriteLine("Socket Exception: " + socketErr);
            }
        }
    }
}

