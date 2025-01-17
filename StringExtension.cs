namespace MovieFactoryWebAPI
{
    public static class StringExtension
    {
        public static string UnifyTheString(this string value)
        {
            return value.Trim().ToUpper();
        }
    }
}
