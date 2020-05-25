using System.Drawing;
using System.Windows.Forms;
using CC_Functions.W32.Native;

namespace CC_Functions.W32
{
    /// <summary>
    ///     Functions for manipulating the mouse
    /// </summary>
    public static class Mouse
    {
        private const int MouseEventFLeftDown = 0x02;
        private const int MouseEventFLeftUp = 0x04;
        private const int MouseEventFRightDown = 0x08;
        private const int MouseEventFRightUp = 0x10;

        /// <summary>
        ///     Emulates a click at the cursors position
        /// </summary>
        /// <param name="right">Set to true to perform right-clicks instead of left-clicks</param>
        public static void Click(bool right = false) => Click(Cursor.Position, right);

        /// <summary>
        ///     Emulates a click at the specified position
        /// </summary>
        /// <param name="location">The position to perform the click at</param>
        /// <param name="right">Set to true to perform right-clicks instead of left-clicks</param>
        public static void Click(Point location, bool right = false) =>
            user32.mouse_event(
                (uint) (right
                    ? MouseEventFRightDown | MouseEventFRightUp
                    : MouseEventFLeftDown | MouseEventFLeftUp), (uint) location.X,
                (uint) location.Y, 0, 0);
    }
}