using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab1
{
    sealed class RootGroup
    {
        public List<Word> Words { get; private set; }
        public string Root { get; private set; }

        public RootGroup(string root)
        {
            Words = new List<Word>();
            Root = root;
        }

        public void Add(Word value)
        {
            if (value.Root != Root) return;
            for (int i = 0; i < Words.Count; i++)
            {
                if (value.Morphemes.Count < Words[i].Morphemes.Count)
                {
                    Words.Insert(i, value);
                    return;
                }
                    
            }
            Words.Add(value);
            
        }

        // Used to verify the presence of the word in the group
        public bool Contains(string value)
        {
            foreach (var w in Words)
            {
                if (value == w.Value) return true;
            }
            return false;
        }

        
    }
}