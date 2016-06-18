using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Web.Models
{
    public static class Cache
    {
        private static IList<Page> pages;
        public static IList<Page> Pages => pages ?? (pages = new DB().Pages.ToList());

        private static string intro;
        public static string Intro
        {
            get
            {
                return intro ??
                    (intro = Pages
                        .First(p => p.Category == "Main" && p.Title == "Intro")
                        .Body);
            }
        }

        public static void Reset()
        {
            Flush();
            Prime();
        }

        public static void Flush()
        {
            pages = null;
            intro = null;
        }

        [SuppressMessage("ReSharper", "UnusedVariable")]
        public static void Prime()
        {
            var a = Pages;
            var b = Intro;
        }
    }
}
