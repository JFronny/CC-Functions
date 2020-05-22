using System;

namespace CC_Functions.Commandline.TUI
{
    public class InputEventArgs : EventArgs
    {
        private readonly ConsoleKeyInfo _info;
        public ConsoleKeyInfo Info => _info;

        public InputEventArgs(ConsoleKeyInfo info) => _info = info;
    }
}