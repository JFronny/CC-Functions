using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace CC_Functions.Commandline
{
    /// <summary>
    ///     Provides functions for parsing enumerables to powershell-like tables
    /// </summary>
    public static class TableParser
    {
        /// <summary>
        ///     Parses the enumerable to a table using with the specified headers and transformed to strings with the specified
        ///     selector
        /// </summary>
        /// <param name="values">The values to display</param>
        /// <param name="columnHeaders">The headers for columns</param>
        /// <param name="valueSelectors">Functions to get data for the cells</param>
        /// <typeparam name="T">The type of the elements in the enumerable</typeparam>
        /// <returns>The generated table</returns>
        public static string ToStringTable<T>(this IEnumerable<T> values, string[] columnHeaders,
            params Func<T, object>[] valueSelectors) => ToStringTable(values.ToArray(), columnHeaders, valueSelectors);

        /// <summary>
        ///     Parses the array to a table using with the specified headers and transformed to strings with the specified selector
        /// </summary>
        /// <param name="values">The values to display</param>
        /// <param name="columnHeaders">The headers for columns</param>
        /// <param name="valueSelectors">Functions to get data for the cells</param>
        /// <typeparam name="T">The type of the elements in the array</typeparam>
        /// <returns>The generated table</returns>
        public static string ToStringTable<T>(this T[] values, string[] columnHeaders,
            params Func<T, object>[] valueSelectors)
        {
            Debug.Assert(columnHeaders.Length == valueSelectors.Length);

            string[,] arrValues = new string[values.Length + 1, valueSelectors.Length];

            // Fill headers
            for (int colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
                arrValues[0, colIndex] = columnHeaders[colIndex];

            // Fill table rows
            for (int rowIndex = 1; rowIndex < arrValues.GetLength(0); rowIndex++)
            for (int colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
            {
                object value = valueSelectors[colIndex].Invoke(values[rowIndex - 1]);

                arrValues[rowIndex, colIndex] = value != null ? value.ToString() : "null";
            }

            return ToStringTable(arrValues);
        }

        /// <summary>
        ///     Parses the array to a table
        /// </summary>
        /// <param name="arrValues">The cells of the table</param>
        /// <returns>The generated table</returns>
        public static string ToStringTable(this string[,] arrValues)
        {
            int[] maxColumnsWidth = GetMaxColumnsWidth(arrValues);
            string headerSpliter = new string('-', maxColumnsWidth.Sum(i => i + 3) - 1);

            StringBuilder sb = new StringBuilder();
            for (int rowIndex = 0; rowIndex < arrValues.GetLength(0); rowIndex++)
            {
                for (int colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
                {
                    // Print cell
                    string cell = arrValues[rowIndex, colIndex];
                    cell = cell.PadRight(maxColumnsWidth[colIndex]);
                    sb.Append(" | ");
                    sb.Append(cell);
                }

                // Print end of line
                sb.Append(" | ");
                sb.AppendLine();

                // Print splitter
                if (rowIndex == 0)
                {
                    sb.AppendFormat(" |{0}| ", headerSpliter);
                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }

        private static int[] GetMaxColumnsWidth(string[,] arrValues)
        {
            int[] maxColumnsWidth = new int[arrValues.GetLength(1)];
            for (int colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
            for (int rowIndex = 0; rowIndex < arrValues.GetLength(0); rowIndex++)
            {
                int newLength = arrValues[rowIndex, colIndex].Length;
                int oldLength = maxColumnsWidth[colIndex];

                if (newLength > oldLength) maxColumnsWidth[colIndex] = newLength;
            }

            return maxColumnsWidth;
        }

        /// <summary>
        ///     Parses the enumerable to a table, transformed to strings with the specified selector
        /// </summary>
        /// <param name="values">The values to display</param>
        /// <param name="valueSelectors">Functions to get data for the cells</param>
        /// <typeparam name="T">The type of the elements in the enumerable</typeparam>
        /// <returns>The generated table</returns>
        public static string ToStringTable<T>(this IEnumerable<T> values,
            params Expression<Func<T, object>>[] valueSelectors)
        {
            string[] headers = valueSelectors.Select(func => GetProperty(func).Name).ToArray();
            Func<T, object>[] selectors = valueSelectors.Select(exp => exp.Compile()).ToArray();
            return ToStringTable(values, headers, selectors);
        }

        private static PropertyInfo GetProperty<T>(Expression<Func<T, object>> expresstion)
        {
            if (expresstion.Body is UnaryExpression expression)
                if (expression.Operand is MemberExpression memberExpression)
                    return memberExpression.Member as PropertyInfo;

            if (expresstion.Body is MemberExpression body) return body.Member as PropertyInfo;
            return null;
        }
    }
}