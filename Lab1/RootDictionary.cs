using System;
using System.Collections.Generic;


namespace Lab1
{
    sealed class RootDictionary
    {
        Dictionary<string, RootGroup> RootGroups;

        public RootDictionary()
        {
            RootGroups = new Dictionary<string, RootGroup>();
        }

        public void Run()
        {
            Console.Write(">");
            string value;
            while ((value = Console.ReadLine()) != "q")
            {
                if (value == "") { Console.Write(">"); continue; }
                if (Contains(value))
                {
                    PrintCognateWords(value);
                }
                else
                {
                    Console.Write("Неизвестное слово. Хотите добавить его в словарь (y/n)?");
                    string ans = Console.ReadLine();
                    if (ans.ToLower() == "y")
                    {
                        Word word = new Word(value); 
                        Add(word);
                    }
                }
                Console.Write(">");
            }
        }

        public bool Contains(string word)
        {
            foreach (var RootGroup in RootGroups)
            {
                if (RootGroup.Value.Contains(word)) return true;
            }
            return false;
        }

        private bool ContainsRoot(string root)
        {
            foreach (var RootGroup in RootGroups)
            {
                if (RootGroup.Value.Root == root) return true;
            }
            return false;
        }

        private string GetRoot(string value)
        {
            foreach (var RootGroup in RootGroups)
            {
                if (RootGroup.Value.Contains(value)) return RootGroup.Value.Root;
            }
            return null;
        }

        public void Add(Word NewWord)
        {
            if (!ContainsRoot(NewWord.Root)) RootGroups.Add(NewWord.Root, new RootGroup(NewWord.Root));
            RootGroups[NewWord.Root].Add(NewWord);
            Console.WriteLine("Слово " + NewWord.Value + " добавлено.");
        }

        public void PrintCognateWords(string value)
        {
            string root = GetRoot(value);
            if (root != null)
            {
                Console.WriteLine("Известные однокоренные слова: ");
                foreach (var word in RootGroups[root].Words)
                {
                    Console.WriteLine(word.Value.ToString());
                }
            }

        }

        
    }
}