using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace CC_Functions.W32
{
    public static class GenericExtensions
    {
        public static Wnd32 GetWindow(this IntPtr handle) => Wnd32.fromHandle(handle);
        public static Wnd32 GetMainWindow(this Process handle) => Wnd32.getProcessMain(handle);
        public static Wnd32 GetWnd32(this Form frm) => Wnd32.fromForm(frm);
        public static bool IsDown(this Keys key) => KeyboardReader.IsKeyDown(key);
    }
}