using DictionaryLib.Models;
using DictionaryLib.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

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
            try
            {
                server.Run();
            }
            catch (Exception e )
            {
                Console.WriteLine("Error", e.Message);
            }
         
        }
    }
}
