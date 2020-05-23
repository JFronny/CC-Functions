using System.Drawing;
using CC_Functions.Misc;

namespace CC_Functions.Commandline.TUI
{
    /// <summary>
    /// A basic button type
    /// </summary>
    public class Button : Control
    {
        /// <summary>
        /// The text inside this button
        /// </summary>
        public string Content;
        /// <summary>
        /// Creates a new button
        /// </summary>
        /// <param name="content">The text inside this button</param>
        public Button(string content)
        {
            Content = content;
            char[,] tmp = Content.ToNdArray2D();
            Size = new Size(tmp.GetLength(0), tmp.GetLength(1));
        }

        /// <inheritdoc />
        public override Pixel[,] Render()
        {
            char[,] inp = Content.ToNdArray2D();
            inp = inp.Resize(Size.Width, Size.Height);
            Pixel[,] output = new Pixel[Size.Width, Size.Height];
            for (int x = 0; x < Size.Width; x++)
            for (int y = 0; y < Size.Height; y++)
                output[x, y] = new Pixel(Selected ? ForeColor : BackColor, Selected ? BackColor : ForeColor, inp[x, y]);
            return output;
        }

        /// <inheritdoc />
        public override bool Selectable { get; } = true;
    }
}