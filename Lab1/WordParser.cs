using System.Text;
using Lab1.Models;

namespace Lab1
{
    // Uses for parse word to morphemes
    public class WordParser
    {

        /// <summary>
        /// Parses word and sets it's root
        /// </summary>
        /// <param name="value"> word which should be parsed</param>
        /// <returns> word as class with morphemes and root</returns>
        public static Word Parse(string value)
        {
            Word parsedWord = new Word(value);
            do
            {
                // do this if word didn't pass validation
                parsedWord.Morphemes.Clear();
                parsedWord.Root = Input.ReadWordMorphemes(parsedWord);
            } while (!IsComplexWordValid(parsedWord));

            return parsedWord;
        }

        // Checks matching of morphemes and word
        private static bool IsComplexWordValid(Word word)
        {
            var complexWord = new StringBuilder();
            foreach (Morpheme morpheme in word.Morphemes)
            {
                complexWord.Append(morpheme.Value);
            }
            return complexWord.ToString() == word.Value;
        }
    }
}