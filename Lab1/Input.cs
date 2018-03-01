using System;
using System.Collections.Generic;
using Lab1.Models;

namespace Lab1
{
    // Input needs for get values like word or morphemes
    public class Input 
    {
        // Read one morpheme using type of morpheme
        private static Morpheme ReadSingleMorpheme(EMorphemeType type)
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
        private static void ReadMorphemesInList(List<Morpheme> morphemes, EMorphemeType type)
        {
            Morpheme morpheme;
            morpheme = ReadSingleMorpheme(type);
            if (type == EMorphemeType.Root)
            {
                morphemes.Add(morpheme);
            }
            else
            {
                while (morpheme != null)
                {
                    morphemes.Add(morpheme);
                    morpheme = ReadSingleMorpheme(type);
                }
            }
        }

        // Reads all morphemes in word
        public static void ReadWordMorphemes(Word word)
        {
            ReadMorphemesInList(word.Morphemes, EMorphemeType.Pref);
            ReadMorphemesInList(word.Morphemes, EMorphemeType.Root);
            ReadMorphemesInList(word.Morphemes, EMorphemeType.Suff);
        }

    }
}