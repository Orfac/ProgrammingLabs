using System;
using System.Collections.Generic;

namespace Lab1.Models
{  
    public sealed class RootDictionary
    {
        private Dictionary<string, RootGroup> rootGroups;

        public RootDictionary()
        {
            rootGroups = new Dictionary<string, RootGroup>();
        }

        public void ConsoleRun()
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
                if (this.Contains(value))
                {
                    PrintCognateWords(value);
                }
                else
                {
                    Console.Write("Неизвестное слово. Хотите добавить его в словарь (y/n)?");
                    string answer = Console.ReadLine();
                    if (answer.ToLower() == "y")
                    {
                        Word word = WordParser.Parse(value); 
                        this.Add(word);
                    }
                }
                Console.Write(">");
            }
        }

        public bool Contains(string word)
        {
            foreach (var RootGroup in rootGroups)
            {
                if (RootGroup.Value.Contains(word)) return true;
            }
            return false;
        }

        private string GetRoot(string value)
        {
            foreach (var RootGroup in rootGroups)
            {
                if (RootGroup.Value.Contains(value)) return RootGroup.Value.Root;
            }
            return null;
        }

        public void Add(Word NewWord)
        {
            if (GetRoot(NewWord.Root) == null)
            {
                rootGroups.Add(NewWord.Root, new RootGroup(NewWord.Root));
            }
            rootGroups[NewWord.Root].Add(NewWord);
            Console.WriteLine("Слово " + NewWord.Value + " добавлено.");
        }

        public void PrintCognateWords(string value)
        {
            string root = GetRoot(value);
            if (root != null)
            {
                Console.WriteLine("Известные однокоренные слова: ");
                ConsoleOutput.Print(rootGroups[root]);
            }

        }

        
    }
}