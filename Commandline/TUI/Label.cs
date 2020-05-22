using System.Drawing;
using CC_Functions.Misc;

namespace CC_Functions.Commandline.TUI
{
    public class Label : Control
    {
        public string Content;
        public Label(string content) => Content = content;

        public override Pixel[,] Render()
        {
            char[,] inp = Content.ToNDArray2D();
            int w = inp.GetLength(0);
            int h = inp.GetLength(1);
            Pixel[,] output = new Pixel[w, h];
            for (int x = 0; x < w; x++)
            for (int y = 0; y < h; y++)
                output[x, y] = new Pixel(BackColor, ForeColor, inp[x, y]);
            Size = new Size(w, h);
            return output;
        }

        protected override void Resize(int width, int height)
        {
            //ignored for [Render]s sake, do not use
        }

        public override bool Selectable { get; } = false;
    }
}