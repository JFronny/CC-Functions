﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace CC_Functions.W32.DCDrawer
{
    public class DCBuffered : IDCDrawer
    {
        private readonly BufferedGraphics buffer;
        private readonly DCUnbuffered drawer;

        public DCBuffered(IntPtr ptr) : this(ptr, IntPtr.Zero)
        {
        }

        public DCBuffered(IntPtr ptr, IntPtr hWnd)
        {
            drawer = new DCUnbuffered(ptr, hWnd);
            buffer = BufferedGraphicsManager.Current.Allocate(drawer.Graphics, Screen.PrimaryScreen.Bounds);
            Graphics = buffer.Graphics;
        }

        public Graphics Graphics { get; }

        public void Dispose()
        {
            buffer.Render(drawer.Graphics);
            Graphics.Dispose();
            buffer.Dispose();
            drawer.Dispose();
        }
    }
}