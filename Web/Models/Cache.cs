using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.Data.Entity;
using Web.Helpers;

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

        private static IList<Page> frontPagePosts;
        public static IList<Page> FrontPagePosts
        {
            get
            {
                return frontPagePosts
                    ?? (frontPagePosts = Pages
                        .Where(p => p.Category.Matches("Blog") && p.Aggregate)
                        .OrderByDescending(p => p.Timestamp)
                        .Take(8)
                        .ToList());
            }
        }

        private static IList<Image> frontPageImages;
        public static IList<Image> FrontPageImages
        {
            get
            {
                return frontPageImages
                    ?? (frontPageImages = Images
                        .Where(i => i.Enabled)
                        .OrderByDescending(i => i.ID)
                        .Take(8)
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
            frontPagePosts = null;
            frontPageImages = null;
            intro = null;
        }

        [SuppressMessage("ReSharper", "UnusedVariable")]
        public static void Prime()
        {
            var a = Pages;
            var b = Images;
            var c = FrontPagePosts;
            var d = FrontPageImages;
            var e = Intro;
        }
    }
}
