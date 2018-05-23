using System.Text;

namespace Lab3Client
{
    class Program
    {
        static void Main(string[] args)
        {
            string host = "127.0.0.1";
            int port = 5999;
            var client = new ConsoleClient(host, port, Encoding.UTF8);
            client.Run();         
        }
    }
}
