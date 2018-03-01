using Lab2.Models;
using System;

namespace Lab2
{
    // Used for working with dictionary
    public class DictionaryRunner
    {
        // used for run dictionary from console
        public void ConsoleRun(RootDictionary rootDictionary)
        {
            Console.Write(">");
            string value;
            while ((value = Console.ReadLine()) != "q")
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    Console.Write(">");
                    continue;
                }
                if (rootDictionary.Contains(value))
                {
                    rootDictionary.PrintCognateWords(value);
                }
                else
                {
                    Console.Write("Неизвестное слово. Хотите добавить его в словарь (y/n)?");
                    string answer = Console.ReadLine();
                    if (answer.ToLower() == "y")
                    {
                        Word word = WordParser.Parse(value);
                        rootDictionary.Add(word);
                    }
                }
                Console.Write(">");
            }
        }
    }
}
