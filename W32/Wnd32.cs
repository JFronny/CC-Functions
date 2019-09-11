using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CC_Functions.W32
{
    public class Wnd32 : IEquatable<Wnd32>
    {
        #region Exposed
        #region CreateInstance
        Wnd32(IntPtr wndref) => hWnd = wndref;
        public static Wnd32 fromHandle(IntPtr handle) => new Wnd32(handle);
        public static Wnd32 fromMetadata(string lpClassName = null, string lpWindowName = null) => fromHandle(FindWindow(lpClassName, lpWindowName));
        public static Wnd32 fromPoint(Point point) => fromHandle(WindowFromPoint(point.X, point.Y));
        public static Wnd32 fromForm(Form form) => fromHandle(form.Handle);
        public static Wnd32 foreground() => fromHandle(GetForegroundWindow());
        #endregion
        #region InstanceActions
        public string title
        {
            get {
                int length = GetWindowTextLength(hWnd);
                StringBuilder sb = new StringBuilder(length + 1);
                GetWindowText(hWnd, sb, sb.Capacity);
                return sb.ToString();
            }
            set {
                SetWindowText(hWnd, value);
            }
        }
        public Rectangle position
        {
            get {
                RECT Rect = new RECT();
                GetWindowRect(hWnd, ref Rect);
                return new Rectangle(new Point(Rect.left, Rect.top), new Size(Rect.right - Rect.left, Rect.bottom - Rect.top));
            }
            set {
                RECT Rect = new RECT();
                GetWindowRect(hWnd, ref Rect);
                MoveWindow(hWnd, value.X, value.Y, value.Width, value.Height, true);
            }
        }
        public bool isForeground
        {
            get {
                return GetForegroundWindow() == hWnd;
            }
            set {
                if (value)
                    SetForegroundWindow(hWnd);
                else
                    throw new InvalidOperationException("You can't set a Window not to be in the foreground. Move another one over it!");
            }
        }
        public bool enabled
        {
            get {
                return IsWindowEnabled(hWnd);
            }
            set {
                EnableWindow(hWnd, value);
            }
        }
        public Icon icon
        {
            get {
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
            set {
                if (value)
                    ShowWindow(hWnd, 9);
                else
                    ShowWindow(hWnd, 0);
            }
        }
        public FormWindowState state
        {
            get {
                int style = (int)GetWindowLong(hWnd, -16);
                if ((style & 0x01000000) == 0x01000000)
                {
                    return FormWindowState.Maximized;
                }
                else if ((style & 0x20000000) == 0x20000000)
                {
                    return FormWindowState.Minimized;
                }
                else
                {
                    return FormWindowState.Normal;
                }
            }
            set {
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

        public void MakeOverlay() => _ = SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
        public bool Destroy()
        {
            if (DestroyWindow(hWnd))
                return true;
            else
                throw new Exception("Failed.");
        }

        public bool stillExists => IsWindow(hWnd);
        public override string ToString() => hWnd.ToString() + "; " + title + "; " + position.ToString();
        public override bool Equals(object obj) => Equals(obj as Wnd32);
        public bool Equals(Wnd32 other) => other != null && EqualityComparer<IntPtr>.Default.Equals(hWnd, other.hWnd);
        public override int GetHashCode() => -75345830 + EqualityComparer<IntPtr>.Default.GetHashCode(hWnd);
        public static bool operator ==(Wnd32 left, Wnd32 right) => EqualityComparer<Wnd32>.Default.Equals(left, right);
        public static bool operator !=(Wnd32 left, Wnd32 right) => !(left == right);
        #endregion
        #endregion
        #region W32
        public IntPtr hWnd;
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        static extern IntPtr WindowFromPoint(int xPoint, int yPoint);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool SetWindowText(IntPtr hWnd, string lpString);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool EnableWindow(IntPtr hWnd, bool bEnable);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool IsWindowEnabled(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool DestroyWindow(IntPtr hwnd);

        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        const UInt32 SWP_NOSIZE = 0x0001;
        const UInt32 SWP_NOMOVE = 0x0002;
        const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hWnd, ref RECT rect);

        struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool IsWindow(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        #endregion
    }
}
