using DictionaryLib.Models;
using DictionaryLib.Xml;
using System.Text;

namespace Lab3Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var dictionary = new RootDictionary();
            var dialoger = new XmlConsoleDialoger();
            dialoger.StartReadDialog(dictionary);

            var server = new Server(5999, 4, dictionary, Encoding.UTF8);
            server.Run();
         
        }
    }
}
