using System;
using System.Collections.Generic;

namespace Lab1.Morphemes
{
    public sealed class Morpheme
    {
        public EMorphemeType MorphemeType { get; private set; }
        public string Value { get; private set; }

        public Morpheme(EMorphemeType morphemeType, string value)
        {
            MorphemeType = morphemeType;
            Value = value;
        }

        public override string ToString()
        {
            switch (MorphemeType)
            {
                case EMorphemeType.Pref:
                    return Value + "-";
                case EMorphemeType.Root:
                    return Value;
                case EMorphemeType.Suff:
                    return "-" + Value;
                default:
                    return "";
            }
        }

        // Used to read and add to list single morpheme or list of morphemes
        public static void Read(List<Morpheme> list, EMorphemeType type, bool IsSingle = false)
        {
            Console.Write(type.ToPrintableString());
            string value;
            while (((value = Console.ReadLine()) == "") 
                == IsSingle) // Single value couldn't be empty, however list can be empty
            {
                if (IsSingle)
                {
                    Console.WriteLine("Ошибка: Значение не может быть пустым. Введите другое значение.");
                }
                else
                {
                    list.Add(new Morpheme(type, value));
                }
                Console.Write(type.ToPrintableString());
            }
            if (IsSingle) list.Add(new Morpheme(type, value));
        }

        // Used to make up a string from morpheme list
        public static string ListMorphemesToString(List<Morpheme> list)
        {
            string res = "";
            foreach (var m in list)
            {
                res += m.Value;
            }
            return res;
        }

        public static string GetRoot(List<Morpheme> list)
        {
            foreach (var m in list)
            {
                if (m.MorphemeType == EMorphemeType.Root)
                {
                    return m.Value;
                }
            }
            return null;
        }

    }
}
