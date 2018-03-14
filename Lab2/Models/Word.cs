using System.Collections.Generic;
using System.Text;

namespace Lab2.Models
{
    /// <summary>
    /// Used for contain about word - list of morphemes, value of word and root
    /// </summary>
    public sealed class Word
    {
        /// <summary>
        /// Morphemes of word
        /// </summary>
        public List<Morpheme> Morphemes { get; set; }
        /// <summary>
        /// General value
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Root of word
        /// </summary>
        public string Root { get; set; }

        /// <summary>
        /// Initialize List of morphemes
        /// </summary>
        /// <param name="value"> general value </param>
        public Word(string value)
        {
            Value = value;
            Morphemes = new List<Morpheme>();
        }

        /// <summary>
        /// Sets Morphemes as morphemes, also sets Value as value and Root as root
        /// </summary>
        /// <param name="morphemes">list of morphemes</param>
        /// <param name="value">Value</param>
        /// <param name="root">Root</param>
        public Word(List<Morpheme> morphemes, string value, string root)
        {
            Morphemes = morphemes;
            Value = value;
            Root = root;
        }

        /// <summary>
        /// Initialize list of morphemes
        /// </summary>
        public Word()
        {
            Morphemes = new List<Morpheme>();
        }

        /// <summary>
        /// Returns splited morphemes
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder wordBuilder = new StringBuilder();
            foreach (Morpheme morpheme in Morphemes)
            {
                wordBuilder.Append(morpheme.ToString());
            }
            return wordBuilder.ToString();
        }



    }
}