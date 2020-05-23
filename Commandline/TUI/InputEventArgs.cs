using System;

namespace CC_Functions.Commandline.TUI
{
    /// <summary>
    /// Arguments containing input data
    /// </summary>
    public class InputEventArgs : EventArgs
    {
        private readonly ConsoleKeyInfo _info;
        /// <summary>
        /// The inputs data
        /// </summary>
        public ConsoleKeyInfo Info => _info;
        /// <summary>
        /// Generates new arguments
        /// </summary>
        /// <param name="info">The input data</param>
        public InputEventArgs(ConsoleKeyInfo info) => _info = info;
    }
}