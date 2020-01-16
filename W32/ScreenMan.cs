using System;
using System.Drawing;
using System.Windows.Forms;
using CC_Functions.W32.DCDrawer;
using CC_Functions.W32.Native;

namespace CC_Functions.W32
{
    public static class ScreenMan
    {
        private const int SRCCOPY = 13369376;
        public static Image CaptureScreen() => CaptureWindow(user32.GetDesktopWindow());

        public static Image CaptureWindow(IntPtr handle)
        {
            IntPtr hdcSrc = user32.GetWindowDC(handle);
            RECT windowRect = new RECT();
            user32.GetWindowRect(handle, ref windowRect);
            IntPtr hdcDest = gdi32.CreateCompatibleDC(hdcSrc);
            IntPtr hBitmap = gdi32.CreateCompatibleBitmap(hdcSrc, windowRect.Width, windowRect.Height);
            IntPtr hOld = gdi32.SelectObject(hdcDest, hBitmap);
            gdi32.BitBlt(hdcDest, 0, 0, windowRect.Width, windowRect.Height, hdcSrc, 0, 0, SRCCOPY);
            gdi32.SelectObject(hdcDest, hOld);
            gdi32.DeleteDC(hdcDest);
            user32.ReleaseDC(handle, hdcSrc);
            Image img = Image.FromHbitmap(hBitmap);
            gdi32.DeleteObject(hBitmap);
            return img;
        }

        public static void Draw(Image img)
        {
            using (IDCDrawer drawerBuffered = GetDrawer())
            {
                drawerBuffered.Graphics.DrawImage(img, GetBounds());
            }
        }

        public static IDCDrawer GetDrawer(bool buffer = true)
        {
            IntPtr ptr = user32.GetDC(IntPtr.Zero);
            return buffer ? (IDCDrawer) new DCBuffered(ptr) : new DCUnbuffered(ptr);
        }

        public static Rectangle GetBounds() => Screen.PrimaryScreen.Bounds;

        public static void Refresh() => shell32.SHChangeNotify(0x8000000, 0x1000, IntPtr.Zero, IntPtr.Zero);
    }
}