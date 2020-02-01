using System;

namespace CC_Functions.Misc
{
    public static class MathEx
    {
        public static decimal Abs(this decimal value) => Math.Abs(value);
        public static double Abs(this double value) => Math.Abs(value);
        public static float Abs(this float value) => Math.Abs(value);
        public static int Abs(this int value) => Math.Abs(value);
        public static long Abs(this long value) => Math.Abs(value);
        public static sbyte Abs(this sbyte value) => Math.Abs(value);
        public static short Abs(this short value) => Math.Abs(value);
        public static double Acos(this double d) => Math.Acos(d);
        public static double Asin(this double d) => Math.Asin(d);
        public static double Atan(this double d) => Math.Atan(d);
        public static double Atan2(this double x, double y) => Math.Atan2(x, y);
        public static decimal Ceiling(this decimal d) => Math.Ceiling(d);
        public static double Ceiling(this double d) => Math.Ceiling(d);
        public static double Cos(this double d) => Math.Cos(d);
        public static double Cosh(this double value) => Math.Cosh(value);
        public static double Exp(this double d) => Math.Exp(d);
        public static decimal Floor(this decimal d) => Math.Floor(d);
        public static double Floor(this double d) => Math.Floor(d);
        public static double Log(this double d) => Math.Log(d);
        public static double Log(this double d, double newBase) => Math.Log(d, newBase);
        public static double Log10(this double d) => Math.Log10(d);
        //Max
        //Min
        public static double Pow(this double x, double y) => Math.Pow(x, y);
        public static double Root(this double value, double n) => Math.Pow(value, 1 / n);
        public static decimal Round(this decimal d) => Math.Round(d);
        public static decimal Round(this decimal d, MidpointRounding mode) => Math.Round(d, mode);
        public static decimal Round(this decimal d, int decimals) => Math.Round(d, decimals);
        public static decimal Round(this decimal d, int decimals, MidpointRounding mode) => Math.Round(d, decimals, mode);
        public static double Round(this double a) => Math.Round(a);
        public static double Round(this double value, MidpointRounding mode) => Math.Round(value, mode);
        public static double Round(this double value, int digits) => Math.Round(value, digits);
        public static double Round(this double value, int digits, MidpointRounding mode) => Math.Round(value, digits, mode);
        public static int Sign(this decimal value) => Math.Sign(value);
        public static int Sign(this double value) => Math.Sign(value);
        public static int Sign(this float value) => Math.Sign(value);
        public static int Sign(this int value) => Math.Sign(value);
        public static int Sign(this long value) => Math.Sign(value);
        public static int Sign(this sbyte value) => Math.Sign(value);
        public static int Sign(this short value) => Math.Sign(value);
        public static double Sin(this double a) => Math.Sin(a);
        public static double Sinh(this double value) => Math.Sinh(value);
        public static double Sqrt(this double d) => Math.Sqrt(d);
        public static double Tan(this double a) => Math.Tan(a);
        public static double Tanh(this double value) => Math.Tanh(value);
        public static decimal Truncate(this decimal d) => Math.Truncate(d);
        public static double Truncate(this double d) => Math.Truncate(d);
        public static long BigMul(this int a, int b) => Math.BigMul(a, b);
        public static int DivRem(this int a, int b, out int result) => Math.DivRem(a, b, out result);
        public static long DivRem(this int a, int b, out long result) => Math.DivRem(a, b, out result);
        public static double IEEERemainder(this double x, double y) => Math.IEEERemainder(x, y);
    }
}