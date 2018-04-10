using System;
using System.Collections.Generic;
using Lab2.Models;


namespace Lab2
{
    /// <summary>
    /// Input needs for get values like word or morphemes
    /// </summary>
    public class Input 
    {
       
        private static Morpheme ConsoleReadSingleMorpheme(EMorphemeType type)
        {
            Morpheme morpheme = null;
            Output.Print(type);
            string value = Console.ReadLine();
            if (type == EMorphemeType.Root)
            {
                // Root can't be null a-priory
                while (String.IsNullOrWhiteSpace(value))
                {
                    Output.PrintError(EErrorType.EmptyRoot);
                    Output.Print(type);
                    value = Console.ReadLine();
                }
                morpheme = new Morpheme(type, value);
            }
            else
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    morpheme = new Morpheme(type, value);
                }
            }
            return morpheme;
        }

        /// <summary>
        /// Used to read one-typed morphemes
        /// </summary>
        /// <param name="morphemes"> place for writing</param>
        /// <param name="type"> morpheme type </param>
        private static void ConsoleReadMorphemesInList(List<Morpheme> morphemes, EMorphemeType type)
        {
            Morpheme morpheme;
            morpheme = ConsoleReadSingleMorpheme(type);
            if (type == EMorphemeType.Root)
            {
                morphemes.Add(morpheme);
            }
            else
            {
                while (morpheme != null)
                {
                    morphemes.Add(morpheme);
                    morpheme = ConsoleReadSingleMorpheme(type);
                }
            }
        }

        /// <summary>
        /// Reads all morphemes of word
        /// </summary>
        /// <param name="word"> word </param>
        /// <returns> root </returns>
        public static string ConsoleReadWordMorphemes(Word word)
        {
            ConsoleReadMorphemesInList(word.Morphemes, EMorphemeType.Pref);
            ConsoleReadMorphemesInList(word.Morphemes, EMorphemeType.Root);
            // gets root by last added morpheme value
            string root = word.Morphemes[word.Morphemes.Count - 1].Value;
            ConsoleReadMorphemesInList(word.Morphemes, EMorphemeType.Suff);
            return root;
        }

        
    }
}