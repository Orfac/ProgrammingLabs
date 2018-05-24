using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DictionaryLib.Models;
using DictionaryLib.Net;
using DictionaryLib.Net.Http;
using DictionaryLib.Net.SocketWrappers;

namespace Lab3Client
{
    public class Client
    {
        public bool Connected => _clientSocket.Connected;

        private ClientSocket _clientSocket;

        public Client(string host, int port, Encoding encoding)
        {
            _clientSocket = new ClientSocket(host, port, encoding);
        }

        public void Connect()
        {
            _clientSocket.Connect();
        }

        public void Reconnect()
        {
            _clientSocket.Reconnect();
        }

        public Response SeekWord(string wordToFind)
        {
            var request = new Request(RequestType.GET, wordToFind);
            return GetResponse(request);
        }

        public Response AddWord(Word word)
        {
            string attributes = WordWrapper.GetAttributes(word.Morphemes);
            var request = new Request(RequestType.POST, word.Value, attributes);
            return GetResponse(request);
        }

        private Response GetResponse(Request request)
        {
            _clientSocket.Send(request.ToString());
            string _response = _clientSocket.Receive();
            return Response.Parse(_response);
        }

    }
}
