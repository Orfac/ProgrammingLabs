﻿using DictionaryLib.Models;
using System;
using System.Xml.Serialization;
using System.IO;


namespace DictionaryLib
{
    /// <summary>
    /// Used for print all models printable types or write something in file
    /// </summary>
    public static class Output
    {
        public static void Print(EMorphemeType morphemeType)
        {
            switch (morphemeType)
            {
                case EMorphemeType.Pref:
                    Console.Write("\t\t\tприставка: ");
                    break;
                case EMorphemeType.Root:
                    Console.Write("\t\t\tкорень: ");
                    break;
                case EMorphemeType.Suff:
                    Console.Write("\t\t\tсуффикс или окончание: "); 
                    break;
                default:
                    break;
            }

        }

        public static void Print(Word word)
        {
            Console.WriteLine(word.ToString());
        }

        public static void PrintError(EErrorType error)
        {
            Console.WriteLine(ErrorHandler.ErrorToString(error));
        }

        public static void Print(RootGroup rootGroup)
        {
            foreach (Word word in rootGroup.Words)
            {
                Print(word);
            }
        }

       
    }
}
