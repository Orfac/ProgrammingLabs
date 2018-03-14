using Lab1.Models;

namespace Lab1
{
    class Program
    {
        
        static void Main(string[] args)
        {
            RootDictionary dictionary = new RootDictionary();
            DictionaryRunner runner = new DictionaryRunner();
            runner.ConsoleRun(dictionary);
        }
    }
}
