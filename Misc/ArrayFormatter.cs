using System;
using System.Drawing;
using System.Linq;

namespace CC_Functions.Misc
{
    /// <summary>
    ///     Contains extension functions to work with 1D and 2D arrays
    /// </summary>
    public static class ArrayFormatter
    {
        /// <summary>
        ///     Copies and resizes the array
        /// </summary>
        /// <param name="original">The original array. This is not modified</param>
        /// <param name="elements">The new amount of elements</param>
        /// <typeparam name="T">The type of elements in the array</typeparam>
        /// <returns>The new, resized array</returns>
        public static T[] Resize<T>(this T[] original, int elements)
        {
            T[] output = new T[original.Length];
            original.CopyTo(output, 0);
            Array.Resize(ref output, elements);
            return output;
        }

        /// <summary>
        ///     Copies and resizes the array
        /// </summary>
        /// <param name="original">The original array. This is not modified</param>
        /// <param name="rows">The new amount of elements in dimension 0</param>
        /// <param name="cols">The new amount of elements in dimension 1</param>
        /// <param name="defaultEl">The element to place in empty fields of the new array</param>
        /// <typeparam name="T">The type of elements in the array</typeparam>
        /// <returns>The new, resized array</returns>
        public static T[,] Resize<T>(this T[,] original, int rows, int cols, T defaultEl = default)
        {
            T[,] newArray = new T[rows, cols];
            newArray.Populate(defaultEl);
            int minRows = Math.Min(rows, original.GetLength(0));
            int minCols = Math.Min(cols, original.GetLength(1));
            for (int i = 0; i < minRows; i++)
            for (int j = 0; j < minCols; j++)
                newArray[i, j] = original[i, j];
            return newArray;
        }

        /// <summary>
        ///     Converts a string to a 2d char array using newlines
        /// </summary>
        /// <param name="source">The source string</param>
        /// <param name="defaultEl">The element to place in empty fields of the new array</param>
        /// <returns>The generated array</returns>
        public static char[,] ToNdArray2D(this string source, char defaultEl = SpecialChars.Empty)
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

        /// <summary>
        ///     Clears and fills the array with the specified value
        /// </summary>
        /// <param name="arr">The array to populate</param>
        /// <param name="value">The value to copy to the array, defaults to the default value (usually null)</param>
        /// <typeparam name="T">The type of elements in the array</typeparam>
        public static void Populate<T>(this T[] arr, T value = default)
        {
            for (int i = 0; i < arr.Length; i++) arr[i] = value;
        }

        /// <summary>
        ///     Clears and fills the array with the specified value
        /// </summary>
        /// <param name="arr">The array to populate</param>
        /// <param name="value">The value to copy to the array, defaults to the default value (usually null)</param>
        /// <typeparam name="T">The type of elements in the array</typeparam>
        public static void Populate<T>(this T[,] arr, T value)
        {
            int w = arr.GetLength(0);
            int h = arr.GetLength(1);
            for (int i = 0; i < w; i++)
            for (int j = 0; j < h; j++)
                arr[i, j] = value;
        }

        /// <summary>
        ///     Copies the content of a 2D array to another with offset
        /// </summary>
        /// <param name="arr">The array to copy from</param>
        /// <param name="target">The array to copy to</param>
        /// <param name="offset">The copy offset</param>
        /// <typeparam name="T">The type of elements in the array</typeparam>
        public static void CopyTo<T>(this T[,] arr, T[,] target, Point offset)
        {
            int w = arr.GetLength(1);
            int h = arr.GetLength(0);
            int mw = target.GetLength(1);
            int mh = target.GetLength(0);
            int ow = offset.X;
            int oh = offset.Y;
            if (oh >= 0 && ow >= 0 && mw >= 0 && mh >= 0 && w >= 0 && h >= 0)
                for (int x = ow; x < Math.Min(mw, w + ow); x++)
                for (int y = oh; y < Math.Min(mh, h + oh); y++)
                    target[y, x] = arr[y - oh, x - ow];
        }

        /// <summary>
        ///     Copies and rotates the 2d array (row->column, column->row)
        /// </summary>
        /// <param name="arr">The array to copy from</param>
        /// <typeparam name="T">The type of elements in the array</typeparam>
        /// <returns>The new, rotated array</returns>
        public static T[,] Rotate<T>(this T[,] arr)
        {
            int w = arr.GetLength(0);
            int h = arr.GetLength(1);
            T[,] target = new T[h, w];
            for (int x = 0; x < w; x++)
            for (int y = 0; y < h; y++)
                target[y, x] = arr[x, y];
            return target;
        }
    }
}