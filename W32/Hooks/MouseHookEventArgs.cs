namespace CC_Functions.W32.Hooks
{
    public class MouseHookEventArgs : EventArgs
    {
        public MouseHookEventArgs(Point point, MouseHook.MouseMessages message)
        {
            Point = point;
            Message = message;
        }

        public Point Point { get; }
        public MouseHook.MouseMessages Message { get; }

        public override string ToString() => $"{Message}; {Point}";
    }
}