using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;

namespace SixMensAI_CS
{
    class NetworkClient
    {
        #region private members
        Thread inThread;
        TcpClient connection;
        String serverIP;
        String outMove;
        #endregion
        public void ConnectServer(String input)
        {
            serverIP = input;
            try
            {
                inThread = new Thread(new ThreadStart(Listen));
                inThread.IsBackground = true;
                inThread.Start();
                Console.WriteLine("Thread is listening");
            }
            catch (Exception err)
            {
                Console.WriteLine("Client connection exception " + err);
            }
        }
        private void Listen()
        {
            try
            {
                connection = new TcpClient("serverIP", 52018);
                Byte[] bytes = new Byte[1024];
                while (true)
                {
                    using (NetworkStream stream = connection.GetStream())
                    {
                        int length;
                        while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            var incommingBytes = new byte[length];
                            Array.Copy(bytes, 0, incommingBytes, 0, length);
                            string incommingMessage = Encoding.ASCII.GetString(incommingBytes);
                            Console.WriteLine("Incomming Move is " + incommingMessage);
                        }
                    }

                }
            }
            catch (SocketException socketErr)
            {
                Console.WriteLine("Socket Exception: " + socketErr);
            }
        }
        private void SendMessage()
        {
            if (connection == null)
            {
                Console.WriteLine("Not connected to a server...");
                return;
            }
            try
            {
                NetworkStream stream = connection.GetStream();
                if (stream.CanWrite)
                {
                    string outMessage = outMove;
                    byte[] outBytes = Encoding.ASCII.GetBytes(outMessage);
                    stream.Write(outBytes, 0, outBytes.Length);
                    Console.WriteLine("Our move has been sent to the server.");
                }
            }
            catch (SocketException socketErr)
            {
                Console.WriteLine("Socket Exception: " + socketErr);
            }
        }

    }

}
}
