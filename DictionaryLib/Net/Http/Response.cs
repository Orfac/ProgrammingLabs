using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DictionaryLib.Net.Http
{
    public class Response
    {
        private StatusCode _statusCode;
        private string _body;

        public StatusCode StatusCode => _statusCode;
        public string Body => _body;

        public Response(StatusCode statusCode, string body = null)
        {
            this._statusCode = statusCode;
            this._body = body;
        }

        public static Response Parse(string response)
        {
            StatusCode statusCode = ParseStatusCode(response);
            string body = null;
            body = ParseResponseBody(response);
            return new Response(statusCode, body);

        }

        private static StatusCode ParseStatusCode(string response)
        {
            try
            {
                int statusCodeLength = 3;
                int codeStartIndex = response.IndexOf(' ') + 1;
                string statusCode = response.Substring(codeStartIndex, statusCodeLength);
                return (StatusCode)Convert.ToInt32(statusCode);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
        }

        private static string ParseResponseBody(string response)
        {
            int bodyStartIndex = response.IndexOf("\n\n") + 2;
            return response.Substring(bodyStartIndex);
        }

        public override string ToString()
        {
            var response = new StringBuilder();
            var contentLength = GetContentLength(_body);
            string header = CreateResponseHeader(_statusCode, contentLength);
            response.Append(header);
            response.Append('\n');
            if (_body != null)
            {
                response.Append(_body);
            }
            return response.ToString();
        }

        private int GetContentLength(string content)
        {
            if (content == null)
            {
                return 0;
            }
            return content.Length;
        }

        private string CreateResponseHeader(StatusCode code, int contentLength)
        {
            var responseHeader = new StringBuilder("HTTP/1.0 ");
            responseHeader.Append(GetResponseMessage(code));
            responseHeader.Append("\nContent-Type: text\n");
            responseHeader.Append("Content-Length: ");
            responseHeader.Append(contentLength);
            responseHeader.Append('\n');
            return responseHeader.ToString();
        }

        private string GetResponseMessage(StatusCode code)
        {
            switch (code)
            {
                case StatusCode.OK:
                    return ("200 OK");
                case StatusCode.Created:
                    return ("201 Created");
                case StatusCode.BadRequest:
                    return ("400 Bad request");
                case StatusCode.ObjectNotFound:
                    return ("404 Object not found");
                case StatusCode.Сonflict:
                    return ("409 Conflict");
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
