using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.Data.Entity;

namespace Web.Models
{
    public static class Cache
    {
        private static IList<Page> pages;
        public static IList<Page> Pages => pages ?? (pages = new DB().Pages.ToList());

        private static IList<Image> images;
        public static IList<Image> Images
        {
            get
            {
                return images
                    ?? (images = new DB().Images
                        .Include(i => i.ImageTags)
                        .ThenInclude(it => it.Tag)
                        .ThenInclude(t => t.TagType)
                        .ToList());
            }
        }

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
            images = null;
            intro = null;
        }

        [SuppressMessage("ReSharper", "UnusedVariable")]
        public static void Prime()
        {
            var a = Pages;
            var b = Images;
            var e = Intro;
        }
    }
}
