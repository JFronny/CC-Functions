using System;
using System.Drawing;

namespace CC_Functions.Commandline.TUI
{
    public abstract class Control
    {
        private Point _point;
        private Size _size;
        public abstract Pixel[,] Render();
        protected abstract void Resize(int width, int height);
        private void Resize(Size size) => Resize(size.Width, size.Height);

        public Size Size
        {
            set
            {
                _size = value;
                Resize(value);
            }
            get => _size;
        }

        public Point Point
        {
            get => _point;
            set => _point = value;
        }

        public ConsoleColor ForeColor { get; set; } = Console.ForegroundColor;
        public ConsoleColor BackColor { get; set; } = Console.BackgroundColor;
        public abstract bool Selectable { get; }

        /// <summary>
        /// Called when [enter] is pressed while the control is selected
        /// </summary>
        /// <param name="screen">An instance of the calling screen</param>
        /// <param name="e">Args</param>
        public delegate void OnClick(Screen screen, EventArgs e);
        /// <summary>
        /// Called when [enter] is pressed while the control is selected
        /// </summary>
        public event OnClick Click;
        /// <summary>
        /// Called when the control is selected and unknown input is given
        /// </summary>
        /// <param name="screen">An instance of the calling screen</param>
        /// <param name="e">Args</param>
        public delegate void OnInput(Screen screen, InputEventArgs e);
        /// <summary>
        /// Called when the control is selected and unknown input is given
        /// </summary>
        public event OnInput Input;
        /// <summary>
        /// Whether the object is selected. Used internally and for drawing
        /// </summary>
        public bool Selected { get; internal set; } = false;
        /// <summary>
        /// Invokes click events
        /// </summary>
        /// <param name="screen">The calling screen</param>
        internal void InvokeClick(Screen screen)
        {
            Click?.Invoke(screen, new EventArgs());
        }
        /// <summary>
        /// Invokes input events
        /// </summary>
        /// <param name="screen">The calling screen</param>
        /// <param name="info">The input data</param>
        internal void InvokeInput(Screen screen, ConsoleKeyInfo info)
        {
            Input?.Invoke(screen, new InputEventArgs(info));
        }

        /// <summary>
        /// Whether the control should be rendered
        /// </summary>
        public bool Visible = true;
    }
}