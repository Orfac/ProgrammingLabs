using System;
using System.Collections.Generic;
using Lab1.Models;

namespace Lab1
{
    public class ConsoleInput 
    {
        private static Morpheme ReadSingleMorpheme(EMorphemeType type)
        {
            Morpheme morpheme = null;
            ConsoleOutput.Print(type);
            string value = Console.ReadLine();
            if (type == EMorphemeType.Root)
            {
                // Root can't be null a-priory
                while (String.IsNullOrWhiteSpace(value))
                {
                    ConsoleOutput.PrintError(EErrorType.EmptyRoot);
                    ConsoleOutput.Print(type);
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

        public static void ReadWordMorphemes(Word word)
        {
            ReadMorphemesInList(word.Morphemes, EMorphemeType.Pref);
            ReadMorphemesInList(word.Morphemes, EMorphemeType.Root);
            ReadMorphemesInList(word.Morphemes, EMorphemeType.Suff);
        }

    }
}