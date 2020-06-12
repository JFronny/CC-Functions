using System;
using System.Collections.Generic;

namespace CC_Functions.Commandline.TUI
{
    /// <summary>
    ///     Represents a pixel
    /// </summary>
    public class Pixel
    {
        public static readonly Pixel Empty = new Pixel();
        
        /// <summary>
        ///     This pixels background color
        /// </summary>
        public ConsoleColor BackColor;

        /// <summary>
        ///     This pixels content character
        /// </summary>
        public char Content;

        /// <summary>
        ///     This pixels foregound color
        /// </summary>
        public ConsoleColor ForeColor;

        /// <summary>
        ///     Generates a new pixel
        /// </summary>
        /// <param name="backColor">The background color</param>
        /// <param name="foreColor">The foreground color</param>
        /// <param name="content">The new content</param>
        public Pixel(ConsoleColor backColor, ConsoleColor foreColor, char content)
        {
            BackColor = backColor;
            ForeColor = foreColor;
            Content = content;
        }

        /// <summary>
        ///     Generates a new pixel
        /// </summary>
        /// <param name="content">The content for this pixel</param>
        public Pixel(char content) : this(Console.BackgroundColor, Console.ForegroundColor, content)
        {
        }

        /// <summary>
        ///     Generates a new pixel
        /// </summary>
        public Pixel() : this(' ')
        {
        }

        /// <summary>
        ///     Use this in functions that require equality comparers
        /// </summary>
        public static IEqualityComparer<Pixel> ColorContentComparer { get; } = new ColorContentEqualityComparer();

        /// <summary>
        ///     Whether this is equal to another pixel
        /// </summary>
        /// <param name="other">The other pixel to compare</param>
        /// <returns>Whether they are equal</returns>
        protected bool Equals(Pixel other) => ColorContentComparer.Equals(this, other);

        /// <summary>
        ///     Whether this is equal to another object
        /// </summary>
        /// <param name="obj">The other object to compare</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Pixel) obj);
        }

        /// <summary>
        ///     Generates an integer for comparing this object
        /// </summary>
        /// <returns>The generated hash</returns>
        public override int GetHashCode() => HashCode.Combine(BackColor, ForeColor, Content);

        /// <summary>
        ///     Whether two pixels are equal
        /// </summary>
        /// <param name="a">First pixel to compare</param>
        /// <param name="b">Second pixel to compare</param>
        /// <returns>Whether they are equal</returns>
        public static bool operator ==(Pixel a, Pixel b) => !ReferenceEquals(a, null) && a.Equals(b);

        /// <summary>
        ///     Whether to pixels are not equal
        /// </summary>
        /// <param name="a">First pixel to compare</param>
        /// <param name="b">Second pixel to compare</param>
        /// <returns>Whether they are not equal</returns>
        public static bool operator !=(Pixel a, Pixel b) => !(a == b);

        /// <summary>
        ///     Returns the content of this pixel
        /// </summary>
        /// <returns>The content of this pixel</returns>
        public override string ToString() => Content.ToString();

        private sealed class ColorContentEqualityComparer : IEqualityComparer<Pixel>
        {
            public bool Equals(Pixel x, Pixel y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.GetHashCode().Equals(y.GetHashCode());
            }

            public int GetHashCode(Pixel obj) => obj.GetHashCode();
        }
    }
}