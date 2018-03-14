namespace Lab2
{
    /// <summary>
    /// EmptyRoot error - trying to use word with null root
    /// WrongMorphemeSplit - missmatching between word and morphemes
    /// </summary>
    public enum EErrorType
    {
       EmptyRoot, 
       WrongMorphemeSplit 
    }

    /// <summary>
    /// Used to handle errors
    /// </summary>
    public static class ErrorHandler
    {
        /// <summary>
        /// Used to return string representation of error
        /// </summary>
        /// <param name="type"> type of error</param>
        /// <returns> string representation of error</returns>
        public static string ErrorToString(EErrorType type)
        {
            switch (type)
            {
                case EErrorType.EmptyRoot:
                    return "Корень не может быть пустым. Введите значение снова.";
                case EErrorType.WrongMorphemeSplit:
                    return "Слово не соответсвует морфемам. Введите морфемы снова.";
                default:
                    return null;
            }
        }
    }
}
