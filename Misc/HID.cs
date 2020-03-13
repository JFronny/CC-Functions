using System;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace CC_Functions.Misc
{
    public static class HID
    {
        public static bool forceWindows = false;
        private static byte[] _fingerPrint;

        private static readonly string HIDClasses = @"Win32_Processor:UniqueId
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
Win32_NetworkAdapterConfiguration:MACAddress";

        public static byte[] Value
        {
            get
            {
                if (_fingerPrint != null) return _fingerPrint;
                string fingerprintTmp = "";
                if (forceWindows || Type.GetType("Mono.Runtime") == null)
                {
                    HIDClasses.Split(new[] {"\r\n"}, StringSplitOptions.None).Select(s =>
                    {
                        if (s.StartsWith("\n"))
                            s = s.Remove(0, 1);
                        return s.Split(':');
                    }).ToList().ForEach(s =>
                    {
                        using ManagementClass mc = new ManagementClass(s[0]);
                        using ManagementObjectCollection moc = mc.GetInstances();
                        ManagementBaseObject[] array = moc.OfType<ManagementBaseObject>().ToArray();
                        for (int j = 0; j < array.Length; j++)
                            try
                            {
                                fingerprintTmp += array[j][s[1]].ToString();
                                break;
                            }
                            catch
                            {
                                Console.WriteLine("Failed to read property");
                            }
                    });
                }
                else //Linux implementation. This will not work if you are using Mono on windows or do not have "uname", "lscpu" and "id" available
                {
                    Process p = new Process
                    {
                        StartInfo =
                        {
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            FileName = "uname",
                            Arguments = "-nmpio"
                        }
                    };
                    p.Start();
                    fingerprintTmp = p.StandardOutput.ReadToEnd();
                    p.WaitForExit();
                    p.StartInfo.FileName = "lscpu";
                    p.StartInfo.Arguments = "-ap";
                    p.Start();
                    fingerprintTmp += p.StandardOutput.ReadToEnd();
                    p.WaitForExit();
                    p.StartInfo.FileName = "ip";
                    p.StartInfo.Arguments = "link";
                    p.Start();
                    fingerprintTmp += p.StandardOutput.ReadToEnd();
                    p.WaitForExit();
                }
                using MD5 sec = new MD5CryptoServiceProvider();
                byte[] bt = Encoding.ASCII.GetBytes(fingerprintTmp);
                _fingerPrint = sec.ComputeHash(bt);

                return _fingerPrint;
            }
        }

        public static byte[] EncryptLocal(byte[] unencrypted) =>
            ProtectedData.Protect(unencrypted, Value, DataProtectionScope.CurrentUser);

        public static byte[] DecryptLocal(byte[] encrypted) =>
            ProtectedData.Unprotect(encrypted, Value, DataProtectionScope.CurrentUser);
    }
}