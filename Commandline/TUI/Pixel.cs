using System;
using System.Collections.Generic;

namespace CC_Functions.Commandline.TUI
{
    public class Pixel
    {
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

        public static IEqualityComparer<Pixel> ColorContentComparer { get; } = new ColorContentEqualityComparer();

        protected bool Equals(Pixel other) => ColorContentComparer.Equals(this, other);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Pixel) obj);
        }

        public override int GetHashCode() => HashCode.Combine((int) BackColor, (int) ForeColor, Content);

        public ConsoleColor BackColor;
        public ConsoleColor ForeColor;
        public char Content;

        public Pixel(ConsoleColor backColor, ConsoleColor foreColor, char content)
        {
            BackColor = backColor;
            ForeColor = foreColor;
            Content = content;
        }
        
        public Pixel(char content) : this(Console.BackgroundColor, Console.ForegroundColor, content)
        {
        }
        
        public Pixel() : this(' ')
        {
        }

        public static bool operator ==(Pixel a, Pixel b) => a.Equals(b);
        public static bool operator !=(Pixel a, Pixel b) => !a.Equals(b);

        public override string ToString() => Content.ToString();
    }
}