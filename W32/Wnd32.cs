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
    /// <summary>
    ///     Object representing a window handle in the Windows API. Provides a simplified interface for basic interactions
    /// </summary>
    public sealed class Wnd32 : IEquatable<Wnd32>
    {
        #region Exposed

        #region CreateInstance

        private Wnd32(IntPtr handle) => HWnd = handle;

        /// <summary>
        ///     Base method. Generates a window object from the specified handle
        /// </summary>
        /// <param name="handle">The handle</param>
        /// <returns>The window</returns>
        public static Wnd32 FromHandle(IntPtr handle) => new Wnd32(handle);

        /// <summary>
        ///     Gets the main window of the process
        /// </summary>
        /// <param name="process">The process</param>
        /// <returns>The window. Might be IntPtr.Zero</returns>
        public static Wnd32 GetProcessMain(Process process) => FromHandle(process.MainWindowHandle);

        /// <summary>
        ///     Generates a window from metadata. Parameters should be null if they are not used
        /// </summary>
        /// <param name="lpClassName">
        ///     The class name of the window. Use the name you found before using the ClassName-parameter of
        ///     a window
        /// </param>
        /// <param name="lpWindowName">The windows name (title)</param>
        /// <returns>The window. Might be IntPtr.Zero</returns>
        public static Wnd32 FromMetadata(string? lpClassName = null, string? lpWindowName = null) =>
            FromHandle(user32.FindWindow(lpClassName, lpWindowName));

        /// <summary>
        ///     Gets the window that is visible at the specified point
        /// </summary>
        /// <param name="point">The point to scan</param>
        /// <returns>The window. Might be IntPtr.Zero</returns>
        public static Wnd32 FromPoint(Point point) => FromHandle(user32.WindowFromPoint(point.X, point.Y));

        /// <summary>
        ///     Gets all windows at the specific point
        /// </summary>
        /// <param name="point">The point to scan</param>
        /// <param name="visible">Whether windows need to be visible</param>
        /// <returns>The windows</returns>
        public static Wnd32[] AllFromPoint(Point point, bool visible = false) => All.Where(s => s.Position.Contains(point) && s.Shown || !visible).ToArray();

        /// <summary>
        ///     Gets the window associated with the forms handle
        /// </summary>
        /// <param name="form">Form to get window from</param>
        /// <returns>The window. Might be IntPtr.Zero</returns>
        public static Wnd32 FromForm(Form form) => FromHandle(form.Handle);

        /// <summary>
        ///     Gets ALL windows. In most cases you will want to use Wnd32.Visible
        /// </summary>
        /// <exception cref="Win32Exception"></exception>
        public static Wnd32[] All
        {
            get
            {
                _windowHandles = new List<IntPtr>();
                if (!user32.EnumDesktopWindows(IntPtr.Zero, FilterCallback, IntPtr.Zero))
                    throw new Win32Exception("There was a native error. This should never happen!");
                return _windowHandles.Select(FromHandle).ToArray();
            }
        }

        /// <summary>
        ///     Gets all visible windows
        /// </summary>
        public static Wnd32[] Visible =>
            All.Where(s => s.Shown).ToArray();

        /// <summary>
        ///     Gets the foreground window
        /// </summary>
        public static Wnd32 Foreground => FromHandle(user32.GetForegroundWindow());

        /// <summary>
        ///     The current programs console window. Do NOT use this if you are not targeting a console app or allocating a console
        /// </summary>
        public static Wnd32 ConsoleWindow => FromHandle(kernel32.GetConsoleWindow());

        #endregion CreateInstance

        #region InstanceActions

        /// <summary>
        ///     The windows title
        /// </summary>
        public string Title
        {
            get
            {
                int length = user32.GetWindowTextLength(HWnd);
                StringBuilder sb = new StringBuilder(length + 1);
                user32.GetWindowText(HWnd, sb, sb.Capacity);
                return sb.ToString();
            }
            set => user32.SetWindowTextW(HWnd, value);
        }

        /// <summary>
        ///     The windows position in screen-space
        /// </summary>
        public Rectangle Position
        {
            get
            {
                RECT rect = new RECT();
                user32.GetWindowRect(HWnd, ref rect);
                return new Rectangle(new Point(rect.Left, rect.Top),
                    new Size(rect.Width, rect.Height));
            }
            set
            {
                RECT rect = new RECT();
                user32.GetWindowRect(HWnd, ref rect);
                user32.MoveWindow(HWnd, value.X, value.Y, value.Width, value.Height, true);
            }
        }

        /// <summary>
        ///     Gets whether the window is the foreground window or brings it to the front. "False" must not be assigned!
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the value is "False"</exception>
        public bool IsForeground
        {
            get => user32.GetForegroundWindow() == HWnd;
            set
            {
                if (value)
                    user32.SetForegroundWindow(HWnd);
                else
                    throw new InvalidOperationException(
                        "You can't set a Window not to be in the foreground. Move another one over it!");
            }
        }

        /// <summary>
        ///     Whether the window is enabled. Functionally similar to WinForms' "Control.Enabled"
        /// </summary>
        public bool Enabled
        {
            get => user32.IsWindowEnabled(HWnd);
            set => user32.EnableWindow(HWnd, value);
        }

        /// <summary>
        ///     Gets the windows icon
        /// </summary>
        public Icon Icon
        {
            get
            {
                IntPtr hIcon = user32.SendMessage(HWnd, 0x7F, 1, 0);
                if (hIcon == IntPtr.Zero)
                    hIcon = user32.SendMessage(HWnd, 0x7F, 0, 0);
                if (hIcon == IntPtr.Zero)
                    hIcon = user32.SendMessage(HWnd, 0x7F, 2, 0);
                return Icon.FromHandle(hIcon);
            }
        }

        /// <summary>
        ///     Whether the window is visible
        /// </summary>
        public bool Shown
        {
            get => user32.IsWindowVisible(HWnd);
            set => user32.ShowWindow(HWnd, value ? 9 : 0);
        }

        /// <summary>
        ///     Gets the windows class name, This is basically only useful for finding window class-names of specified programs
        /// </summary>
        public string ClassName
        {
            get
            {
                StringBuilder className = new StringBuilder(256);
                user32.GetClassName(HWnd, className, className.Capacity);
                return className.ToString();
            }
        }

        /// <summary>
        ///     Sets the window state
        /// </summary>
        public FormWindowState State
        {
            get
            {
                int style = user32.GetWindowLong(HWnd, -16);
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
                        user32.ShowWindow(HWnd, 11);
                        break;

                    case FormWindowState.Normal:
                        user32.ShowWindow(HWnd, 1);
                        break;

                    case FormWindowState.Maximized:
                        user32.ShowWindow(HWnd, 3);
                        break;

                    default:
                        throw new ArgumentException("The provided WindowState was invalid", "value");
                }
            }
        }

        /// <summary>
        ///     Overlays the window over others
        /// </summary>
        public bool Overlay
        {
            set
            {
                Rectangle tmp = Position;
                user32.SetWindowPos(HWnd, value ? new IntPtr(-1) : new IntPtr(-2), tmp.X, tmp.Y, tmp.Width, tmp.Height,
                    value ? (uint) 3 : 0);
            }
        }

        /// <summary>
        ///     Forces the window to close
        /// </summary>
        /// <exception cref="Exception">Thrown if the window could not be closed</exception>
        public void Destroy()
        {
            if (!user32.DestroyWindow(HWnd))
                throw new Exception("Failed.");
        }

        /// <summary>
        ///     Whether the IntPtr is a window and still exists
        /// </summary>
        public bool StillExists => user32.IsWindow(HWnd);

        /// <summary>
        ///     Creates a user-readable string from the windows hWnd, title and position
        /// </summary>
        /// <returns>The created string</returns>
        public override string ToString() => $"{HWnd}; {Title}; {Position}";

        /// <summary>
        ///     Equality operator, uses the hWnd field
        /// </summary>
        /// <param name="obj">Object (Window) to compare</param>
        /// <returns>Equality result</returns>
        public override bool Equals(object obj) => Equals(obj as Wnd32);

        /// <summary>
        ///     Equality operator, uses the hWnd field
        /// </summary>
        /// <param name="other">Window to compare</param>
        /// <returns>Equality result</returns>
        public bool Equals(Wnd32 other) => !IsNull(other) && other != null && HWnd.Equals(other.HWnd);

        /// <summary>
        ///     Equality operator, uses the hWnd field
        /// </summary>
        /// <returns>Equality result</returns>
        public override int GetHashCode() => HWnd.GetHashCode();

        /// <summary>
        ///     Equality operator, uses the hWnd field
        /// </summary>
        /// <param name="left">Window to compare</param>
        /// <param name="right">Window to compare</param>
        /// <returns>Equality result</returns>
        public static bool operator ==(Wnd32 left, Wnd32 right) => !AreNull(left, right) && left.HWnd == right.HWnd;

        /// <summary>
        ///     Equality operator, uses the hWnd field
        /// </summary>
        /// <param name="left">Window to compare</param>
        /// <param name="right">Window to compare</param>
        /// <returns>Equality result</returns>
        public static bool operator !=(Wnd32 left, Wnd32 right) => AreNull(left, right) || left.HWnd != right.HWnd;

        private static bool AreNull(params Wnd32[] windows) => windows.Any(IsNull);

        private static bool IsNull(Wnd32 window)
        {
            try
            {
                window.ToString();
                return false;
            }
            catch (NullReferenceException)
            {
                return true;
            }
        }

        #endregion InstanceActions

        #endregion Exposed

        #region Internal

        /// <summary>
        ///     The windows' handle
        /// </summary>
        public readonly IntPtr HWnd;

        private static List<IntPtr> _windowHandles;

        private static bool FilterCallback(IntPtr hWnd, int lParam)
        {
            StringBuilder sbTitle = new StringBuilder(1024);
            user32.GetWindowText(hWnd, sbTitle, 1024);
            _windowHandles.Add(hWnd);
            return true;
        }

        #endregion Internal
    }
}