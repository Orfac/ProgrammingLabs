using System;
using System.Collections.Generic;
using Lab1.Morphemes;

namespace Lab1
{
    public sealed class Word
    {
        public List<Morpheme> Morphemes { get; set; }
        public string Value { get; set; }
        public string Root { get; set; }

        public Word(string value)
        {
            Morphemes = new List<Morpheme>();
            ReadMorphemes();
            while (value != Morpheme.ListMorphemesToString(Morphemes))
            {
                Console.WriteLine("Слово не соответсвует морфемам. " +
                    "Введите морфемы снова.");
                Morphemes.Clear();
                ReadMorphemes();
            }
            Root = Morpheme.GetRoot(Morphemes);
            Value = value;
        }

        public Word(List<Morpheme> morphemes, string value, string root)
        {
            Morphemes = morphemes;
            Value = value;
            Root = root;
        }

        private void ReadMorphemes()
        {
            Morpheme.Read(Morphemes, EMorphemeType.Pref);
            Morpheme.Read(Morphemes, EMorphemeType.Root, true);
            Morpheme.Read(Morphemes, EMorphemeType.Suff);
        }

        public override string ToString()
        {
            string res = "";
            foreach (var m in Morphemes)
            {
                res += m.ToString();
            }
            return res;
        }



    }
}