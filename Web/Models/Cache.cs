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

        public static IList<Page> Pages
        {
            get
            {
                if (pages == null)
                {
                    var db = new DB();
                    pages = db.Pages.ToList();
                }
                return pages;
            }
        }

        private static IList<Image> images;
        public static IList<Image> Images
        {
            get
            {
                if (images == null)
                {
                    var db = new DB();
                    images = db.Images
                        .Include(i => i.ImageTags)
                        .ThenInclude(it => it.Tag)
                        .ThenInclude(t => t.TagType)
                        .ToList();
                }
                return images;
            }
        }

        private static IList<Page> frontPagePosts;
        public static IList<Page> FrontPagePosts
        {
            get
            {
                if (frontPagePosts == null)
                {
                    frontPagePosts = Pages
                        .Where(p => p.Category.Matches("Blog") && p.Aggregate)
                        .OrderByDescending(p => p.Timestamp)
                        .Take(8)
                        .ToList();
                }
                return frontPagePosts;
            }
        }

        private static IList<Image> frontPageImages;
        public static IList<Image> FrontPageImages
        {
            get
            {
                if (frontPageImages == null)
                {
                    frontPageImages = Images
                        .Where(i => i.Enabled)
                        .OrderByDescending(i => i.ID)
                        .Take(8)
                        .ToList();
                }
                return frontPageImages;
            }
        }

        private static string navbar;
        public static string Navbar
        {
            get
            {
                if (navbar == null)
                {
                    navbar = Pages
                        .First(p => p.Category == "Main" && p.Title == "Navbar")
                        .Body;
                }
                return navbar;
            }
        }

        private static string intro;
        public static string Intro
        {
            get
            {
                if (intro == null)
                {
                    intro = Pages
                        .First(p => p.Category == "Main" && p.Title == "Intro")
                        .Body;
                }
                return intro;
            }
        }

        [SuppressMessage("ReSharper", "UnusedVariable")]
        public static void Flush()
        {
            pages = null;
            images = null;
            frontPagePosts = null;
            frontPageImages = null;
            navbar = null;
            intro = null;

            var a = Pages;
            var b = Images;
            var c = FrontPagePosts;
            var d = FrontPageImages;
            var e = Navbar;
            var f = Intro;
        }
    }
}
