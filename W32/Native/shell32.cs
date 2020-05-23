namespace CC_Functions.W32.Native
{
    internal static class shell32
    {
        [DllImport("Shell32.dll")]
        public static extern int SHChangeNotify(int eventId, int flags, IntPtr item1, IntPtr item2);
    }
}