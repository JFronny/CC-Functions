using System;
using System.Drawing;
using CC_Functions.Misc;

namespace CC_Functions.Commandline.TUI
{
    /// <summary>
    /// Provides a control for users to select a boolean
    /// </summary>
    public class CheckBox : Control
    {
        /// <summary>
        /// The text inside this checkbox
        /// </summary>
        public string Content;
        /// <summary>
        /// Whether the box is checked
        /// </summary>
        public bool Checked = false;
        /// <summary>
        /// Creates a new checkbox
        /// </summary>
        /// <param name="content">The text inside this CheckBox</param>
        public CheckBox(string content)
        {
            Content = content;
            Input += (screen, args) =>
            {
                switch (args.Info.Key)
                {
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.Spacebar:
                        Checked = !Checked;
                        CheckedChanged?.Invoke(screen, args);
                        break;
                }
            };
            Click += (screen, args) =>
            {
                Checked = !Checked;
                CheckedChanged?.Invoke(screen, args);
            };
        }

        /// <inheritdoc />
        public override Pixel[,] Render()
        {
            char[,] inp1 = Content.ToNdArray2D();
            char[,] inp = new char[inp1.GetLength(0), inp1.GetLength(1) + 4];
            inp.Populate(' ');
            inp1.CopyTo(inp, new Point(4, 0));
            inp[0, 0] = '[';
            inp[0, 1] = Checked ? 'X' : SpecialChars.Empty;
            inp[0, 2] = ']';
            int w = inp.GetLength(0);
            int h = inp.GetLength(1);
            Pixel[,] output = new Pixel[w, h];
            for (int x = 0; x < w; x++)
            for (int y = 0; y < h; y++)
                output[x, y] = new Pixel(Selected ? ForeColor : BackColor, Selected ? BackColor : ForeColor, inp[x, y]);
            Size = new Size(w, h);
            return output;
        }

        /// <inheritdoc />
        public override bool Selectable { get; } = true;

        /// <summary>
        /// Called when the state of this checkbox is changed
        /// </summary>
        /// <param name="screen">The current screen instance</param>
        /// <param name="e">Args</param>
        public delegate void OnCheckedChanged(Screen screen, EventArgs e);
        /// <summary>
        /// Called when the state of this checkbox is changed
        /// </summary>
        public event OnClick CheckedChanged;
    }
}