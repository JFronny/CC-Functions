using System;
using System.Windows.Forms;

namespace CC_Functions.W32
{
    public sealed class KeyboardHookEventArgs : EventArgs
    {
        public KeyboardHookEventArgs(Keys key)
        {
            Key = key;
        }

        public Keys Key { get; }

        public override string ToString() => Key.ToString();
    }
}