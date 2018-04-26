using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Lab3Server
{
    class Client
    {
        public Client(TcpClient Client)
        {
            string ResponseBody = "It works!";
            var response = new StringBuilder();
            // Response header
            response.Append("HTTP/1.1 200 OK\nContent-type: text\nContent-Length:");
            response.Append(ResponseBody.Length.ToString());
            response.Append("\n\n");
            response.Append(ResponseBody);
            byte[] Buffer = Encoding.ASCII.GetBytes(response.ToString());
            Client.GetStream().Write(Buffer, 0, Buffer.Length);
            Client.Close();
        }
    }
}
