using Lab2.Models;
using Lab2.Xml;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Lab2
{
   
    class Program
    {
        
        static void Main(string[] args)
        {
            var dictionary = new RootDictionary();
            var dialoger = new XmlConsoleDialoger();
            dialoger.StartReadDialog(dictionary);
            var runner = new DictionaryRunner();
            runner.ConsoleRun(dictionary);
            dialoger.StartWriteDialog(dictionary);
        }
    }
}
