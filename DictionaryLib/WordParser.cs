using System.Text;
using DictionaryLib.Models;

namespace DictionaryLib
{
    // Uses for parse word to morphemes
    public class WordParser
    {

        /// <summary>
        /// Parses word and sets it's root
        /// </summary>
        /// <param name="value"> word which should be parsed</param>
        /// <returns> word as class with morphemes and root</returns>
        public static Word ConsoleParse(string value)
        {
            Word parsedWord = new Word(value);
            do
            {
                // do this if word didn't pass validation
                parsedWord.Morphemes.Clear();
                parsedWord.Root = Input.ConsoleReadWordMorphemes(parsedWord);
            } while (!IsComplexWordValid(parsedWord));

            return parsedWord;
        }

        /// <summary>
        /// Checks matching of morphemes and word
        /// </summary>
        /// <param name="word">word with morphemes</param>
        /// <returns>true if valid</returns>
        public static bool IsComplexWordValid(Word word)
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