using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
