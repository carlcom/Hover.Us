using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;

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

        public static void Flush()
        {
            pages = null;
            images = null;
        }
    }
}
