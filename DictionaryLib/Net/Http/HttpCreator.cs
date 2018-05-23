using DictionaryLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DictionaryLib.Net
{
    public static class HttpCreator
    {
        /// <summary>
        /// Creates string view of http 1.0 request
        /// </summary>
        /// <param name="word">word to seek or </param>
        /// <param name="requestType"> GET, POST and other  </param>
        /// <param name="hostName">host adress to connect</param>
        /// <param name="attributes">attributes of request at body</param>
        /// <returns></returns>
        public static string CreateRequest(string word, string requestType, string host, string[] attributes = null)
        {
            var request = new StringBuilder();
            request.Append(CreateRequestHeader(word, requestType, host));
            request.Append(CreateRequestBody(attributes));
            return request.ToString();
        }

        /// <summary>
        /// Creates string view of http 1.0 response
        /// </summary>
        /// <param name="statusCode">code from 1xx to 5xx</param>
        /// <param name="words">founded words if GET response</param>
        /// <returns></returns>
        public static string CreateResponse(int statusCode, string[] words = null)
        {
            var response = new StringBuilder();
            var contentLength = GetContentLength(words);
            string header = CreateResponseHeader(statusCode, contentLength);
            response.Append(header);
            response.Append("\n\n");
            if (words != null)
            {
                foreach (string word in words)
                {
                    response.Append(word + '\n');
                }
            }
            
            return response.ToString();
        }

        public static string[] CreateAttributes(List<Morpheme> morphemes)
        {
            string[] attributes = new string[morphemes.Count];
            for (int i = 0; i < morphemes.Count; i++)
            {
                try
                {
                    char type = GetMorphemeType(morphemes[i]);
                    attributes[i] = type + morphemes[i].Value;
                }
                catch (Exception)
                {
                    continue;
                }     
            }
            return attributes;
        }

        private static char GetMorphemeType(Morpheme morpheme)
        {
            switch (morpheme.MorphemeType)
            {
                case EMorphemeType.Pref:
                    return 'P';
                case EMorphemeType.Root:
                    return 'R';
                case EMorphemeType.Suff:
                    return 'S';
                default:
                    throw new Exception();
            }
        }

        private static string CreateResponseHeader(int responseCode, int contentLength)
        {
            var responseHeader = new StringBuilder("HTTP/1.0 ");
            responseHeader.Append(responseCode.ToString());
            responseHeader.Append(' ');
            responseHeader.Append(HttpUtils.GetResponseMessage(responseCode));
            responseHeader.Append("\nContent-Type: text\n");
            responseHeader.Append("Content-Length: ");
            responseHeader.Append(contentLength);
            responseHeader.Append("\n");
            return responseHeader.ToString();
        }

        private static int GetContentLength(string[] content)
        {
            if (content == null)
            {
                return 0;
            }
            int length = 0;
            foreach (string line in content)
            {
                length += line.Length;
            }
            return length;
        }

        private static string CreateRequestHeader(string word, string requestType, string host)
        {
            var header = new StringBuilder();
            header.Append(requestType);
            header.Append(" /");
            header.Append(word);
            header.Append(" HTTP/1.0\n\n");
            return header.ToString();
        }

        private static string CreateRequestBody(string[] attributes)
        {
            if (attributes == null)
            {
                return "";
            }
            var body = string.Join("&",attributes);
            return body.ToString();
        }


    }
}
