using System;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace Misc
{
    public static class HID
    {
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
Win32_NetworkAdapterConfiguration:MACAddress:IPEnabled";

        public static byte[] Value
        {
            get
            {
                if (_fingerPrint == null)
                {
                    var fingerprint_tmp = "";
                    if (Type.GetType("Mono.Runtime") == null)
                    {
                        HIDClasses.Split('\r').Select(s =>
                        {
                            if (s.StartsWith("\n"))
                                s = s.Remove(0, 1);
                            return s.Split(':');
                        }).ToList().ForEach(s =>
                        {
                            using (var mc = new ManagementClass(s[0]))
                            using (var moc = mc.GetInstances())
                            {
                                var array = moc.OfType<ManagementBaseObject>().ToArray();
                                for (var j = 0; j < array.Length; j++)
                                {
                                    if (s.Length > 2 && array[j][s[2]].ToString() != "True") continue;
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
                    }
                    else //Linux implementation. This will not work if you are using Mono on windows or do not have uname and lscpu available
                    {
                        var p = new Process
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
                        fingerprint_tmp = p.StandardOutput.ReadToEnd();
                        p.WaitForExit();
                        p.StartInfo.FileName = "lscpu";
                        p.StartInfo.Arguments = "";
                        p.Start();
                        fingerprint_tmp += p.StandardOutput.ReadToEnd();
                        p.WaitForExit();
                    }

                    using (MD5 sec = new MD5CryptoServiceProvider())
                    {
                        var bt = Encoding.ASCII.GetBytes(fingerprint_tmp);
                        _fingerPrint = sec.ComputeHash(bt);
                    }
                }

                return _fingerPrint;
            }
        }

        public static byte[] EncryptLocal(byte[] unencrypted)
        {
            return ProtectedData.Protect(unencrypted, Value, DataProtectionScope.CurrentUser);
        }

        public static byte[] DecryptLocal(byte[] encrypted)
        {
            return ProtectedData.Unprotect(encrypted, Value, DataProtectionScope.CurrentUser);
        }
    }
}