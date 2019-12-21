using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace CC_Functions.W32
{
    public sealed class Wnd32 : IEquatable<Wnd32>
    {
        #region Exposed

        #region CreateInstance

        private Wnd32(IntPtr wndref)
        {
            hWnd = wndref;
        }

        public static Wnd32 fromHandle(IntPtr handle)
        {
            return new Wnd32(handle);
        }

        public static Wnd32 fromMetadata(string lpClassName = null, string lpWindowName = null)
        {
            return fromHandle(FindWindow(lpClassName, lpWindowName));
        }

        public static Wnd32 fromPoint(Point point)
        {
            return fromHandle(WindowFromPoint(point.X, point.Y));
        }

        public static Wnd32 fromForm(Form form)
        {
            return fromHandle(form.Handle);
        }

        public static Wnd32 foreground()
        {
            return fromHandle(GetForegroundWindow());
        }

        public static Wnd32[] getVisible()
        {
            WindowHandles = new List<IntPtr>();
            if (!EnumDesktopWindows(IntPtr.Zero, FilterCallback, IntPtr.Zero))
                throw new Win32Exception("There was a native error. This should never happen!");
            return WindowHandles.Select(s => fromHandle(s)).ToArray();
        }

        #endregion CreateInstance

        #region InstanceActions

        public string title
        {
            get
            {
                var length = GetWindowTextLength(hWnd);
                var sb = new StringBuilder(length + 1);
                GetWindowText(hWnd, sb, sb.Capacity);
                return sb.ToString();
            }
            set => SetWindowText(hWnd, value);
        }

        public Rectangle position
        {
            get
            {
                var Rect = new RECT();
                GetWindowRect(hWnd, ref Rect);
                return new Rectangle(new Point(Rect.left, Rect.top),
                    new Size(Rect.right - Rect.left, Rect.bottom - Rect.top));
            }
            set
            {
                var Rect = new RECT();
                GetWindowRect(hWnd, ref Rect);
                MoveWindow(hWnd, value.X, value.Y, value.Width, value.Height, true);
            }
        }

        public bool isForeground
        {
            get => GetForegroundWindow() == hWnd;
            set
            {
                if (value)
                    SetForegroundWindow(hWnd);
                else
                    throw new InvalidOperationException(
                        "You can't set a Window not to be in the foreground. Move another one over it!");
            }
        }

        public bool enabled
        {
            get => IsWindowEnabled(hWnd);
            set => EnableWindow(hWnd, value);
        }

        public Icon icon
        {
            get
            {
                var hicon = SendMessage(hWnd, 0x7F, 1, 0);
                if (hicon == IntPtr.Zero)
                    hicon = SendMessage(hWnd, 0x7F, 0, 0);
                if (hicon == IntPtr.Zero)
                    hicon = SendMessage(hWnd, 0x7F, 2, 0);
                return Icon.FromHandle(hicon);
            }
        }

        public bool shown
        {
            get => IsWindowVisible(hWnd);
            set
            {
                if (value)
                    ShowWindow(hWnd, 9);
                else
                    ShowWindow(hWnd, 0);
            }
        }

        public string className
        {
            get
            {
                var ClassName = new StringBuilder(256);
                _ = GetClassName(hWnd, ClassName, ClassName.Capacity);
                return ClassName.ToString();
            }
        }

        public FormWindowState state
        {
            get
            {
                var style = GetWindowLong(hWnd, -16);
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
                        ShowWindow(hWnd, 11);
                        break;

                    case FormWindowState.Normal:
                        ShowWindow(hWnd, 1);
                        break;

                    case FormWindowState.Maximized:
                        ShowWindow(hWnd, 3);
                        break;
                }
            }
        }

        public void MakeOverlay()
        {
            overlay = true;
        }

        public bool overlay
        {
            set
            {
                var tmp = position;
                _ = SetWindowPos(hWnd, value ? HWND_TOPMOST : HWND_NOTOPMOST, tmp.X, tmp.Y, tmp.Width, tmp.Height,
                    value ? SWP_NOMOVE | SWP_NOSIZE : 0);
            }
        }

        public bool Destroy()
        {
            if (DestroyWindow(hWnd))
                return true;
            throw new Exception("Failed.");
        }

        public bool stillExists => IsWindow(hWnd);

        public override string ToString()
        {
            return hWnd + "; " + title + "; " + position;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Wnd32);
        }

        public bool Equals(Wnd32 other)
        {
            return other != null && EqualityComparer<IntPtr>.Default.Equals(hWnd, other.hWnd);
        }

        public override int GetHashCode()
        {
            return -75345830 + EqualityComparer<IntPtr>.Default.GetHashCode(hWnd);
        }

        public static bool operator ==(Wnd32 left, Wnd32 right)
        {
            return EqualityComparer<Wnd32>.Default.Equals(left, right);
        }

        public static bool operator !=(Wnd32 left, Wnd32 right)
        {
            return !(left == right);
        }

        #endregion InstanceActions

        #endregion Exposed

        #region W32

        public IntPtr hWnd;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        private static extern long GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern IntPtr WindowFromPoint(int xPoint, int yPoint);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool SetWindowText(IntPtr hWnd, string lpString);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool EnableWindow(IntPtr hWnd, bool bEnable);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWindowEnabled(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool DestroyWindow(IntPtr hwnd);

        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private static readonly IntPtr HWND_TOP = new IntPtr(0);
        private static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        private const uint SWP_NOSIZE = 0x0001;
        private const uint SWP_NOMOVE = 0x0002;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy,
            uint uFlags);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall,
            ExactSpelling = true, SetLastError = true)]
        private static extern bool GetWindowRect(IntPtr hWnd, ref RECT rect);

        private struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWindow(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "EnumDesktopWindows", ExactSpelling = false, CharSet = CharSet.Auto,
            SetLastError = true)]
        private static extern bool EnumDesktopWindows(IntPtr hDesktop, EnumDelegate lpEnumCallbackFunction,
            IntPtr lParam);

        // Define the callback delegate's type.
        private delegate bool EnumDelegate(IntPtr hWnd, int lParam);

        private static List<IntPtr> WindowHandles;

        private static bool FilterCallback(IntPtr hWnd, int lParam)
        {
            var sb_title = new StringBuilder(1024);
            GetWindowText(hWnd, sb_title, sb_title.Capacity);
            if (IsWindowVisible(hWnd) && string.IsNullOrEmpty(sb_title.ToString()) == false) WindowHandles.Add(hWnd);
            return true;
        }

        #endregion W32
    }
}