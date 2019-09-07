using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                return a.Remove(a.Length - 1 - Seperator.Length);
            }
            catch { throw; }
        }
    }
}
