using System.IO;

namespace CC_Functions.Misc
{
    public static class IO
    {
        public static long GetDirectorySize(string path)
        {
            try
            {
                string[] a = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
                long size = 0;
                for (int i = 0; i < a.Length; i++)
                {
                    size += new FileInfo(a[i]).Length;
                }
                return size;
            }
            catch { throw; }
        }
    }
}