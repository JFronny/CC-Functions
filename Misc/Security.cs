using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
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

    public static class HID
    {

        private static byte[] _fingerPrint;
        static readonly string HIDClasses = @"Win32_Processor:UniqueId
Win32_Processor:ProcessorId
Win32_Processor:Name
Win32_Processor:Manufacturer
Win32_BIOS:Manufacturer
Win32_BIOS:SMBIOSBIOSVersion
Win32_BIOS:IdentificationCode
Win32_BIOS:SerialNumber
Win32_BIOS:ReleaseDate
Win32_BIOS:Version
Win32_BaseBoard:Model
Win32_BaseBoard:Manufacturer
Win32_BaseBoard:Name
Win32_BaseBoard:SerialNumber
Win32_NetworkAdapterConfiguration:MACAddress:IPEnabled";
        public static byte[] Value
        {
            get {
                if (_fingerPrint == null)
                {
                    string fingerprint_tmp = "";
                    HIDClasses.Split('\r').Select(s =>
                    {
                        if (s.StartsWith("\n"))
                            s = s.Remove(0, 1);
                        return s.Split(':');
                    }).ToList().ForEach(s =>
                    {
                        using (ManagementClass mc = new ManagementClass(s[0]))
                        using (ManagementObjectCollection moc = mc.GetInstances())
                        {
                            ManagementBaseObject[] array = moc.OfType<ManagementBaseObject>().ToArray();
                            for (int j = 0; j < array.Length; j++)
                            {
                                if ((s.Length > 2) && array[j][s[2]].ToString() != "True") continue;
                                try
                                {
                                    fingerprint_tmp += array[j][s[1]].ToString();
                                    break;
                                }
                                catch
                                {
                                }
                            }
                        }
                    });
                    using (MD5 sec = new MD5CryptoServiceProvider())
                    {
                        byte[] bt = Encoding.ASCII.GetBytes(fingerprint_tmp);
                        _fingerPrint = sec.ComputeHash(bt);
                    }
                }
                return _fingerPrint;
            }
        }
        public static byte[] EncryptLocal(byte[] unencrypted) => ProtectedData.Protect(unencrypted, Value, DataProtectionScope.CurrentUser);
        public static byte[] DecryptLocal(byte[] encrypted) => ProtectedData.Unprotect(encrypted, Value, DataProtectionScope.CurrentUser);
    }
}
