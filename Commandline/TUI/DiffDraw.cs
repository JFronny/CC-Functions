using System;
using System.Drawing;
using CC_Functions.Misc;

namespace CC_Functions.Commandline.TUI
{
    /// <summary>
    ///     Provides differential drawing of a char[,] Do not use in combination with System.Console
    /// </summary>
    public static class DiffDraw
    {
        private static Pixel[,] _last = new Pixel[0, 0];
        private static Pixel[,] Screen { get; set; } = new Pixel[0, 0];

        /// <summary>
        ///     The regions width
        /// </summary>
        public static int Width => Screen.GetLength(1);

        /// <summary>
        ///     The regions height
        /// </summary>
        public static int Height => Screen.GetLength(0);

        /// <summary>
        ///     Draws to the console
        /// </summary>
        /// <param name="color">Whether to use color</param>
        public static void Draw(bool color)
        {
            Console.CursorTop = 0;
            Console.CursorLeft = 0;
            ConsoleColor fCol = Console.ForegroundColor;
            ConsoleColor bCol = Console.BackgroundColor;
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
                        if (Console.ForegroundColor != tmp1.ForeColor)
                            Console.ForegroundColor = tmp1.ForeColor;
                        if (Console.BackgroundColor != tmp1.BackColor)
                            Console.BackgroundColor = tmp1.BackColor;
                    }
                    Console.CursorLeft = x;
                    Console.Write(tmp1);
                }
                Console.WriteLine();
                Console.CursorLeft = 0;
            }
            Console.ForegroundColor = fCol;
            Console.BackgroundColor = bCol;
            _last = Screen;
        }

        /// <summary>
        ///     Redraws the entire screen (should be done from time to time to prevent corruption)
        /// </summary>
        /// <param name="color">Whether to use color</param>
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
                    Pixel? tmp1 = Screen[y, x];
                    if (tmp1 != null && color)
                    {
                        if (Console.ForegroundColor != tmp1.ForeColor)
                            Console.ForegroundColor = tmp1.ForeColor;
                        if (Console.BackgroundColor != tmp1.BackColor)
                            Console.BackgroundColor = tmp1.BackColor;
                    }
                    Console.CursorLeft = x;
                    Console.Write(tmp1 ?? Pixel.Empty);
                }
                Console.WriteLine();
                Console.CursorLeft = 0;
            }
            Console.ForegroundColor = fcol;
            Console.BackgroundColor = bcol;
            _last = Screen;
        }

        /// <summary>
        ///     Gets the char at a location
        /// </summary>
        /// <param name="p">The location</param>
        /// <returns>The char</returns>
        public static char Get(Point p) => Get(p.X, p.Y);

        /// <summary>
        ///     Gets the char at a location
        /// </summary>
        /// <param name="x">The locations X coordinate</param>
        /// <param name="y">The locations Y coordinate</param>
        /// <returns>The char</returns>
        public static char Get(int x, int y) => Screen[y, x].Content;

        /// <summary>
        ///     Gets the foreground color at a location
        /// </summary>
        /// <param name="p">The location</param>
        /// <returns>The color</returns>
        public static ConsoleColor GetForeColor(Point p) => GetForeColor(p.X, p.Y);

        /// <summary>
        ///     Gets the foreground color at a location
        /// </summary>
        /// <param name="x">The locations X coordinate</param>
        /// <param name="y">The locations Y coordinate</param>
        /// <returns>The color</returns>
        public static ConsoleColor GetForeColor(int x, int y) => Screen[y, x].ForeColor;

        /// <summary>
        ///     Gets the background color at a location
        /// </summary>
        /// <param name="p">The location</param>
        /// <returns>The color</returns>
        public static ConsoleColor GetBackColor(Point p) => GetBackColor(p.X, p.Y);

        /// <summary>
        ///     Gets the background color at a location
        /// </summary>
        /// <param name="x">The locations X coordinate</param>
        /// <param name="y">The locations Y coordinate</param>
        /// <returns>The color</returns>
        public static ConsoleColor GetBackColor(int x, int y) => Screen[y, x].BackColor;

        /// <summary>
        ///     Sets a pixel at a point
        /// </summary>
        /// <param name="p">The point to place at</param>
        /// <param name="c">The pixel to place</param>
        public static void Set(Point p, Pixel c) => Set(p.X, p.Y, c);

        /// <summary>
        ///     Sets a pixel at a location
        /// </summary>
        /// <param name="x">The locations X coordinate</param>
        /// <param name="y">The locations Y coordinate</param>
        /// <param name="c">The pixel to place</param>
        public static void Set(int x, int y, Pixel c) => Screen[y, x] = c;

        /// <summary>
        ///     Clears the screen
        /// </summary>
        public static void Clear() => Clear(Width, Height);

        /// <summary>
        ///     Resizes and clears the screen
        /// </summary>
        /// <param name="width">The new width</param>
        /// <param name="height">The new height</param>
        public static void Clear(int width, int height)
        {
            Screen = new Pixel[height, width];
            _last = _last.Resize(height, width);
        }

        /// <summary>
        ///     Replaces the screen state
        /// </summary>
        /// <param name="content">The new state</param>
        public static void Clear(Pixel[,] content)
        {
            Screen = content;
            _last = _last.Resize(Height, Width);
        }
    }
}