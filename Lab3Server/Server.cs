using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using DictionaryLib;
using DictionaryLib.Models;
using DictionaryLib.Net;
using DictionaryLib.Net.Http;
using DictionaryLib.Net.SocketWrappers;


namespace Lab3Server
{
    public class Server
    {
        private ServerSocket _socketServer;
        private RootDictionary _rootDictionary;
        private Object thisLock = new Object();

        public Server(int port, int backlog, RootDictionary rootDictionary, Encoding encoding)
        {
            this._socketServer = new ServerSocket(port, backlog, GetResponse, encoding);
            this._rootDictionary = rootDictionary;
        }

        public void Run()
        {
            _socketServer.Run();
        }

        /// <summary>
        /// Sending message and getting response  
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string GetResponse(string message)
        {
            try
            {
                var request = Request.Parse(message);
                switch (request.Type)
                {
                    case RequestType.POST:
                        return HandlePostRequest(request);
                    case RequestType.GET:
                        return HandleGetRequest(request);
                    default:
                        return new Response(StatusCode.BadRequest).ToString();
                }
            }
            catch (InvalidOperationException ex)
            {
                return new Response(StatusCode.BadRequest).ToString();
            }

        }

        private string HandlePostRequest(Request request)
        {
            Word newWord = new Word(request.Value);

            if (_rootDictionary.Contains(newWord.Value))
            {
                return new Response(StatusCode.Сonflict).ToString();
            }

            WordWrapper.SetMorphemes(newWord, request.Attributes);
            lock (thisLock)
            {
                if (_rootDictionary.Contains(newWord.Value))
                    return new Response(StatusCode.Сonflict).ToString();

                if (WordParser.IsComplexWordValid(newWord))
                {
                    _rootDictionary.Add(newWord);
                    return new Response(StatusCode.Created).ToString();

                }
            }
            return new Response(StatusCode.BadRequest).ToString();
        }

        private string HandleGetRequest(Request request)
        {
            string wordToSeek = request.Value;
            if (_rootDictionary.Contains(wordToSeek))
            {
                string root = _rootDictionary.GetRoot(wordToSeek);
                List<Word> cognateWords = _rootDictionary.RootGroups[root].Words;
                var wordsToReturn = new StringBuilder();
                for (int i = 0; i < cognateWords.Count; i++)
                {
                    wordsToReturn.Append(cognateWords[i]);
                    wordsToReturn.Append('\n');
                }
                return new Response(StatusCode.OK, wordsToReturn.ToString()).ToString();
            }
            else
            {
                return new Response(StatusCode.ObjectNotFound).ToString();
            }
        }
    }
}
