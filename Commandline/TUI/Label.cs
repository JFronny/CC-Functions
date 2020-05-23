using System.Drawing;
using CC_Functions.Misc;

namespace CC_Functions.Commandline.TUI
{
    /// <summary>
    /// A basic text control
    /// </summary>
    public class Label : Control
    {
        /// <summary>
        /// The text inside this label
        /// </summary>
        public string Content;
        /// <summary>
        /// Creates a new label
        /// </summary>
        /// <param name="content">The text inside this label</param>
        public Label(string content) => Content = content;
        /// <inheritdoc />
        public override Pixel[,] Render()
        {
            char[,] inp = Content.ToNdArray2D();
            int w = inp.GetLength(0);
            int h = inp.GetLength(1);
            Pixel[,] output = new Pixel[w, h];
            for (int x = 0; x < w; x++)
            for (int y = 0; y < h; y++)
                output[x, y] = new Pixel(BackColor, ForeColor, inp[x, y]);
            Size = new Size(w, h);
            return output;
        }
        /// <inheritdoc />
        public override bool Selectable { get; } = false;
    }
}