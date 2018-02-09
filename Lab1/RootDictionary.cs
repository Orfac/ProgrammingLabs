using System;
using System.Collections.Generic;


namespace Lab1
{
    sealed class RootDictionary
    {
        Dictionary<string, RootGroup> RootGroups;

        public RootDictionary()
        {
            this.RootGroups = new Dictionary<string, RootGroup>();
        }

        public void Run()
        {
            Console.Write(">");
            string word = Console.ReadLine();
            while (word != "q")
            {
                if (this.Contains(word))
                {
                    this.PrintCognateWords(word);
                }
                else
                {
                    Console.Write("Неизвестное слово. Хотите добавить его в словарь (y/n)?");
                    string ans = Console.ReadLine();
                    if (ans == "y")
                    {
                        this.Add(word);
                    }
                }
                Console.Write(">");
                word = Console.ReadLine();
            }
        }

        public bool Contains(string word)
        {
            foreach (var RootGroup in this.RootGroups)
            {
                if (RootGroup.Value.Contains(word))
                {
                    return true;
                }
            }
            return false;
        }

        public bool Contains(Word word)
        {
            foreach (var RootGroup in this.RootGroups)
            {
                if (RootGroup.Value.Contains(word))
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsRootExist(string root)
        {
            foreach (var RootGroup in this.RootGroups)
            {
                if (RootGroup.Value.Root == root)
                {
                    return true;
                }
            }
            return false;
        }

        public void Add(string word)
        {
            Word NewWord = new Word(word);
            if (!IsRootExist(NewWord.Root))
            {
                this.RootGroups.Add(NewWord.Root, new RootGroup(NewWord.Root));
            }
            this.RootGroups[NewWord.Root].Add(NewWord);
            Console.WriteLine("Слово " + word + " добавлено.");
        }

        public string GetRoot(string word)
        {
            if (this.Contains(word))
            {
                foreach (var RootGroup in this.RootGroups)
                {
                    foreach (var w in RootGroup.Value.Words)
                    {
                        if (w.Value == word)
                        {
                            return w.Root;
                        }
                    }
                }
                return null;
            }
            else
            {
                return null;
            }


        }

        public void PrintCognateWords(string word)
        {
            string root = this.GetRoot(word);
            if (root != null)
            {
                this.RootGroups[root].PrintWords();
            }

        }



    }
}