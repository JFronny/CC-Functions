using System;
using System.Runtime.InteropServices;

namespace CC_Functions.W32.Native
{
    internal static class advapi32
    {
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool LookupPrivilegeValue(string lpsystemname, string lpname,
            [MarshalAs(UnmanagedType.Struct)] ref Privileges.LUID lpLuid);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool AdjustTokenPrivileges(IntPtr tokenhandle,
            [MarshalAs(UnmanagedType.Bool)] bool disableAllPrivileges,
            [MarshalAs(UnmanagedType.Struct)] ref Privileges.TOKEN_PRIVILEGES newstate, uint bufferlength,
            IntPtr previousState, IntPtr returnlength);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool OpenProcessToken(IntPtr processHandle, uint desiredAccesss,
            out IntPtr tokenHandle);
    }
}