﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DictionaryLib.Net.Http
{
    public class Request
    {
        private RequestType _type;
        private string _value;
        private string _attributes;

        public RequestType Type => _type;
        public string Value => _value;
        public string Attributes => _attributes;

        public Request(RequestType type, string value, string attributes = null)
        {
            this._type = type;
            this._value = value;
            this._attributes = attributes;
        }

        public static Request Parse(string request)
        {
            var type = ParseRequestType(request);
            string value = null;
            string attributes = null;
            value = ParseValueFromLink(request);
            attributes = ParseRequestBody(request);
            return new Request(type, value, attributes);
        }

        public override string ToString()
        {
            var request = new StringBuilder();
            request.Append(CreateRequestHeader(_type, _value));
            if (_attributes != null)
            {
                request.Append(_attributes);
            }
            return request.ToString();
        }

        private string CreateRequestHeader(RequestType type, string value)
        {
            var header = new StringBuilder();
            header.Append(TypeToString(type));
            header.Append(" /");
            header.Append(value);
            header.Append(" HTTP/1.0\n\n");
            return header.ToString();
        }

        private static RequestType ParseRequestType(string request)
        {
            try
            {
                int typeEndIndex = request.IndexOf(' ');
                string type = request.Substring(0, typeEndIndex);
                return StringToType(type);
            }
            catch (InvalidOperationException)
            {
                throw;
            }

        }

        private static string ParseValueFromLink(string request)
        {
            try
            {
                int wordStartIndex = request.IndexOf('/') + 1;
                int wordEndIndex = request.IndexOf(' ', wordStartIndex);
                int length = wordEndIndex - wordStartIndex;
                return request.Substring(wordStartIndex, length);
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        private static string ParseRequestBody(string request)
        {
            int bodyStartIndex = request.IndexOf("\n\n") + 2;
            if (bodyStartIndex >= 2)
            {
                return request.Substring(bodyStartIndex);
            }
            return null;
        }

        private static RequestType StringToType(string type)
        {
            switch (type)
            {
                case "GET":
                    return RequestType.GET;
                case "POST":
                    return RequestType.POST;
                default:
                    throw new InvalidOperationException();
            }
        }

        private string TypeToString(RequestType type)
        {
            switch (type)
            {
                case RequestType.POST:
                    return "POST";
                case RequestType.GET:
                    return "GET";
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
