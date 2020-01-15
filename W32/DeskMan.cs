using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using CC_Functions.W32.DCDrawer;
using CC_Functions.W32.Native;
using Microsoft.Win32;

namespace CC_Functions.W32
{
    public static class DeskMan
    {
        public static IDCDrawer CreateGraphics(bool buffered = false)
        {
            Wnd32 progman = Wnd32.fromMetadata("Progman");
            IntPtr result = IntPtr.Zero;
            user32.SendMessageTimeout(progman.hWnd, 0x052C, new IntPtr(0), IntPtr.Zero, 0x0, 1000, out result);
            IntPtr workerW = IntPtr.Zero;
            user32.EnumWindows((tophandle, topparamhandle) =>
            {
                IntPtr p = user32.FindWindowEx(tophandle, IntPtr.Zero, "SHELLDLL_DefView", IntPtr.Zero);
                if (p != IntPtr.Zero)
                {
                    workerW = user32.FindWindowEx(IntPtr.Zero, tophandle, "WorkerW", IntPtr.Zero);
                }
                return true;
            }, IntPtr.Zero);
            IntPtr dc = user32.GetDCEx(workerW, IntPtr.Zero, 0x403);
            if (dc == IntPtr.Zero)
            {
                throw new Exception("Something went wrong when creatiing the Graphics object");
            }
            return buffered ? (IDCDrawer)new DCBuffered(dc) : new DCUnbuffered(dc);
        }
        
        public static Image Wallpaper
        {
            get 
            {
                string currentWallpaper = new string('\0', 260);
                user32.SystemParametersInfo(0x73, currentWallpaper.Length, currentWallpaper, 0);
                return Image.FromFile(currentWallpaper.Substring(0, currentWallpaper.IndexOf('\0')));
            }
            set
            {
                string tempPath = Path.Combine(Path.GetTempPath(), "wallpaper.bmp");
                Wallpaper.Save(tempPath, ImageFormat.Bmp);
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
                key.SetValue(@"WallpaperStyle", 2.ToString());
                key.SetValue(@"TileWallpaper", 0.ToString());
                user32.SystemParametersInfo(20, 0, tempPath, 0x01 | 0x02);
                File.Delete(tempPath);
                Console.WriteLine("Saved background image to " + tempPath);
            }
        }
    }
}