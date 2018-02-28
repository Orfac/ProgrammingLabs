namespace Lab1
{
    public enum EErrorType
    {
       EmptyRoot,
       WrongMorphemeSplit
       
    }
    public static class ErrorHandler
    {
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
