using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CC_Functions.Misc;

namespace CC_Functions.Commandline.TUI
{
    /// <summary>
    ///     A basic button type
    /// </summary>
    public class Button : Control
    {
        /// <summary>
        ///     The text inside this button
        /// </summary>
        public string Content;

        /// <summary>
        ///     Creates a new button
        /// </summary>
        /// <param name="content">The text inside this button</param>
        public Button(string content)
        {
            Content = content;
            char[,] tmp = Content.ToNdArray2D();
            Size = new Size(tmp.GetLength(1), tmp.GetLength(0));
        }

        /// <inheritdoc />
        public override bool Selectable { get; } = true;

        /// <inheritdoc />
        public override Pixel[,] Render()
        {
            char[,] inp = Indent(SplitLines(Content, Size.Width), Size.Width).ToNdArray2D();
            inp = inp.Resize(Size.Height, Size.Width);
            Pixel[,] output = new Pixel[Size.Height, Size.Width];
            for (int x = 0; x < Size.Height; x++)
            for (int y = 0; y < Size.Width; y++)
                output[x, y] = new Pixel(Selected ? ForeColor : BackColor, Selected ? BackColor : ForeColor, inp[x, y]);
            return output;
        }

        private string Indent(string source, int maxLen)
        {
            string[] tmp = source.Split('\n');
            for (int i = 0; i < tmp.Length; i++) tmp[i] = new string(SpecialChars.Empty, (maxLen - tmp[i].Length) / 2) + tmp[i];
            return string.Join('\n', tmp);
        }

        private string SplitLines(string source, int maxLen)
        {
            List<string> parts = new List<string>(source.Split());
            while (parts.Any(s => s.Length > maxLen))
            {
                parts = parts.SelectMany(s =>
                {
                    if (s.Length > maxLen)
                        return s.Insert(maxLen, "\n").Split('\n');
                    else
                        return new[] {s};
                }).ToList();
            }
            return string.Join('\n', parts);
        }
    }
}