using System;
using System.IO;

namespace web
{
    public static class Helpers
    {
        public static string SolutionRoot => new DirectoryInfo(Environment.CurrentDirectory).Parent?.Parent?.FullName;
    }
}
