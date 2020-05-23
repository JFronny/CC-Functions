using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CC_Functions.Misc;

namespace CC_Functions.Commandline.TUI
{
    /// <summary>
    ///     A basic non-scrolling text-editor control
    /// </summary>
    public class TextBox : Control
    {
        /// <summary>
        ///     The text inside this textbox
        /// </summary>
        public string Content;

        /// <summary>
        ///     The "Cursors" position in this text box
        /// </summary>
        public Point Cursor = new Point(0, 0);

        /// <summary>
        ///     Creates a new text box
        /// </summary>
        /// <param name="content">The text inside this text box</param>
        public TextBox(string content)
        {
            Content = content;
            Input += (screen, args) => { ProcessInput(args.Info.Key, args.Info); };
            Click += (screen, args) => ProcessInput(ConsoleKey.Enter, new ConsoleKeyInfo());
        }

        private string[] Lines
        {
            get => Content.Split('\n');
            set => Content = string.Join('\n', value);
        }

        /// <inheritdoc />
        public override bool Selectable { get; } = true;

        /// <summary>
        ///     Function to process input
        /// </summary>
        /// <param name="key">The pressed key</param>
        /// <param name="info">Input metadata</param>
        private void ProcessInput(ConsoleKey key, ConsoleKeyInfo info)
        {
            string[] lines = Lines;
            List<string> tmp;
            int tmplen;
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    Cursor.X--;
                    if (Cursor.X < 0)
                    {
                        if (Cursor.Y > 0) Cursor.Y--;
                        ProcessInput(ConsoleKey.End, info);
                    }
                    break;
                case ConsoleKey.RightArrow:
                    Cursor.X++;
                    if (Cursor.X >= Lines[Cursor.Y].Length)
                    {
                        Cursor.Y++;
                        Cursor.X = 0;
                        if (Cursor.Y >= Lines.Length)
                        {
                            Cursor.Y--;
                            ProcessInput(ConsoleKey.End, info);
                        }
                    }
                    break;
                case ConsoleKey.UpArrow:
                    if (Cursor.Y > 0)
                    {
                        Cursor.Y--;
                        if (Cursor.X >= Lines[Cursor.Y].Length) ProcessInput(ConsoleKey.End, info);
                    }
                    else
                        Cursor.X = 0;
                    break;
                case ConsoleKey.DownArrow:
                    if (Cursor.Y < Lines.Length - 1)
                    {
                        Cursor.Y++;
                        if (Cursor.X >= Lines[Cursor.Y].Length) ProcessInput(ConsoleKey.End, info);
                    }
                    else
                        ProcessInput(ConsoleKey.End, info);
                    break;
                case ConsoleKey.Home:
                    Cursor.X = 0;
                    break;
                case ConsoleKey.End:
                    Cursor.X = Math.Max(Lines[Cursor.Y].Length - 1, 0);
                    break;
                case ConsoleKey.PageUp:
                    for (int i = 0; i < 5; i++)
                        ProcessInput(ConsoleKey.UpArrow, info);
                    break;
                case ConsoleKey.PageDown:
                    for (int i = 0; i < 5; i++)
                        ProcessInput(ConsoleKey.DownArrow, info);
                    break;
                case ConsoleKey.Delete:
                    if (lines[Cursor.Y].Length > Cursor.X)
                        lines[Cursor.Y] = lines[Cursor.Y].Remove(Cursor.X, 1);
                    Lines = lines;
                    break;
                case ConsoleKey.Backspace:
                    if (Cursor.X > 0 && lines[Cursor.Y].Length > 0)
                    {
                        lines[Cursor.Y] = lines[Cursor.Y].Remove(Cursor.X - 1, 1);
                        ProcessInput(ConsoleKey.LeftArrow, info);
                    }
                    else
                    {
                        if (Cursor.Y > 0)
                        {
                            tmp = lines.ToList();
                            tmplen = tmp[Cursor.Y - 1].Length;
                            tmp[Cursor.Y - 1] += tmp[Cursor.Y];
                            tmp.RemoveAt(Cursor.Y);
                            lines = tmp.ToArray();
                            Cursor.Y--;
                            Cursor.X = tmplen;
                        }
                    }
                    Lines = lines;
                    break;
                case ConsoleKey.Enter:
                    if (lines.Length < Size.Height)
                    {
                        tmp = lines.ToList();
                        lines[Cursor.Y] = lines[Cursor.Y].Insert(Math.Max(Cursor.X, 0), "\n");
                        Cursor.Y++;
                        Cursor.X = 0;
                        Lines = lines;
                    }
                    break;
                default:
                    if (lines[Cursor.Y].Length < Size.Width)
                    {
                        lines[Cursor.Y] = lines[Cursor.Y].Insert(Cursor.X, info.KeyChar.ToString());
                        Cursor.X++;
                        Lines = lines;
                    }
                    break;
            }
        }

        /// <inheritdoc />
        public override Pixel[,] Render()
        {
            char[,] inp1 = Content.ToNdArray2D();
            inp1 = inp1.Resize(Size.Height, Size.Width - 2, SpecialChars.Empty);
            char[,] inp = new char[Size.Width, Size.Height];
            inp.Populate(SpecialChars.Empty);
            for (int i = 0; i < Size.Height; i++)
            {
                inp[0, i] = '[';
                inp[Size.Width - 1, i] = ']';
            }
            if (Lines.Length < Size.Width)
                for (int i = 0; i < Size.Width; i++) inp[i, Size.Height - 1] = '.';
            inp1.Rotate().CopyTo(inp, new Point(0, 1));
            if (Selected)
                inp[Math.Max(Cursor.X + 1, 1), Cursor.Y] = '▒';
            Pixel[,] output = new Pixel[Size.Height, Size.Width];
            output.Populate(new Pixel(Selected ? ForeColor : BackColor, Selected ? BackColor : ForeColor, SpecialChars.Empty));
            for (int x = 0; x < Size.Width; x++)
            for (int y = 0; y < Size.Height; y++)
                output[y, x] = new Pixel(Selected ? ForeColor : BackColor, Selected ? BackColor : ForeColor, inp[x, y]);
            return output;
        }
    }
}