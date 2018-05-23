﻿using DictionaryLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DictionaryLib.Net.Http
{
    public static class WordWrapper
    {
        private static char splitter = '&';

        public static string GetAttributes(List<Morpheme> morphemes = null)
        {
            if (morphemes == null)
            {
                return "";
            }

            var attributes = new StringBuilder();
            AddMorpheme(attributes, morphemes[0]);
            for (int i = 1; i < morphemes.Count; i++)
            {
                attributes.Append(splitter);
                AddMorpheme(attributes, morphemes[i]);
            }
            return attributes.ToString();
        }

        public static void SetMorphemes(Word word, string body)
        {
            if (body == null) return;
            

        }

        private static List<Morpheme> GetMorphemes(out string root, string body)
        {
            root = null;
            if (body == null) return null;

            string[] typedMorphemes = body.Split(splitter);
            List<Morpheme> morphemes = new List<Morpheme>();
            for (int i = 0; i < typedMorphemes.Length; i++)
            {
                Morpheme newMorpheme;
                newMorpheme = GetMorphemeFromString(typedMorphemes[i], out root);
                morphemes.Add(newMorpheme);
            }
            return morphemes;
        }

        private static Morpheme GetMorphemeFromString(string typedMorpheme, out string root)
        {
            root = null;
            if (typedMorpheme == null)
            {
                return null;
            }
            try
            {
                EMorphemeType type = GetTypeFromTypedMorpheme(typedMorpheme[0]);
                Morpheme resultMorpheme = new Morpheme
                {
                    Value = typedMorpheme.Substring(1),
                    MorphemeType = type
                };
                if (type == EMorphemeType.Root)
                {
                    root = resultMorpheme.Value;
                }
                return resultMorpheme;
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        private static EMorphemeType GetTypeFromTypedMorpheme(char type)
        {
            switch (type)
            {
                case 'S':
                    return EMorphemeType.Suff;
                case 'R':
                    return EMorphemeType.Root;
                case 'P':
                    return EMorphemeType.Pref;
                default:
                    throw new Exception(message:"Unknown morpheme type");
            }
        }

        private static void AddMorpheme(StringBuilder sb, Morpheme morpheme)
        {
            sb.Append(GetMorphemeType(morpheme));
            sb.Append(morpheme.Value);
        }
        private static char GetMorphemeType(Morpheme morpheme)
        {
            switch (morpheme.MorphemeType)
            {
                case EMorphemeType.Pref:
                    return 'P';
                case EMorphemeType.Root:
                    return 'R';
                case EMorphemeType.Suff:
                    return 'S';
                default:
                    throw new Exception();
            }
        }



    }
}