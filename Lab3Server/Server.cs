using Lab3Server;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Lab3Server
{
    class Server
    {
        void ClientThread(Object newClient)
        {
            new Client((TcpClient)newClient);
        }

        private TcpListener Listener; 
        public Server(int Port)
        {
            Listener = new TcpListener(IPAddress.Any, Port);
            Listener.Start();
            while (true)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(ClientThread), Listener.AcceptTcpClient());
            }
        }
    }
}
