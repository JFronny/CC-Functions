using System;
using System.Drawing;
using System.Linq;

namespace CC_Functions.Commandline.TUI
{
    /// <summary>
    ///     Provides a front-end renderer for panels, draws using DiffDraw
    /// </summary>
    public class Screen : Panel
    {
        /// <summary>
        ///     Called if Escape is pressed, use this for flow control
        /// </summary>
        /// <param name="screen">This instance of the screen class</param>
        /// <param name="e">Args</param>
        public delegate void OnClose(Screen screen, EventArgs e);

        /// <summary>
        ///     Called when the selected control is changed
        /// </summary>
        /// <param name="screen">This instance of the screen class</param>
        /// <param name="args">Args</param>
        public delegate void OnTabChanged(Screen screen, EventArgs args);

        /// <summary>
        ///     Called by ReadInput if a change in the window size is detected. Use this for positioning
        /// </summary>
        /// <param name="screen">This instance of the screen class</param>
        /// <param name="e">Args</param>
        public delegate void OnWindowResize(Screen screen, EventArgs e);

        private bool _color;

        /// <summary>
        ///     Whether to output in color. Recommended for most terminals, might cause slowdowns in others
        /// </summary>
        public bool Color
        {
            get => _color;
            set
            {
                if (_color != value)
                {
                    _color = value;
                    DiffDraw.Draw(_color);
                }
            }
        }

        private int _wndHeight = Console.WindowHeight;
        private int _wndWidth = Console.WindowWidth;

        /// <summary>
        ///     The current index of the tab-selected control in an array of selectable controls
        /// </summary>
        public int TabPoint;

        /// <summary>
        ///     Creates a screen object. Multiple can be instantiated but drawing one overrides others. Use panels for that
        /// </summary>
        /// <param name="width">The screens with</param>
        /// <param name="height">The screens height</param>
        /// <param name="color">Whether to output in color</param>
        public Screen(int width, int height, bool color = true)
        {
            Color = color;
            Border = false;
            Resize(width, height);
            Tab();
        }

        /// <summary>
        ///     Resizes the screen. Make sure that this does not exceed the console size
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public new void Resize(int width, int height)
        {
            Size = new Size(width, height);
            DiffDraw.Clear(width, height);
        }

        /// <summary>
        ///     Renders the screen, draws it to the console and outputs the new state
        /// </summary>
        /// <returns>The new state of the screen</returns>
        public new Pixel[,] Render()
        {
            Pixel[,] tmp = base.Render();
            DiffDraw.Clear(tmp);
            DiffDraw.Draw(Color);
            return tmp;
        }

        /// <summary>
        ///     Reads input from Console and calls according functions
        /// </summary>
        /// <param name="canRedraw">
        ///     Set to false to prevent redrawing if the screen should be updated. You can Render manually in
        ///     that case
        /// </param>
        public void ReadInput(bool canRedraw = true)
        {
            bool render = false;
            while (Console.KeyAvailable)
            {
                Control[] controls = EnumerateRecursive();
                Control[] selectable = controls.Where(s => s.Selectable).ToArray();
                ConsoleKeyInfo input = Console.ReadKey();
                switch (input.Key)
                {
                    case ConsoleKey.Tab:
                        Tab(selectable, (input.Modifiers & ConsoleModifiers.Shift) == 0);
                        break;
                    case ConsoleKey.Enter:
                        if (selectable.Any() && selectable.Length >= TabPoint && selectable[TabPoint].Enabled)
                        {
                            selectable[TabPoint].InvokeClick(this);
                            render = true;
                        }
                        break;
                    case ConsoleKey.Escape:
                        Close?.Invoke(this, new EventArgs());
                        break;
                }
                if (selectable.Any() && selectable.Length >= TabPoint && selectable[TabPoint].Enabled)
                    selectable[TabPoint].InvokeInput(this, input);
                InvokeInput(this, input);
                render = true;
            }
            if (_wndWidth != Console.WindowWidth || _wndHeight != Console.WindowHeight)
            {
                render = true;
                _wndWidth = Console.WindowWidth;
                _wndHeight = Console.WindowHeight;
                WindowResize?.Invoke(this, new EventArgs());
            }
            if (canRedraw && render)
                Render();
        }

        /// <summary>
        ///     Increases the TabPoint or reverts back to 0 if at the end of selectables
        /// </summary>
        /// <param name="positive">Set to false to decrease instead</param>
        public void Tab(bool positive = true)
        {
            Control[] controls = EnumerateRecursive();
            Control[] selectable = controls.Where(s => s.Selectable).ToArray();
            Tab(selectable, positive);
        }

        /// <summary>
        ///     Increases the TabPoint or reverts back to 0 if at the end of selectables
        /// </summary>
        /// <param name="selectable">The array of selectable controls to select from. You should most likely not use this</param>
        /// <param name="positive">Set to false to decrease instead</param>
        public void Tab(Control[] selectable, bool positive)
        {
            if (selectable.Any())
            {
                if (positive)
                {
                    TabPoint++;
                    if (TabPoint >= selectable.Length) TabPoint = 0;
                }
                else
                {
                    TabPoint--;
                    if (TabPoint < 0) TabPoint = selectable.Length - 1;
                }
                foreach (Control control in selectable) control.Selected = false;
                selectable[TabPoint].Selected = true;
                TabChanged?.Invoke(this, new EventArgs());
                Render();
            }
        }

        /// <summary>
        ///     Called if Escape is pressed, use this for flow control
        /// </summary>
        public event OnClose Close;

        /// <summary>
        ///     Called by ReadInput if a change in the window size is detected. Use this for positioning
        /// </summary>
        public event OnWindowResize WindowResize;

        /// <summary>
        ///     Called when the selected control is changed
        /// </summary>
        public event OnTabChanged TabChanged;
    }
}