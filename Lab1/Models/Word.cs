using System.Collections.Generic;

namespace Lab1.Models
{
    // Used for contain about word - list of morphemes, value of word and root
    public sealed class Word
    {
        public List<Morpheme> Morphemes { get; set; }
        public string Value { get; private set; }
        public string Root { get; set; }

        public Word(string value)
        {
            Value = value;
            Morphemes = new List<Morpheme>();
        }

        public Word(List<Morpheme> morphemes, string value, string root)
        {
            Morphemes = morphemes;
            Value = value;
            Root = root;
        }

        public override string ToString()
        {
            string result = "";
            foreach (Morpheme morpheme in Morphemes)
            {
                result += morpheme.ToString();
            }
            return result;
        }



    }
}