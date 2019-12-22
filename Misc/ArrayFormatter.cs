namespace CC_Functions.Misc
{
    public static class ArrayFormatter
    {
        public static string ElementsToString(this object[] input, string separator = "\r\n")
        {
            string a = "";
            for (int i = 0; i < input.Length; i++)
                a += input[i] + separator;
            if (separator.Length > 0)
                a = a.Remove(a.Length - separator.Length);
            return a;
        }
    }
}