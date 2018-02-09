using System;
using System.Collections.Generic;

namespace Lab1
{
    sealed class Word
    {
        public string Root { get; private set; }
        public string Value { get; private set; }
        public int MorphemesCount { get; private set; }
        private List<string> prefixes;
        private List<string> suffixes;


        public Word(string value)
        {
            this.Value = value;
            this.prefixes = new List<string>();
            this.suffixes = new List<string>();
            this.Root = "";
            this.MorphemesCount = 0;
            while (!Parse())
            {
                Console.WriteLine("Морфемы были введены некорректно. Введите снова.");
            }
        }

        public Word(string value, List<string> prefixes, List<string> suffixes, string root, int MorphemesCount)
        {
            this.Value = value;
            this.prefixes = new List<string>();
            this.prefixes = prefixes;
            this.suffixes = new List<string>();
            this.suffixes = suffixes;
            this.Root = root;
            this.MorphemesCount = MorphemesCount;
        }

        public override string ToString()
        {
            string prefixesStr = (prefixes.Count > 0 ? String.Join("-", prefixes.ToArray()) + "-" : "");
            string suffixesStr = (suffixes.Count > 0 ? "-" + String.Join("-", suffixes.ToArray()) : "");
            return prefixesStr + Root + suffixesStr;
        }

        private bool Parse()
        {
            SetMorphemes();
            return CheckParsedWord();

        }

        private void SetMorphemes()
        {
            SetPrefixesFromConsole();
            SetRootFromConsole();
            SetSuffixesFromConsole();
        }

        private bool CheckParsedWord()
        {
            string parsedWord = "";
            foreach (var pref in prefixes)
            {
                parsedWord += pref;
            }
            parsedWord += Root;
            foreach (var suff in suffixes)
            {
                parsedWord += suff;
            }

            if (parsedWord == Value)
            {
                return true;
            }
            else
            {
                this.prefixes.Clear();
                this.suffixes.Clear();
                this.Root = "";
                this.MorphemesCount = 0;
                return false;
            }
        }

        private void SetPrefixesFromConsole()
        {
            Console.Write("\t\t\tприставка:");
            string value = Console.ReadLine();
            while (value != "")
            {
                this.MorphemesCount++;
                prefixes.Add(value);
                Console.Write("\t\t\tприставка:");
                value = Console.ReadLine();
            }
        }

        private void SetRootFromConsole()
        {
            Console.Write("\t\t\tкорень:");
            Root = Console.ReadLine();
            this.MorphemesCount++;
        }

        private void SetSuffixesFromConsole()
        {
            Console.Write("\t\t\tсуффикс или окончание:");
            string value = Console.ReadLine();
            while (value != "")
            {
                this.MorphemesCount++;
                suffixes.Add(value);
                Console.Write("\t\t\tсуффикс или окончание:");
                value = Console.ReadLine();
            }
        }


    }
}