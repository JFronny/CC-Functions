using System;
using System.Drawing;
using CC_Functions.Misc;

namespace CC_Functions.Commandline.TUI
{
    /// <summary>
    /// Provides differential drawing of a char[,] Do not use in combination with System.Console
    /// </summary>
    public static class DiffDraw
    {
        private static Pixel[,] Screen { get; set; } = new Pixel[0, 0];
        private static Pixel[,] _last = new Pixel[0,0];
        public static int Width => Screen.GetLength(1);
        public static int Height => Screen.GetLength(0);

        public static void Draw(bool color)
        {
            Console.CursorTop = 0;
            Console.CursorLeft = 0;
            ConsoleColor fcol = Console.ForegroundColor;
            ConsoleColor bcol = Console.BackgroundColor;
            int width = Width;
            int height = Height;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Pixel tmp1 = Screen[y, x];
                    if (tmp1 == _last[y, x]) continue;
                    if (color)
                    {
                        Console.ForegroundColor = tmp1.ForeColor;
                        Console.BackgroundColor = tmp1.BackColor;
                    }
                    Console.CursorLeft = x;
                    Console.Write(tmp1);
                }
                Console.WriteLine();
                Console.CursorLeft = 0;
            }
            Console.ForegroundColor = fcol;
            Console.BackgroundColor = bcol;
            _last = Screen;
        }

        public static void FullDraw(bool color)
        {
            Console.CursorTop = 0;
            Console.CursorLeft = 0;
            ConsoleColor fcol = Console.ForegroundColor;
            ConsoleColor bcol = Console.BackgroundColor;
            Console.Clear();
            int width = Width;
            int height = Height;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Pixel tmp1 = Screen[y, x];
                    if (color)
                    {
                        Console.ForegroundColor = tmp1.ForeColor;
                        Console.BackgroundColor = tmp1.BackColor;
                    }
                    Console.CursorLeft = x;
                    Console.Write(tmp1);
                }
                Console.WriteLine();
                Console.CursorLeft = 0;
            }
            Console.ForegroundColor = fcol;
            Console.BackgroundColor = bcol;
            _last = Screen;
        }

        public static char Get(Point p) => Get(p.X, p.Y);
        public static char Get(int x, int y) => Screen[y, x].Content;
        public static ConsoleColor GetForeColor(Point p) => GetForeColor(p.X, p.Y);
        public static ConsoleColor GetForeColor(int x, int y) => Screen[y, x].ForeColor;
        public static ConsoleColor GetBackColor(Point p) => GetBackColor(p.X, p.Y);
        public static ConsoleColor GetBackColor(int x, int y) => Screen[y, x].BackColor;

        public static void Set(Point p, Pixel c) => Set(p.X, p.Y, c);

        public static void Set(int x, int y, Pixel c) => Screen[y, x] = c;

        public static void Clear() => Clear(Width, Height);

        public static void Clear(int width, int height)
        {
            Screen = new Pixel[height, width];
            _last = _last.Resize(height, width);
        }

        public static void Clear(Pixel[,] content)
        {
            Screen = content;
            _last = _last.Resize(Height, Width);
        }
    }
}