﻿using System;
using CC_Functions.W32.Native;
using static CC_Functions.W32.Privileges;

namespace CC_Functions.W32
{
    public static class Power
    {
        [Flags]
        public enum ExitWindows : uint
        {
            // ONE of the following five:
            LogOff = 0x00,

            ShutDown = 0x01,
            Reboot = 0x02,
            PowerOff = 0x08,
            RestartApps = 0x40,

            // plus AT MOST ONE of the following two:
            Force = 0x04,

            ForceIfHung = 0x10
        }

        [Flags]
        public enum ShutdownMod : uint
        {
            None = 0x00,
            Force = 0x04,
            ForceIfHung = 0x10
        }

        [Flags]
        public enum ShutdownMode : uint
        {
            LogOff = 0x00,
            ShutDown = 0x01,
            Reboot = 0x02,
            PowerOff = 0x08,
            RestartApps = 0x40,
            BSoD = 0x29a
        }

        [Flags]
        public enum ShutdownReason : uint
        {
            MajorApplication = 0x00040000,
            MajorHardware = 0x00010000,
            MajorLegacyApi = 0x00070000,
            MajorOperatingSystem = 0x00020000,
            MajorOther = 0x00000000,
            MajorPower = 0x00060000,
            MajorSoftware = 0x00030000,
            MajorSystem = 0x00050000,

            MinorBlueScreen = 0x0000000F,
            MinorCordUnplugged = 0x0000000b,
            MinorDisk = 0x00000007,
            MinorEnvironment = 0x0000000c,
            MinorHardwareDriver = 0x0000000d,
            MinorHotfix = 0x00000011,
            MinorHung = 0x00000005,
            MinorInstallation = 0x00000002,
            MinorMaintenance = 0x00000001,
            MinorMMC = 0x00000019,
            MinorNetworkConnectivity = 0x00000014,
            MinorNetworkCard = 0x00000009,
            MinorOther = 0x00000000,
            MinorOtherDriver = 0x0000000e,
            MinorPowerSupply = 0x0000000a,
            MinorProcessor = 0x00000008,
            MinorReconfig = 0x00000004,
            MinorSecurity = 0x00000013,
            MinorSecurityFix = 0x00000012,
            MinorSecurityFixUninstall = 0x00000018,
            MinorServicePack = 0x00000010,
            MinorServicePackUninstall = 0x00000016,
            MinorTermSrv = 0x00000020,
            MinorUnstable = 0x00000006,
            MinorUpgrade = 0x00000003,
            MinorWMI = 0x00000015,

            FlagUserDefined = 0x40000000,
            FlagPlanned = 0x80000000
        }

        public static void RaiseEvent(ShutdownMode mode, ShutdownReason reason = ShutdownReason.MinorOther,
            ShutdownMod mod = ShutdownMod.None)
        {
            if (mode == ShutdownMode.BSoD)
            {
                ntdll.RtlAdjustPrivilege(19, true, false, out bool _);
                ntdll.NtRaiseHardError(0xc0000022, 0, 0, IntPtr.Zero, 6, out uint _);
            }
            else
            {
                EnablePrivilege(SecurityEntity.SeShutdownPrivilege);
                user32.ExitWindowsEx((ExitWindows) ((uint) mode | (uint) mod), reason);
            }
        }
    }
}