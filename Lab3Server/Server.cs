using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            catch (Exception)
            {
                return new Response(StatusCode.BadRequest).ToString();
            }

        }

        private string HandlePostRequest(Request request)
        {
            Console.WriteLine(request.ToString());
            Word newWord = new Word(request.Value);
            if (_rootDictionary.Contains(newWord.Value))
            {
                return new Response(StatusCode.Сonflict).ToString();
            }
            WordWrapper.SetMorphemes(newWord, request.Attributes);
            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine("MOR"+newWord.Morphemes[i]); 
            }
            bool b = WordParser.IsComplexWordValid(newWord);
            Console.WriteLine(b);
            if (WordParser.IsComplexWordValid(newWord))
            {
                _rootDictionary.Add(newWord);
                return new Response(StatusCode.Created).ToString();
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
                    wordsToReturn.Append("\n");
                }
                return new Response(StatusCode.OK,wordsToReturn.ToString()).ToString();
            }
            else
            {
                return new Response(StatusCode.ObjectNotFound).ToString();
            }
        }
    }
}
