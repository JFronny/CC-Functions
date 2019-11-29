using System.IO;
using System.Security.Principal;

namespace Misc
{
    public static class MiscFunctions
    {
        public static bool IsAdministrator => new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

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