using System;
using System.Drawing;

namespace CC_Functions.Commandline.TUI
{
    /// <summary>
    ///     Abstract class inherited by all controls
    /// </summary>
    public abstract class Control
    {
        /// <summary>
        ///     Called when [enter] is pressed while the control is selected
        /// </summary>
        /// <param name="screen">An instance of the calling screen</param>
        /// <param name="e">Args</param>
        public delegate void OnClick(Screen screen, EventArgs e);

        /// <summary>
        ///     Called when the control is selected and unknown input is given
        /// </summary>
        /// <param name="screen">An instance of the calling screen</param>
        /// <param name="e">Args</param>
        public delegate void OnInput(Screen screen, InputEventArgs e);

        /// <summary>
        ///     Called when the controls Size property is changed
        /// </summary>
        /// <param name="caller">The calling control</param>
        /// <param name="e">Args</param>
        public delegate void OnResize(Control caller, EventArgs e);

        private Size _size;

        /// <summary>
        ///     Whether the control can be interacted with
        /// </summary>
        public bool Enabled = true;

        /// <summary>
        ///     Whether the control should be rendered
        /// </summary>
        public bool Visible = true;

        /// <summary>
        ///     The size of the control
        /// </summary>
        public Size Size
        {
            set
            {
                if (_size != value)
                {
                    _size = value;
                    Resize?.Invoke(this, new EventArgs());
                }
            }
            get => _size;
        }

        /// <summary>
        ///     The position of this control
        /// </summary>
        public Point Point { get; set; }

        /// <summary>
        ///     The foreground color for this control
        /// </summary>
        public ConsoleColor ForeColor { get; set; } = Console.ForegroundColor;

        /// <summary>
        ///     The background color for this control
        /// </summary>
        public ConsoleColor BackColor { get; set; } = Console.BackgroundColor;

        /// <summary>
        ///     Whether the control can be selected
        /// </summary>
        public abstract bool Selectable { get; }

        /// <summary>
        ///     Whether the object is selected. Used internally and for drawing
        /// </summary>
        public bool Selected { get; internal set; } = false;

        /// <summary>
        ///     Called when the controls Size property is changed
        /// </summary>
        public event OnResize Resize;

        /// <summary>
        ///     Renders the control
        /// </summary>
        /// <returns>The rendered pixels</returns>
        public abstract Pixel[,] Render();

        /// <summary>
        ///     Called when [enter] is pressed while the control is selected
        /// </summary>
        public event OnClick Click;

        /// <summary>
        ///     Called when the control is selected and unknown input is given
        /// </summary>
        public event OnInput Input;

        /// <summary>
        ///     Invokes click events
        /// </summary>
        /// <param name="screen">The calling screen</param>
        internal void InvokeClick(Screen screen)
        {
            Click?.Invoke(screen, new EventArgs());
        }

        /// <summary>
        ///     Invokes input events
        /// </summary>
        /// <param name="screen">The calling screen</param>
        /// <param name="info">The input data</param>
        internal void InvokeInput(Screen screen, ConsoleKeyInfo info)
        {
            Input?.Invoke(screen, new InputEventArgs(info));
        }
    }
}