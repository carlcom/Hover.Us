using System.Linq;
using System.Xml.Linq;
using Microsoft.AspNet.Mvc;
using Web.Helpers;
using Web.Models;

namespace Web.Controllers
{
    public class SitemapController : Controller
    {
        [HttpGet("sitemap.xml")]
        public string Index()
        {
            var xml = new XElement(XName.Get("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9"));

            addCmsSitemap(xml);
            addPhotoSitemap(xml);

            return new XDocument(new XDeclaration("1.0", "UTF-8", null), xml).ToString().Replace(" xmlns=\"\"", "");
        }

        private static void addCmsSitemap(XContainer xml)
        {
            var pages = Cache.Pages.GroupBy(p => p.Category);
            foreach (var category in pages)
            {
                var categoryName = category.Key.ToLower();

                if (!categoryName.Matches("Home"))
                    xml.Add(urlElementFor(categoryName));

                foreach (var page in category.Where(p => !p.Category.Matches("Home") && p.Crawl))
                    xml.Add(urlElementFor(categoryName + "/" + page.URL.ToLower()));
            }
        }

        private void addPhotoSitemap(XContainer xml)
        {
            xml.Add(urlElementFor("photo"));
            foreach (var photo in Cache.Images)
                xml.Add(urlElementFor("photo/image/" + photo.ID));

            var allTags = Cache.Images.SelectMany(i => i.ImageTags.Select(it => it.Tag)).Distinct();
            var tags = allTags
               .Where(t => t.ImageTags.Any(it => it.Image.Enabled))
               .OrderBy(t => t.TagType.ID)
               .ThenBy(t => t.ID)
               .ToList();
            foreach (var tag in tags)
                xml.Add(urlElementFor("photo/" + tag.TagType.Name.ToLower() + "/" + tag.ID));
        }

        private static XElement urlElementFor(string url)
        {
            return new XElement("url", new XElement("loc", Startup.Domain + url));
        }
    }
}