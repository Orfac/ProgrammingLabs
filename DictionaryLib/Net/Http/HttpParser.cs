using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DictionaryLib.Models;

namespace DictionaryLib.Net
{
    /// <summary>
    /// Uses to parse string reponses and requests 
    /// </summary>
    public static class HttpParser
    {
        /// <summary>
        /// Parses body from http 1.0 which is denoted by \n\n
        /// </summary>
        /// <param name="response"> http 1.0 response in string view without clrf</param>
        /// <returns></returns>
        public static string[] ParseResponseBody(string response)
        {
            int bodyStartIndex = response.IndexOf("\n\n");
            return response.Substring(bodyStartIndex).Split('\n');
        }

        /// <summary>
        /// Uses to get status code from response 
        /// </summary>
        /// <param name="response">http response utf-8</param>
        /// <returns></returns>
        public static int ParseStatusCode(string response)
        {
            int codeStartIndex = response.IndexOf(' ') + 1;
            int codeEndIndex = codeStartIndex + 3;
            int length = codeEndIndex - codeStartIndex;
            string statusCode = response.Substring(codeStartIndex, length);
            return Convert.ToInt32(statusCode);
        }

        /// <summary>
        /// Uses to take word to find from request
        /// </summary>
        /// <param name="request">http request utf-8</param>
        /// <returns></returns>
        public static string ParseGetRequest(string request)
        {
            return ParseValueFromLink(request);
        }

        /// <summary>
        /// Uses to create word and set it's morpheme from request
        /// </summary>
        /// <param name="request">http 1.0 request</param>
        /// <returns>parsed word</returns>
        public static Word ParsePostRequest(string request)
        {
            string value = ParseValueFromLink(request);
            int bodyStartIndex = request.IndexOf("\n\n") + 2;
            string[] morphemesWithTypes;
            morphemesWithTypes = request.Substring(bodyStartIndex).Split('&');
            ParseMorphemes(morphemesWithTypes, out List<Morpheme> morphemes, out string root);
            return new Word(morphemes, value, root);
        }

        public static HttpUtils.RequestType ParseRequestType(string request)
        {
            int typeEndIndex = request.IndexOf(' ');
            string type = request.Substring(0, typeEndIndex);
            switch (type)
            {
                case "GET":
                    return HttpUtils.RequestType.GET;
                case "POST":
                    return HttpUtils.RequestType.POST;
                default:
                    return HttpUtils.RequestType.UNDEFINED;
            }
        }

        private static void ParseMorphemes(string[] morphemesWithTypes, out List<Morpheme> morphemes, out string root)
        {
            morphemes = new List<Morpheme>();
            root = null;
            for (int i = 0; i < morphemesWithTypes.Length; i++)
            {
                var newMorpheme = new Morpheme
                {
                    Value = morphemesWithTypes[i].Substring(1)
                };
                SetMorphemeType(ref newMorpheme, out bool IsRoot, morphemesWithTypes[i][0]);

                if (IsRoot)
                {
                    root = newMorpheme.Value;
                }
                morphemes.Add(newMorpheme);
            }
        }

        private static string ParseValueFromLink(string request)
        {
            int wordStartIndex = request.IndexOf('/') + 1;
            int wordEndIndex = request.IndexOf(' ', wordStartIndex);
            int length = wordEndIndex - wordStartIndex;
            return request.Substring(wordStartIndex, length);
        }

        private static void SetMorphemeType(ref Morpheme morpheme, out bool IsRoot, char type)
        {
            switch (type)
            {
                case 'S':
                    morpheme.MorphemeType = EMorphemeType.Suff;
                    IsRoot = false;
                    break;
                case 'R':
                    morpheme.MorphemeType = EMorphemeType.Root;
                    IsRoot = true;
                    break;
                case 'P':
                    morpheme.MorphemeType = EMorphemeType.Pref;
                    IsRoot = false;
                    break;
                default:
                    IsRoot = false;
                    break;
            }
        }
    }
}
