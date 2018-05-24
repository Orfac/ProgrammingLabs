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
        private string _host;

        public Client(string host, int port, Encoding encoding)
        {
            _clientSocket = new ClientSocket(host, port, encoding);
            _host = host;
        }

        public void Connect()
        {
            _clientSocket.Connect();
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
            Console.WriteLine(request.ToString());
            _clientSocket.Send(request.ToString());
            string _response = _clientSocket.Receive();
            Console.WriteLine(_response);
            return  Response.Parse(_response); 
        }

    }
}
