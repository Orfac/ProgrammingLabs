using System.Text;

namespace Lab1.Models
{
    // Uses for parse word to morphemes
    public class WordParser
    {
        // Parses word and sets it's root
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