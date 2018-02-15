namespace Lab1.Morphemes
{
    public enum EMorphemeType
    {
        Pref,
        Root,
        Suff
    }

    public static class EnumExtensions
    {
        public static string ToPrintableString(this EMorphemeType morphemeType)
        {
            switch (morphemeType)
            {
                case EMorphemeType.Pref:
                    return "\t\t\tприставка: ";
                case EMorphemeType.Root:
                    return "\t\t\tкорень: ";
                case EMorphemeType.Suff:
                    return "\t\t\tсуффикс или окончание: ";
                default:
                    return "";
            }
        }
    }
}
