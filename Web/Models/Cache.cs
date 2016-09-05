using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Web.Models
{
    public static class Cache
    {
        private static IEnumerable<Page> pages;
        public static IEnumerable<Page> Pages => pages ?? setPages(new DB().Pages.ToList());

        private static string intro;

        public static string Intro => intro ?? setIntro(Pages.First(p => p.Category == "Main" && p.Title == "Intro").Body);

        private static string setIntro(string content)
        {
            return intro = content;
        }

        private static IEnumerable<Page> setPages(IEnumerable<Page> content)
        {
            return pages = content;
        }

        [SuppressMessage("ReSharper", "UnusedVariable")]
        public static void Reset()
        {
            setPages(null);
            setIntro(null);
            var a = Pages;
            var b = Intro;
        }
    }
}
