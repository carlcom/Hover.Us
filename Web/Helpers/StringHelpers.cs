using System;

namespace Web.Helpers
{
    public static class StringHelpers
    {
        public static bool Matches(this string left, string right)
        {
            return string.Equals(left, right, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}