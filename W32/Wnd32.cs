using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CC_Functions.W32.Native;

namespace CC_Functions.W32
{
    public sealed class Wnd32 : IEquatable<Wnd32>
    {
        #region Exposed

        #region CreateInstance

        private Wnd32(IntPtr wndref) => hWnd = wndref;

        public static Wnd32 fromHandle(IntPtr handle) => new Wnd32(handle);

        public static Wnd32 getProcessMain(Process process) => fromHandle(process.MainWindowHandle);

        public static Wnd32 fromMetadata(string? lpClassName = null, string? lpWindowName = null) =>
            fromHandle(user32.FindWindow(lpClassName, lpWindowName));

        public static Wnd32 fromPoint(Point point) => fromHandle(user32.WindowFromPoint(point.X, point.Y));

        public static Wnd32 fromForm(Form form) => fromHandle(form.Handle);

        public static Wnd32[] All
        {
            get { WindowHandles = new List<IntPtr>();
                if (!user32.EnumDesktopWindows(IntPtr.Zero, FilterCallback, IntPtr.Zero))
                    throw new Win32Exception("There was a native error. This should never happen!");
                return WindowHandles.Select(s => fromHandle(s)).ToArray(); }
        }

        public static Wnd32[] Visible => All.Where(s => user32.IsWindowVisible(s.hWnd) && !string.IsNullOrEmpty(s.title)).ToArray();
        public static Wnd32 Foreground => fromHandle(user32.GetForegroundWindow());
        public static Wnd32 ConsoleWindow => fromHandle(kernel32.GetConsoleWindow());

        #endregion CreateInstance

        #region InstanceActions

        public string title
        {
            get
            {
                int length = user32.GetWindowTextLength(hWnd);
                StringBuilder sb = new StringBuilder(length + 1);
                user32.GetWindowText(hWnd, sb, sb.Capacity);
                return sb.ToString();
            }
            set => user32.SetWindowText(hWnd, value);
        }

        public Rectangle position
        {
            get
            {
                RECT Rect = new RECT();
                user32.GetWindowRect(hWnd, ref Rect);
                return new Rectangle(new Point(Rect.Left, Rect.Top),
                    new Size(Rect.Width, Rect.Height));
            }
            set
            {
                RECT Rect = new RECT();
                user32.GetWindowRect(hWnd, ref Rect);
                user32.MoveWindow(hWnd, value.X, value.Y, value.Width, value.Height, true);
            }
        }

        public bool isForeground
        {
            get => user32.GetForegroundWindow() == hWnd;
            set
            {
                if (value)
                    user32.SetForegroundWindow(hWnd);
                else
                    throw new InvalidOperationException(
                        "You can't set a Window not to be in the foreground. Move another one over it!");
            }
        }

        public bool enabled
        {
            get => user32.IsWindowEnabled(hWnd);
            set => user32.EnableWindow(hWnd, value);
        }

        public Icon icon
        {
            get
            {
                IntPtr hicon = user32.SendMessage(hWnd, 0x7F, 1, 0);
                if (hicon == IntPtr.Zero)
                    hicon = user32.SendMessage(hWnd, 0x7F, 0, 0);
                if (hicon == IntPtr.Zero)
                    hicon = user32.SendMessage(hWnd, 0x7F, 2, 0);
                return Icon.FromHandle(hicon);
            }
        }

        public bool shown
        {
            get => user32.IsWindowVisible(hWnd);
            set => user32.ShowWindow(hWnd, value ? 9 : 0);
        }

        public string className
        {
            get
            {
                StringBuilder ClassName = new StringBuilder(256);
                user32.GetClassName(hWnd, ClassName, ClassName.Capacity);
                return ClassName.ToString();
            }
        }

        public FormWindowState state
        {
            get
            {
                int style = user32.GetWindowLong(hWnd, -16);
                if ((style & 0x01000000) == 0x01000000)
                    return FormWindowState.Maximized;
                if ((style & 0x20000000) == 0x20000000)
                    return FormWindowState.Minimized;
                return FormWindowState.Normal;
            }
            set
            {
                switch (value)
                {
                    case FormWindowState.Minimized:
                        user32.ShowWindow(hWnd, 11);
                        break;

                    case FormWindowState.Normal:
                        user32.ShowWindow(hWnd, 1);
                        break;

                    case FormWindowState.Maximized:
                        user32.ShowWindow(hWnd, 3);
                        break;
                }
            }
        }

        public bool overlay
        {
            set
            {
                Rectangle tmp = position;
                user32.SetWindowPos(hWnd, value ? HWND_TOPMOST : HWND_NOTOPMOST, tmp.X, tmp.Y, tmp.Width, tmp.Height,
                    value ? SWP_NOMOVE | SWP_NOSIZE : 0);
            }
        }

        public bool Destroy()
        {
            if (user32.DestroyWindow(hWnd))
                return true;
            throw new Exception("Failed.");
        }

        public bool stillExists => user32.IsWindow(hWnd);

        public override string ToString() => hWnd + "; " + title + "; " + position;

        public override bool Equals(object obj) => Equals(obj as Wnd32);

        public bool Equals(Wnd32 other) => other != null && EqualityComparer<IntPtr>.Default.Equals(hWnd, other.hWnd);

        public override int GetHashCode() => -75345830 + EqualityComparer<IntPtr>.Default.GetHashCode(hWnd);

        public static bool operator ==(Wnd32 left, Wnd32 right) => EqualityComparer<Wnd32>.Default.Equals(left, right);

        public static bool operator !=(Wnd32 left, Wnd32 right) => !(left == right);

        #endregion InstanceActions

        #endregion Exposed

        #region Internal

        public IntPtr hWnd;

        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        private const uint SWP_NOSIZE = 0x0001;
        private const uint SWP_NOMOVE = 0x0002;
        private static List<IntPtr> WindowHandles;
        private static bool FilterCallback(IntPtr hWnd, int lParam)
        {
            StringBuilder sbTitle = new StringBuilder(1024);
            user32.GetWindowText(hWnd, sbTitle, 1024);
            WindowHandles.Add(hWnd);
            return true;
        }
        #endregion Internal
    }
}