using System;

namespace VTSV.EXIF
{
    /// <summary>
    /// private class
    /// </summary>
    public sealed class Rational
    {
        private readonly int n;
        private readonly int d;
        public Rational(int n, int d)
        {
            this.n = n;
            this.d = d;
            simplify(ref n, ref d);
        }
        public Rational(uint n, uint d)
        {
            this.n = Convert.ToInt32(n);
            this.d = Convert.ToInt32(d);

            simplify(ref this.n, ref this.d);
        }

        public string ToString(string sp)
        {
            if (sp == null) sp = "/";
            return n + sp + d;
        }
        public double ToDouble()
        {
            return d == 0 ? 0.0 : Math.Round(Convert.ToDouble(n) / Convert.ToDouble(d), 2);
        }

        private static void simplify(ref int a, ref int b)
        {
            if (a == 0 || b == 0)
                return;

            var gcd = euclid(a, b);
            a /= gcd;
            b /= gcd;
        }
        private static int euclid(int a, int b)
        {
            return b == 0 ? a : euclid(b, a % b);
        }
    }
}