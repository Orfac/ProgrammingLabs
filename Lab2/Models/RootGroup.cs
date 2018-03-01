using System.Collections.Generic;

namespace Lab2.Models
{
    // Class used for containing words with same morpheme Root
    public sealed class RootGroup
    {

        public List<Word> Words { get; private set; }
        public string Root { get; private set; }

        public RootGroup(string root)
        {
            Words = new List<Word>();
            Root = root;
        }

        /// <summary>
        /// Used to add new element in right place like iteration
        /// of insertion sorting where key is Word.Morphemes.Count.
        /// </summary>
        /// <param name="newElem">new element</param>
        public void Add(Word newElem)
        {

            if (newElem.Root != Root)
            {
                return;
            }

            for (int i = 0; i < Words.Count; i++)
            {
                if (newElem.Morphemes.Count < Words[i].Morphemes.Count)
                {
                    Words.Insert(i, newElem);
                    return;
                }
                    
            }

            //If there are no place to insert between current elements
            //word will be added to the end of list.
            Words.Add(newElem);
            
        }

        // Used to verify the presence of the word in the group
        public bool Contains(string value)
        {
            foreach (var word in Words)
            {
                if (value == word.Value) return true;
            }
            return false;
        }
    }
}