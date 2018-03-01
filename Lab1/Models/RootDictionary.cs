using System;
using System.Collections.Generic;

namespace Lab1.Models
{  
    // Class used for containing and handling information about RootGroups
    public sealed class RootDictionary
    {
        private Dictionary<string, RootGroup> rootGroups;
        public RootDictionary()
        {
            rootGroups = new Dictionary<string, RootGroup>();
        }

        // If word contains in dictionary return true otherwise false
        public bool Contains(string word)
        {
            foreach (var RootGroup in rootGroups)
            {
                if (RootGroup.Value.Contains(word)) return true;
            }
            return false;
        }

        // Get relevant root by word or null if there is no such word in dictionary
        private string GetRoot(string word)
        {
            foreach (var RootGroup in rootGroups)
            {
                if (RootGroup.Value.Contains(word)) return RootGroup.Value.Root;
            }
            return null;
        }

        // Adds new word to relevant root groop and creates RootGroup if it's necessary
        public void Add(Word NewWord)
        {
            if (GetRoot(NewWord.Root) == null)
            {
                rootGroups.Add(NewWord.Root, new RootGroup(NewWord.Root));
            }
            rootGroups[NewWord.Root].Add(NewWord);
            Console.WriteLine("Слово " + NewWord.Value + " добавлено.");
        }

        // Prints all words from dictionary with same root as word
        public void PrintCognateWords(string word)
        {
            string root = GetRoot(word);
            if (root != null)
            {
                Console.WriteLine("Известные однокоренные слова: ");
                Output.Print(rootGroups[root]);
            }

        }

        
    }
}