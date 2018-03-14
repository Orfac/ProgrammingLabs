using Lab2.Models;
using System;

namespace Lab2
{
    /// <summary>
    /// Used for working with dictionary
    /// </summary>
    public class DictionaryRunner
    {
        /// <summary>
        /// used for run dictionary from console
        /// </summary>
        /// <param name="rootDictionary"> dictionary to run </param>
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
                    Console.WriteLine("Известные однокоренные слова: ");
                    string root = rootDictionary.GetRoot(value);
                    Output.Print(rootDictionary.RootGroups[root]);
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
