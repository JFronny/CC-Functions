using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;

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
                parsed = (T) o;
                return true;
            }
            catch
            {
                parsed = default;
                return false;
            }
        }

        public static G selectO<T, G>(this T self, Func<T, G> func) => func.Invoke(self);

        public static void runIf(bool condition, Action func)
        {
            if (condition)
                func();
        }

        public static T ParseToEnum<T>(string value) => (T) Enum.Parse(typeof(T),
            Enum.GetNames(typeof(T)).First(s => s.ToLower() == value.ToLower()));

        public static bool? ParseBool(string value) =>
            string.IsNullOrWhiteSpace(value) || value.ToLower() == "Indeterminate"
                ? (bool?) null
                : bool.Parse(value);

        public static bool AND(this bool? left, bool? right) => left.TRUE() && right.TRUE();

        public static bool OR(this bool? left, bool? right) => left.TRUE() || right.TRUE();

        public static bool XOR(this bool? left, bool? right) => left.OR(right) && !left.AND(right);

        public static bool TRUE(this bool? self) => self == true;

        public static bool FALSE(this bool? self) => self == false;

        public static bool NULL(this bool? self) => self == null;

        public static void RemoveAt<T, G>(this Dictionary<T, G> dict, int index) =>
            dict.Remove(dict.Keys.OfType<T>().ToArray()[index]);

        public static long GetSize(this DirectoryInfo directory) => IO.GetDirectorySize(directory.FullName);

        public static ZipArchiveEntry AddDirectory(this ZipArchive archive, string folderPath, string entryName,
            string[] ignoredExtensions, string[] ignoredPaths)
        {
            entryName = entryName.TrimEnd('/');
            ZipArchiveEntry result = archive.CreateEntry($"{entryName}/");
            string[] files = Directory.GetFiles(folderPath);
            for (int i = 0; i < files.Length; i++)
                if (!ignoredExtensions.Contains(Path.GetExtension(files[i])) &&
                    !ignoredPaths.Any(s => IO.CheckPathEqual(s, files[i])))
                    archive.CreateEntryFromFile(files[i], $"{entryName}/{Path.GetFileName(files[i])}");
            string[] dirs = Directory.GetDirectories(folderPath);
            for (int i = 0; i < dirs.Length; i++)
                if (!ignoredPaths.Any(s => IO.CheckPathEqual(s, dirs[i])))
                    archive.AddDirectory(dirs[i], $"{entryName}/{Path.GetFileName(dirs[i])}", ignoredExtensions,
                        ignoredPaths);
            return result;
        }

        public static Uri Unshorten(this Uri self)
        {
            HttpWebRequest req = (HttpWebRequest) WebRequest.Create(self);
            req.AllowAutoRedirect = true;
            req.MaximumAutomaticRedirections = 100;
            WebResponse resp = req.GetResponse();
            return resp.ResponseUri;
        }

        public static bool Ping(this Uri self)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(self);
                request.Timeout = 3000;
                request.AllowAutoRedirect = true;
                using WebResponse response = request.GetResponse();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static Rectangle Round(this RectangleF self) => Rectangle.Round(self);
        public static Rectangle Ceiling(this RectangleF self) => Rectangle.Ceiling(self);
        public static byte[] Encrypt(this byte[] self, byte[] key) => Crypto.Encrypt(self, key);
        public static byte[] Decrypt(this byte[] self, byte[] key) => Crypto.Decrypt(self, key);
    }
}