using System;
using System.Net.Sockets;
using System.Text;

namespace DictionaryLib.Net.SocketWrappers
{
    /// <summary>
    /// Client for information exchange between server and client
    /// Can be created both from client and server side
    /// </summary>
    public class ClientSocket
    {
        private string _host;
        private int _port;
        private Socket _socket;
        private Encoding _encoding;

        public bool Connected => _socket.Connected;

        /// <summary>
        /// Uses for creating socket client from client side
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="encoding"></param>
        public ClientSocket(string host, int port, Encoding encoding)
        {
            this._host = host;
            this._port = port;
            this._encoding = encoding;
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        /// <summary>
        /// Uses for creating socket client from server side
        /// </summary>
        /// <param name="recievingSocket"></param>
        /// <param name="encoding"></param>
        public ClientSocket(Socket recievingSocket, Encoding encoding)
        {
            this._socket = recievingSocket;
            this._encoding = encoding;
        }

        /// <summary>
        /// Uses from client side to connect to remote host
        /// </summary>
        public void Connect()
        {
            _socket.Connect(_host, _port);
        }

        public void Reconnect()
        {
            _socket.Close();
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.Connect();
        }


        /// <summary>
        /// Wrapper over socket send  and encoding
        /// </summary>
        /// <param name="message"></param>
        public void Send(string message)
        {
            byte[] byteMessage = _encoding.GetBytes(message);
            int messageLength = byteMessage.Length;
            int readIndex = 0;
            do
            {
                int length = GetSegmentSize(messageLength, readIndex, _socket.SendBufferSize);
                readIndex += _socket.Send(byteMessage, readIndex, length, SocketFlags.None);
            } while (readIndex < messageLength);

        }

        /// <summary>
        /// receives message including oversize messages and encoding
        /// </summary>
        /// <returns>encoded data</returns>
        public string Receive()
        {
            var receivedString = new StringBuilder();
            byte[] buffer = new byte[_socket.ReceiveBufferSize];
            do
            {
                int bytesCount =_socket.Receive(buffer);
                receivedString.Append(_encoding.GetChars(buffer, 0, bytesCount));
            } while (_socket.Available > 0);
            return receivedString.ToString();
        }


        private int GetSegmentSize(int messageLength, int index, int bufferSize)
        {
            return Math.Min(messageLength - index, bufferSize);
        }
    }
}
