using CC_Functions.W32.Native;

namespace CC_Functions.W32
{
    public static class Time
    {
        public static void Set(DateTime time)
        {
            time = time.ToUniversalTime();
            kernel32.SYSTEMTIME st = new kernel32.SYSTEMTIME
            {
                wYear = (short) time.Year,
                wMonth = (short) time.Month,
                wDay = (short) time.Day,
                wHour = (short) time.Hour,
                wMinute = (short) time.Minute,
                wSecond = (short) time.Second
            };
            kernel32.SetSystemTime(ref st);
        }
    }
}