using System;
using System.Drawing;
using CC_Functions.Misc;

namespace CC_Functions.Commandline.TUI
{
    public class CheckBox : Control
    {
        public string Content;
        public bool Checked = false;
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

        public override Pixel[,] Render()
        {
            char[,] inp1 = Content.ToNDArray2D();
            char[,] inp = new char[inp1.GetLength(0), inp1.GetLength(1) + 4];
            inp.Populate(' ');
            inp1.CopyTo(inp, new Point(4, 0));
            inp[0, 0] = '[';
            inp[0, 1] = Checked ? 'X' : SpecialChars.empty;
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

        protected override void Resize(int width, int height)
        {
            //ignored for [Render]s sake, do not use
        }

        public override bool Selectable { get; } = true;
        public delegate void OnCheckedChanged(Screen screen, EventArgs e);
        public event OnClick CheckedChanged;
    }
}