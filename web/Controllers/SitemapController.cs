using System.Linq;
using System.Xml.Linq;
using Microsoft.AspNet.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class SitemapController : Controller
    {
        readonly DB db;

        public SitemapController()
        {
            db = new DB();
        }

        public string Index()
        {
            var xml = new XElement(XName.Get("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9"));

            foreach (var category in db.Pages.GroupBy(p => p.Category))
            {
                var categoryName = category.Key.ToLower();
                xml.Add(urlElementFor(categoryName));
                foreach (var page in category)
                {
                    xml.Add(urlElementFor(categoryName + "/" + page.URL));
                }
            }

            xml.Add(urlElementFor("photo"));
            foreach (var photo in db.Images)
            {
                xml.Add(urlElementFor("photo/" + photo.ID));
            }

            xml.Add(urlElementFor("talks"));
            xml.Add(urlElementFor("talks/hewny15"));

            return new XDocument(new XDeclaration("1.0", "UTF-8", null), xml).ToString().Replace(" xmlns=\"\"", "");
        }

        private static XElement urlElementFor(string url)
        {
            return new XElement("url", new XElement("loc", "http://stevedesmond.ca/" + url));
        }
    }
}
