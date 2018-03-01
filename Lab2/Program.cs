using Lab2.Models;

namespace Lab2
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
