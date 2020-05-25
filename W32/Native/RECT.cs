using System.Runtime.InteropServices;

namespace CC_Functions.W32.Native
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left, Top, Right, Bottom;
        public int Height => Bottom - Top;

        public int Width => Right - Left;
    }
}