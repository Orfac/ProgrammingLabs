using System;
using System.Collections.Generic;

namespace Lab2
{
    sealed class RootGroup
    {
        public List<Word> Words;
        public string Root { get; private set; }

        public RootGroup(string root)
        {
            this.Words = new List<Word>();
            this.Root = root;
        }

        public void Add(Word word)
        {
            try
            {
                if (word != null)
                {
                    for (int i = 0; i < Words.Count; i++)
                    {
                        if (Words[i].MorphemesCount > word.MorphemesCount)
                        {
                            Words.Insert(i, word);
                            return;
                        }
                    }
                    Words.Add(word);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Не удалось выделить память, попробуйте позднее");
                throw;
            }

        }

        public void PrintWords()
        {
            Console.WriteLine("Известные однокоренные слова");
            foreach (var word in this.Words)
            {
                Console.WriteLine("\t\t\t" + word.Value);
            }
        }

        public bool Contains(Word word)
        {
            return Words.Contains(word);
        }

        public bool Contains(string word)
        {
            foreach (var w in this.Words)
            {
                if (w.Value == word)
                {
                    return true;
                }
            }
            return false;
        }
    }
}