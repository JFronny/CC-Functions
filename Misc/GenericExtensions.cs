using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;

namespace CC_Functions.Misc
{
    /// <summary>
    /// Extension methods for various types
    /// </summary>
    public static class GenericExtensions
    {
        /// <summary>
        /// Gets an element from the dictionary or adds the default
        /// </summary>
        /// <param name="dict">The dictionary to get from</param>
        /// <param name="key">The key to check</param>
        /// <param name="def">The default value to place</param>
        /// <typeparam name="TKey">The key type</typeparam>
        /// <typeparam name="TValue">The value type</typeparam>
        /// <returns>The element at the key</returns>
        public static TValue Get<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue def = default)
        {
            if (!dict.ContainsKey(key))
                dict[key] = def;
            return dict[key];
        }
        /// <summary>
        /// Sets an element and returns it
        /// </summary>
        /// <param name="dict">The dictionary to set in</param>
        /// <param name="key">The key to set at</param>
        /// <param name="val">The value to place</param>
        /// <typeparam name="TKey">The key type</typeparam>
        /// <typeparam name="TValue">The value type</typeparam>
        /// <returns>The value that was placed</returns>
        public static TValue Set<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue val = default)
        {
            dict[key] = val;
            return dict[key];
        }
        /// <summary>
        /// Tries to cast an object
        /// </summary>
        /// <param name="o">The object to try to parse</param>
        /// <param name="parsed">The parsed object (if successful) or the default (usually null)</param>
        /// <typeparam name="T">The type to cast to</typeparam>
        /// <returns>Whether the cast was successful</returns>
        public static bool TryCast<T>(this object o, out T parsed)
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
        /// <summary>
        /// Runs a function that transforms an object in-line
        /// </summary>
        /// <param name="self">The object to run on</param>
        /// <param name="func">The function to run</param>
        /// <typeparam name="TIn">The input type</typeparam>
        /// <typeparam name="TOut">The output type</typeparam>
        /// <returns></returns>
        public static TOut SelectO<TIn, TOut>(this TIn self, Func<TIn, TOut> func) => func.Invoke(self);
        /// <summary>
        /// Runs a function under a condition in-line (equal to if)
        /// </summary>
        /// <param name="condition">The condition to check</param>
        /// <param name="func">The function to run</param>
        public static void RunIf(bool condition, Action func)
        {
            if (condition)
                func();
        }
        /// <summary>
        /// Parses a string to a value of an enum
        /// </summary>
        /// <param name="value">The string to parse</param>
        /// <typeparam name="TEnum">The enum type (MUST be an enum)</typeparam>
        /// <returns>The element</returns>
        public static TEnum ParseToEnum<TEnum>(string value) => (TEnum) Enum.Parse(typeof(TEnum),
            Enum.GetNames(typeof(TEnum)).First(s => s.ToLower() == value.ToLower()));

        /// <summary>
        /// Parses a string to a nullable bool (defaults to null if parse fails)
        /// </summary>
        /// <param name="value">The st string to parse</param>
        /// <returns>The output nullable bool</returns>
        public static bool? ParseBool(string value) =>
            bool.TryParse(value, out bool tmp) ? (bool?)tmp : null;
        /// <summary>
        /// AND operation for nullable bools (uses <see cref="True">True</see>)
        /// </summary>
        /// <param name="left">First bool to check</param>
        /// <param name="right">Second bool to check</param>
        /// <returns>The operation result</returns>
        public static bool And(this bool? left, bool? right) => left.True() && right.True();
        /// <summary>
        /// OR operation for nullable bools (uses <see cref="True">True</see>)
        /// </summary>
        /// <param name="left">First bool to check</param>
        /// <param name="right">Second bool to check</param>
        /// <returns>The operation result</returns>
        public static bool Or(this bool? left, bool? right) => left.True() || right.True();
        /// <summary>
        /// XOR operation for nullable bools (uses <see cref="True">True</see>)
        /// </summary>
        /// <param name="left">First bool to check</param>
        /// <param name="right">Second bool to check</param>
        /// <returns>The operation result</returns>
        public static bool Xor(this bool? left, bool? right) => left.Or(right) && !left.And(right);
        /// <summary>
        /// Whether the nullable bool is true (null->false)
        /// </summary>
        /// <param name="self">Value to check</param>
        /// <returns>Whether it is true</returns>
        public static bool True(this bool? self) => self == true;
        /// <summary>
        /// Whether the nullable bool is false (null->false)
        /// </summary>
        /// <param name="self">Value to check</param>
        /// <returns>Whether it is false</returns>
        public static bool False(this bool? self) => self == false;
        /// <summary>
        /// Whether the nullable bool is null
        /// </summary>
        /// <param name="self">Value to check</param>
        /// <returns>Whether it is null</returns>
        public static bool Null(this bool? self) => self == null;
        /// <summary>
        /// Removes an element from a dictionary by its index (not key)
        /// </summary>
        /// <param name="dict">The dictionary to remove from</param>
        /// <param name="index">The index of the value</param>
        /// <typeparam name="TKey">The key type</typeparam>
        /// <typeparam name="TValue">The value type</typeparam>
        public static void RemoveAt<TKey, TValue>(this Dictionary<TKey, TValue> dict, int index) =>
            dict.Remove(dict.Keys.ToArray()[index]);
        /// <summary>
        /// Gets the size of a dictionary
        /// </summary>
        /// <param name="directory">The dictionary to check</param>
        /// <returns>The size of the dictionary</returns>
        public static long GetSize(this DirectoryInfo directory) => IO.GetDirectorySize(directory.FullName);
        /// <summary>
        /// Adds a directory to an archive recursively
        /// </summary>
        /// <param name="archive">The archive to add to</param>
        /// <param name="folderPath">The directory to add</param>
        /// <param name="entryName">The name of the directory in-archive</param>
        /// <param name="ignoredExtensions">Extensions for files to ignore</param>
        /// <param name="ignoredPaths">Paths to exclude from adding</param>
        /// <returns>The new entry</returns>
        public static ZipArchiveEntry AddDirectory(this ZipArchive archive, string folderPath, string entryName,
            string[] ignoredExtensions, string[] ignoredPaths)
        {
            entryName = entryName.TrimEnd('/');
            ZipArchiveEntry result = archive.CreateEntry($"{entryName}/");
            string[] files = Directory.GetFiles(folderPath);
            foreach (string t in files)
                if (!ignoredExtensions.Contains(Path.GetExtension(t)) &&
                    !ignoredPaths.Any(s => IO.CheckPathEqual(s, t)))
                    archive.CreateEntryFromFile(t, $"{entryName}/{Path.GetFileName(t)}");
            string[] dirs = Directory.GetDirectories(folderPath);
            foreach (string t in dirs)
                if (!ignoredPaths.Any(s => IO.CheckPathEqual(s, t)))
                    archive.AddDirectory(t, $"{entryName}/{Path.GetFileName(t)}", ignoredExtensions,
                        ignoredPaths);
            return result;
        }
        /// <summary>
        /// "Unshorten" (follow) an URL
        /// </summary>
        /// <param name="self">The URL to unshorten</param>
        /// <returns>The unshortened URL</returns>
        public static Uri Unshorten(this Uri self)
        {
            HttpWebRequest req = (HttpWebRequest) WebRequest.Create(self);
            req.AllowAutoRedirect = true;
            req.MaximumAutomaticRedirections = 100;
            WebResponse resp = req.GetResponse();
            return resp.ResponseUri;
        }
        /// <summary>
        /// Pings an URL to check for availability
        /// </summary>
        /// <param name="self">The URL to check</param>
        /// <returns>Whether the service is online</returns>
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
        /// <summary>
        /// Rounds a RectangleF to a Rectangle instead of flooring
        /// </summary>
        /// <param name="self">The RectangleF to round</param>
        /// <returns>The rounded Rectangle</returns>
        public static Rectangle Round(this RectangleF self) => Rectangle.Round(self);
        /// <summary>
        /// Ceilings a RectangleF to a Rectangle instead of flooring
        /// </summary>
        /// <param name="self">The RectangleF to ceil (?)</param>
        /// <returns>The ceiled (?) Rectangle</returns>
        public static Rectangle Ceiling(this RectangleF self) => Rectangle.Ceiling(self);
        /// <summary>
        /// Extension method for <see cref="Crypto">Crypto's</see> Encrypt
        /// </summary>
        /// <param name="self">The data to encrypt</param>
        /// <param name="key">The key to encrypt with</param>
        /// <returns>The encrypted data</returns>
        public static byte[] Encrypt(this byte[] self, byte[] key) => Crypto.Encrypt(self, key);
        /// <summary>
        /// Extension method for <see cref="Crypto">Crypto's</see> Decrypt
        /// </summary>
        /// <param name="self">The data to decrypt</param>
        /// <param name="key">The key to decrypt with</param>
        /// <returns>The decrypted data</returns>
        public static byte[] Decrypt(this byte[] self, byte[] key) => Crypto.Decrypt(self, key);
    }
}