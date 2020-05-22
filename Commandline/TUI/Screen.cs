using System;
using System.Drawing;
using System.Linq;

namespace CC_Functions.Commandline.TUI
{
    public class Screen : Panel
    {
        public readonly bool Color;
        public int TabPoint = 0;
        /// <summary>
        /// Creates a screen object. Multiple can be instantiated but drawing one overrides others. Use panels for that
        /// </summary>
        /// <param name="width">The screens with</param>
        /// <param name="height">The screens height</param>
        /// <param name="color">Whether to output in color</param>
        public Screen(int width, int height, bool color = true)
        {
            Color = color;
            Resize(width, height);
            Tab();
        }

        /// <summary>
        /// Resizes the screen. Make sure that this does not exceed the console size
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void Resize(int width, int height)
        {
            Size = new Size(width, height);
            DiffDraw.Clear(width, height);
        }
        
        /// <summary>
        /// Renders the screen, draws it to the console and outputs the new state
        /// </summary>
        /// <returns>The new state of the screen</returns>
        public Pixel[,] Render()
        {
            Pixel[,] tmp = base.Render();
            DiffDraw.Clear(tmp);
            DiffDraw.Draw(Color);
            return tmp;
        }

        public void ReadInput()
        {
            while (Console.KeyAvailable)
            {
                Control[] controls = EnumerateRecursive();
                Control[] selectable = controls.Where(s => s.Selectable).ToArray();
                ConsoleKeyInfo input = Console.ReadKey();
                switch (input.Key)
                {
                    case ConsoleKey.Tab:
                        Tab(selectable, (input.Modifiers & ConsoleModifiers.Shift) != 0);
                        break;
                    case ConsoleKey.Enter:
                        if (selectable.Any() && selectable.Length >= TabPoint)
                        {
                            selectable[TabPoint].InvokeClick(this);
                            Render();
                        }
                        break;
                    case ConsoleKey.Escape:
                        Close?.Invoke(this, new EventArgs());
                        break;
                    default:
                        if (selectable.Any() && selectable.Length >= TabPoint)
                        {
                            selectable[TabPoint].InvokeInput(this, input);
                            Render();
                        }
                        break;
                }
            }
        }

        public void Tab(bool positive = true)
        {
            Control[] controls = EnumerateRecursive();
            Control[] selectable = controls.Where(s => s.Selectable).ToArray();
            Tab(selectable, positive);
        }

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
                Render();
            }
        }

        public delegate void OnClose(Screen screen, EventArgs e);

        public event OnClick Close;
    }
}