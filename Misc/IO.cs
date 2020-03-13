using System;
using System.IO;

namespace CC_Functions.Misc
{
    public static class IO
    {
        public static long GetDirectorySize(string path)
        {
            string[] a = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            long size = 0;
            for (int i = 0; i < a.Length; i++) size += new FileInfo(a[i]).Length;
            return size;
        }

        public static bool CheckPathEqual(string path1, string path2) =>
            Path.GetFullPath(path1)
                .Equals(Path.GetFullPath(path2), StringComparison.InvariantCultureIgnoreCase);
    }
}