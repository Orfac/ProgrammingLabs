using System.Text;

namespace Lab1.Models
{
    public class WordParser
    {
        public static Word Parse(string value)
        {
            Word parsedWord = new Word(value);
            do
            {
                // do this if word didn't pass validation
                parsedWord.Morphemes.Clear();
                Input.ReadWordMorphemes(parsedWord);
            } while (!IsComplexWordValid(parsedWord));

            return parsedWord;
        }

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