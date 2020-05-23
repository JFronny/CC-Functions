using System;
using System.IO;

namespace CC_Functions.Misc
{
    /// <summary>
    /// IO functions
    /// </summary>
    public static class IO
    {
        /// <summary>
        /// Recursively gets the size of an directory
        /// </summary>
        /// <param name="path">The path of the directory</param>
        /// <returns>The size of the directory</returns>
        public static long GetDirectorySize(string path)
        {
            string[] a = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            long size = 0;
            foreach (string t in a)
                size += new FileInfo(t).Length;
            return size;
        }
        /// <summary>
        /// Check whether the paths are equivalent (ignores case)
        /// </summary>
        /// <param name="path1">The first path to check</param>
        /// <param name="path2">The second path to check</param>
        /// <returns>Whether the paths are equal</returns>
        public static bool CheckPathEqual(string path1, string path2) =>
            Path.GetFullPath(path1)
                .Equals(Path.GetFullPath(path2), StringComparison.InvariantCultureIgnoreCase);
    }
}