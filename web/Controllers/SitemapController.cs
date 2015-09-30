using System.Linq;
using System.Xml.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Web.Helpers;
using Web.Models;

namespace Web.Controllers
{
    public class SitemapController : Controller
    {
        private readonly DB db;

        public SitemapController()
        {
            db = new DB();
        }

        [HttpGet("sitemap.xml")]
        public string Index()
        {
            var xml = new XElement(XName.Get("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9"));

            addCmsSitemap(xml);
            addPhotoSitemap(xml);
            addTalksSitemap(xml);

            return new XDocument(new XDeclaration("1.0", "UTF-8", null), xml).ToString().Replace(" xmlns=\"\"", "");
        }

        private static void addTalksSitemap(XElement xml)
        {
            xml.Add(urlElementFor("talks"));
            xml.Add(urlElementFor("talks/hewny15"));
        }

        private void addCmsSitemap(XElement xml)
        {
            foreach (var category in db.Pages.GroupBy(p => p.Category))
            {
                var categoryName = category.Key.ToLower();

                if (!categoryName.Matches("Home"))
                    xml.Add(urlElementFor(categoryName));

                foreach (var page in category.Where(p => !p.Category.Matches("Home") && p.Crawl))
                    xml.Add(urlElementFor(categoryName + "/" + page.URL.ToLower()));
            }
        }

        private void addPhotoSitemap(XElement xml)
        {
            xml.Add(urlElementFor("photo"));
            foreach (var photo in db.Images)
                xml.Add(urlElementFor("photo/image/" + photo.ID));

            var allTags = db.Tags
               .Include(t => t.TagType)
               .Include(t => t.ImageTags)
               .ThenInclude(it => it.Image)
               .ToList();
            foreach (var tag in allTags.Where(t => t.ImageTags.Any(it => it.Image.Enabled)).OrderBy(t => t.TagType.ID).ThenBy(t => t.ID))
                xml.Add(urlElementFor("photo/" + tag.TagType.Name.ToLower() + "/" + tag.ID));
        }

        private static XElement urlElementFor(string url)
        {
            return new XElement("url", new XElement("loc", "http://stevedesmond.ca/" + url));
        }
    }
}