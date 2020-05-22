using System;
using System.Drawing;
using System.Linq;

namespace CC_Functions.Misc
{
    public static class ArrayFormatter
    {
        public static T[,] Resize<T>(this T[,] original, int rows, int cols)
        {
            T[,] newArray = new T[rows, cols];
            int minRows = Math.Min(rows, original.GetLength(0));
            int minCols = Math.Min(cols, original.GetLength(1));
            for (int i = 0; i < minRows; i++)
            for (int j = 0; j < minCols; j++)
                newArray[i, j] = original[i, j];
            return newArray;
        }

        public static char[,] ToNDArray2D(this string source, char defaultEl = SpecialChars.empty)
        {
            string[] sourceArr = source.Split('\n');
            int width = sourceArr.Select(s => s.Length).OrderBy(s => s).Last();
            int height = sourceArr.Length;
            char[,] output = new char[height, width];
            output.Populate(defaultEl);
            for (int i = 0; i < sourceArr.Length; i++)
            {
                string s = sourceArr[i];
                for (int j = 0; j < s.Length; j++)
                    output[i, j] = s[j];
            }
            return output;
        }
        
        public static void Populate<T>(this T[] arr, T value) {
            for ( int i = 0; i < arr.Length;i++ ) arr[i] = value;
        }
        
        public static void Populate<T>(this T[,] arr, T value)
        {
            int w = arr.GetLength(0);
            int h = arr.GetLength(1);
            for (int i = 0; i < w; i++)
            for (int j = 0; j < h; j++) arr[i, j] = value;
        }

        public static void CopyTo<T>(this T[,] arr, T[,] target, Point offset)
        {
            int w = arr.GetLength(1);
            int h = arr.GetLength(0);
            int mw = target.GetLength(1);
            int mh = target.GetLength(0);
            int ow = offset.X;
            int oh = offset.Y;
            for (int x = ow; x < Math.Min(mw, w + ow); x++)
            for (int y = oh; y < Math.Min(mh, h + oh); y++)
                target[y, x] = arr[y - oh, x - ow];
        }

        public static T[,] Rotate<T>(this T[,] arr)
        {
            int w = arr.GetLength(0);
            int h = arr.GetLength(1);
            T[,] target = new T[h, w];
            for (int x = 0; x < w; x++)
            for (int y = 0; y < h; y++) target[y, x] = arr[x, y];
            return target;
        }
    }
}