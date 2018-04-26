using System;
using System.Collections.Generic;

namespace DictionaryLib.Models
{
    /// <summary>
    /// Class used for containing and handling information about RootGroups
    /// </summary>
    public sealed class RootDictionary
    {
        /// <summary>
        /// RootGroups with own roots and words
        /// </summary>
        public Dictionary<string, RootGroup> RootGroups { get; set; }

        /// <summary>
        /// Initialize rootGroups
        /// </summary>
        public RootDictionary()
        {
            RootGroups = new Dictionary<string, RootGroup>();
        }

        public RootDictionary(Dictionary<string, RootGroup> rootGroups)
        {
            this.RootGroups = rootGroups;
        }

        /// <summary>
        /// Returns existense of word
        /// </summary>
        /// <param name="word"> word </param>
        /// <returns> If word contains in dictionary return true otherwise false </returns>
        public bool Contains(string word)
        {
            foreach (var RootGroup in RootGroups)
            {
                if (RootGroup.Value.Contains(word)) return true;
            }
            return false;
        }

        /// <summary>
        /// hiu
        /// </summary>
        /// <param name="word">word</param>
        /// <returns>asdasd</returns>
        public string GetRoot(string word)
        {
            foreach (var RootGroup in RootGroups)
            {
                if (RootGroup.Value.Contains(word)) return RootGroup.Value.Root;
            }
            return null;
        }

        //  Checks if rootgroup exists with this root     
        private bool IsRootExists(string root)
        {
            return RootGroups.ContainsKey(root);
        }

        /// <summary>
        /// Adds new word to relevant root groop and creates RootGroup if it's necessary
        /// </summary>
        /// <param name="NewWord"> new word</param>
        public void Add(Word NewWord)
        {
            if (!IsRootExists(NewWord.Root))
            {
                RootGroups.Add(NewWord.Root, new RootGroup(NewWord.Root));
            }
            RootGroups[NewWord.Root].Add(NewWord);
            Console.WriteLine("Слово " + NewWord.Value + " добавлено.");
        }
        
    }
}