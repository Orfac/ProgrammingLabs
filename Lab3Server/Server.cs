using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DictionaryLib;
using DictionaryLib.Models;
using DictionaryLib.Net;
using DictionaryLib.Net.SocketWrappers;


namespace Lab3Server
{
    public class Server
    {
        private ServerSocket _socketServer;
        private RootDictionary _rootDictionary;

        public Server(int port, int backlog, RootDictionary rootDictionary, Encoding encoding)
        {
            this._socketServer = new ServerSocket(port, backlog, GetResponse, encoding);
            this._rootDictionary = rootDictionary;
        }

        public void Run()
        {
            try
            {
                _socketServer.Run();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GetResponse(string request)
        {
            try
            {
                var requestType = HttpParser.ParseRequestType(request);
                switch (requestType)
                {
                    case HttpUtils.RequestType.POST:
                        return HandlePostRequest(request);
                    case HttpUtils.RequestType.GET:
                        return HandleGetRequest(request);
                    default:
                        return HttpCreator.CreateResponse(400);
                }
            }
            catch (Exception)
            {
                return HttpCreator.CreateResponse(400);
            }

        }

        private string HandlePostRequest(string request)
        {
            Word newWord = HttpParser.ParsePostRequest(request);
            if (_rootDictionary.Contains(newWord.Value))
            {
                return HttpCreator.CreateResponse(409, new string[] { "Already exists" });
            }
            if (WordParser.IsComplexWordValid(newWord))
            {
                _rootDictionary.Add(newWord);
                return HttpCreator.CreateResponse(201, new string[] { "Created new word", newWord.Value });
            }
            return HttpCreator.CreateResponse(400);
        }

        private string HandleGetRequest(string request)
        {
            string wordToSeek = HttpParser.ParseGetRequest(request);
            if (_rootDictionary.Contains(wordToSeek))
            {
                string root = _rootDictionary.GetRoot(wordToSeek);
                List<Word> cognateWords = _rootDictionary.RootGroups[root].Words;
                string[] wordsToReturn = new string[cognateWords.Count];
                for (int i = 0; i < cognateWords.Count; i++)
                {
                    wordsToReturn[i] = cognateWords[i].ToString();
                }
                return HttpCreator.CreateResponse(200, wordsToReturn);
            }
            else
            {
                return HttpCreator.CreateResponse(404);
            }
        }
    }
}
