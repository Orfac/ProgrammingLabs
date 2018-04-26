namespace DictionaryLib.Models
{
    /// <summary>
    /// Used for containg information about morpheme - it's type and value
    /// </summary>
    public sealed class Morpheme
    {
        /// <summary>
        /// Type of Morpheme
        /// </summary>
        public EMorphemeType MorphemeType { get; set; }
        /// <summary>
        /// Value of Morpheme
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Setting basic fields
        /// </summary>
        /// <param name="morphemeType"> morpheme type</param>
        /// <param name="value"> value </param>
        public Morpheme(EMorphemeType morphemeType, string value)
        {
            MorphemeType = morphemeType;
            Value = value;
        }

        public Morpheme()
        {
        }

        /// <summary>
        /// String representation
        /// </summary>
        /// <returns> string morpheme </returns>
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
