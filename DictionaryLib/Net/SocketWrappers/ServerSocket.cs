using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace DictionaryLib.Net.SocketWrappers
{
    public class ServerSocket
    {
        private Func<string,string> _getMessage;

        private Socket _socket;
        public readonly int backlog;
        public readonly Encoding encoding;

        public ServerSocket(int port, int backlog, Func<string,string> getMessage, Encoding encoding)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var iPEndPoint = new IPEndPoint(IPAddress.Any, port);
            _socket.Bind(iPEndPoint);
            this.backlog = backlog;
            this._getMessage = getMessage;
            this.encoding = encoding;
        }

        public void Run()
        {
            try
            {
                _socket.Listen(backlog);
            }
            catch (Exception)
            {
                throw;
            }
           
            while (true)
            {
                try
                {
                    var client = _socket.Accept();
                        ThreadPool.QueueUserWorkItem(new WaitCallback(ClientThread), client);   
                }
                catch (Exception)
                {
                    continue;
                }

            }
        }

        private void ClientThread(object newClientObject)
        {
            if (newClientObject is null)
            {
                return;
            }
            var newClient = newClientObject as Socket;
            var clientSocket = new ClientSocket(newClient, encoding);

            var receivedData = clientSocket.Receive();
            var dataToSend = _getMessage(receivedData);
            clientSocket.Send(dataToSend);
        }
    }
}
