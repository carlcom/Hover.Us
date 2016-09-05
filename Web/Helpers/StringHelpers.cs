using System;

namespace Web.Helpers
{
    internal static class StringHelpers
    {
        internal static bool Matches(this string left, string right)
        {
            return string.Equals(left, right, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}