namespace Lab1.Models
{
    // Used for containg information about morpheme - it's type and value
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
    }
}
