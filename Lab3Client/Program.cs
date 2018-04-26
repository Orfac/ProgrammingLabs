using DictionaryLib;
using DictionaryLib.Models;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Lab3Client
{
    class Program
    {
        static void Main(string[] args)
        {
            string hostName = "127.0.0.1";
            int port = 80;
            var host = IPAddress.Parse(hostName);
            var hostep = new IPEndPoint(host, port);
            var sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var TcpClient = new TcpClient(hostName,port);
            sock.Connect(hostep);
            TcpClient.Client.Send(Encoding.ASCII.GetBytes(CreateHttpRequest("abcd", hostName, port)));
            byte[] response = new byte[256];
            int responseCode = TcpClient.Client.Receive(response);
            Console.WriteLine("Вводите слова для поиска или добавления в словарь.\nВведите q для выхода.");
            Console.Write(">");
            string value;
            while ((value = Console.ReadLine()) != "q")
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    Console.Write(">");
                    continue;
                }
                //if (rootdictionary.contains(value))
                //{
                //    console.writeline("известные однокоренные слова: ");
                //    string root = rootdictionary.getroot(value);
                //    output.print(rootdictionary.rootgroups[root]);
                //}
                //else
                //{
                //    console.write("неизвестное слово. хотите добавить его в словарь (y/n)?");
                //    string answer = console.readline();
                //    if (answer.tolower() == "y")
                //    {
                //        word word = wordparser.parse(value);
                //        requesttoadd(word);
                //    }
                //}
                Console.Write(">");
            }
        }

        public static string CreateHttpRequest(string word, string hostname, int port)
        {
            var request = new StringBuilder();
            // Request header
            request.Append("GET http://");
            request.Append(hostname);
            request.Append(":");
            request.Append(port);
            request.Append("HTTP/1.1\r\n");
            request.Append("Connection: keep - alive\r\n");
            request.Append("Content - type: text\nContent - Length:");
            request.Append(word.Length.ToString());
            request.Append("\n\n");
            // Request body
            request.Append(word);
            return request.ToString();
        }

        public static int RequestToAdd(Word word)
        {
            return 0;
        }

        public static int RequestToSearch(string word)
        {
            return 0;
        }
    }
}
