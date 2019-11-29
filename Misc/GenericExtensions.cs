using System;
using System.Collections.Generic;
using System.Linq;

namespace CC_Functions.Misc
{
    public static class GenericExtensions
    {
        public static T get<G, T>(this Dictionary<G, T> dict, G key, T def)
        {
            if (!dict.ContainsKey(key))
                dict[key] = def;
            return dict[key];
        }

        public static T set<G, T>(this Dictionary<G, T> dict, G key, T val)
        {
            dict[key] = val;
            return dict[key];
        }

        public static bool tryCast<T>(this object o, out T parsed)
        {
            try
            {
                parsed = (T)o;
                return true;
            }
            catch
            {
                parsed = default(T);
                return false;
            }
        }

        public static G selectO<T, G>(this T self, Func<T, G> func) => func.Invoke(self);

        public static void runIf(bool condition, Action func)
        {
            if (condition)
                func();
        }

        public static T ParseToEnum<T>(string value) => (T)Enum.Parse(typeof(T), Enum.GetNames(typeof(T)).First(s => s.ToLower() == value.ToLower()));

        public static bool? ParseBool(string value) => (string.IsNullOrWhiteSpace(value) || value.ToLower() == "Indeterminate") ? (bool?)null : bool.Parse(value);

        public static bool AND(this bool? left, bool? right) => left.TRUE() && right.TRUE();

        public static bool OR(this bool? left, bool? right) => left.TRUE() || right.TRUE();

        public static bool XOR(this bool? left, bool? right) => left.OR(right) && (!left.AND(right));

        public static bool TRUE(this bool? self) => self == true;

        public static bool FALSE(this bool? self) => self == false;

        public static bool NULL(this bool? self) => self == null;
    }
}