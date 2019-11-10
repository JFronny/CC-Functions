using System;
using System.Drawing;

namespace CC_Functions.W32
{
    public class MouseHookEventArgs : EventArgs {
        public MouseHookEventArgs(Point point, MouseHook.MouseMessages message)
        {
            Point = point;
            Message = message;
        }

        public Point Point { get; }
        public MouseHook.MouseMessages Message { get; }
        public override string ToString()
        {
            return Message.ToString() + "; " + Point.ToString();
        }
    }
}
