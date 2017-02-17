using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Web.Models
{
    public static class Cache
    {
        public static int CSSHash { get; internal set; }

        private static IEnumerable<Page> pages;
        public static IEnumerable<Page> Pages => pages ?? setPages(new DB().Pages.ToList());

        private static IEnumerable<Page> setPages(IEnumerable<Page> content)
        {
            return pages = content;
        }

        private static string intro;
        public static string Intro => intro ?? setIntro(Pages.First(p => p.Category == "Main" && p.Title == "Intro").Body);
        public static IConfigurationRoot Config { get; set; }

        private static string setIntro(string content)
        {
            return intro = content;
        }
        
        public static void Reset()
        {
            setPages(null);
            setIntro(null);
            var a = Pages;
            var b = Intro;
        }
    }
}
