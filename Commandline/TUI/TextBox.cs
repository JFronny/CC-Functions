using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CC_Functions.Misc;

namespace CC_Functions.Commandline.TUI
{
    public class TextBox : Control
    {
        public string Content;
        private string[] Lines
        {
            get => Content.Split('\n');
            set => Content = string.Join('\n', value);
        }

        public Point Cursor = new Point(0, 0);
        public TextBox(string content)
        {
            Content = content;
            Input += (screen, args) =>
            {
                ProcessInput(args.Info.Key, args.Info);
            };
            Click += (screen, args) => ProcessInput(ConsoleKey.Enter, new ConsoleKeyInfo());
        }

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
                            Cursor.X = tmplen - 1;
                        }
                    }
                    Lines = lines;
                    break;
                case ConsoleKey.Enter:
                    tmp = lines.ToList();
                    lines[Cursor.Y] = lines[Cursor.Y].Insert(Cursor.X, "\n");
                    Cursor.Y++;
                    Cursor.X = 0;
                    Lines = lines;
                    break;
                default:
                    lines[Cursor.Y] = lines[Cursor.Y].Insert(Cursor.X, info.KeyChar.ToString());
                    Lines = lines;
                    break;
            }
        }

        public override Pixel[,] Render()
        {
            char[,] inp1 = Content.ToNDArray2D();
            inp1 = inp1.Resize(Size.Height, Size.Width - 2);
            char[,] inp = new char[Size.Width, Size.Height];
            inp.Populate(SpecialChars.empty);
            for (int i = 0; i < Size.Height; i++)
            {
                inp[0, i] = '[';
                inp[Size.Width - 1, i] = ']';
            }
            for (int i = 0; i < Size.Width; i++) inp[i, Size.Height - 1] = '.';
            inp1.Rotate().CopyTo(inp, new Point(0, 1));
            if (Selected)
                inp[Cursor.X + 1, Cursor.Y] = '▒';
            Pixel[,] output = new Pixel[Size.Height, Size.Width];
            for (int x = 0; x < Size.Width; x++)
            for (int y = 0; y < Size.Height; y++)
                output[y, x] = new Pixel(Selected ? ForeColor : BackColor, Selected ? BackColor : ForeColor, inp[x, y]);
            return output;
        }

        protected override void Resize(int width, int height)
        {
            //ignored for [Render]s sake, do not use
        }

        public override bool Selectable { get; } = true;
    }
}