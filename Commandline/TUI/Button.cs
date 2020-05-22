using System.Drawing;
using CC_Functions.Misc;

namespace CC_Functions.Commandline.TUI
{
    public class Button : Control
    {
        public string Content;
        public Button(string content)
        {
            Content = content;
            char[,] tmp = Content.ToNDArray2D();
            Size = new Size(tmp.GetLength(0), tmp.GetLength(1));
        }

        public override Pixel[,] Render()
        {
            char[,] inp = Content.ToNDArray2D();
            inp = inp.Resize(Size.Width, Size.Height);
            Pixel[,] output = new Pixel[Size.Width, Size.Height];
            for (int x = 0; x < Size.Width; x++)
            for (int y = 0; y < Size.Height; y++)
                output[x, y] = new Pixel(Selected ? ForeColor : BackColor, Selected ? BackColor : ForeColor, inp[x, y]);
            return output;
        }

        protected override void Resize(int width, int height)
        {
            //ignored for [Render]s sake, do not use
        }

        public override bool Selectable { get; } = true;
    }
}