using System;

namespace Web.Helpers
{
    public static class StringExtensions
    {
        public static string Minify(this string s)
        {
            return s.Replace("  ", "").Replace(Environment.NewLine, "");
        }
    }
}