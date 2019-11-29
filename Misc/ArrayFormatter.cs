namespace CC_Functions.Misc
{
    public static class ArrayFormatter
    {
        public static string ArrayToString(object[] Input, string Seperator = "\r\n")
        {
            try
            {
                string a = "";
                for (int i = 0; i < Input.Length; i++)
                    a += Input[i].ToString() + Seperator;
                if (Seperator.Length > 0)
                    a = a.Remove(a.Length - Seperator.Length);
                return a;
            }
            catch { throw; }
        }
    }
}