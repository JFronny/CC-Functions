using System;
using System.Drawing;
using CC_Functions.W32.Native;

namespace CC_Functions.W32.DCDrawer
{
    public class DCUnbuffered : IDCDrawer
    {
        private readonly IntPtr hWnd;
        private readonly IntPtr ptr;

        public DCUnbuffered(IntPtr ptr, IntPtr hWnd)
        {
            this.ptr = ptr;
            this.hWnd = hWnd;
            Graphics = Graphics.FromHdc(ptr);
        }

        public DCUnbuffered(IntPtr ptr)
        {
            this.ptr = ptr;
            hWnd = IntPtr.Zero;
            Graphics = Graphics.FromHdc(ptr);
        }

        public Graphics Graphics { get; }

        public void Dispose()
        {
            Graphics.Dispose();
            user32.ReleaseDC(hWnd, ptr);
        }
    }
}