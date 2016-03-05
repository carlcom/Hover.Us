using System.Linq;
using System.Xml.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.Net.Http.Headers;
using Web.Models;

namespace Web.Controllers
{
    public class FeedsController
    {
        public IActionResult rss()
        {
            var xml = new XElement("rss",
                new XAttribute("version", "2.0"),
                new XElement("channel",
                    new XElement("title", Settings.Title),
                    new XElement("description", Settings.Description),
                    new XElement("link", Settings.Domain),
                    Cache.FrontPagePosts
                        .Select(p => new XElement("item",
                            new XElement("title", p.Title),
                            new XElement("description", p.Description),
                            new XElement("link", p.FullURL),
                            new XElement("guid", p.FullURL),
                            new XElement("pubDate", p.Timestamp.ToString("r"))
                        )
                    )
                )
            );
            return new ContentResult { Content = xml.ToString(), ContentType = MediaTypeHeaderValue.Parse("application/rss+xml") };
        }

        public IActionResult atom()
        {
            var xml = new XElement("feed",
                new XElement("title", Settings.Title),
                new XElement("subtitle", Settings.Description),
                new XElement("link", new XAttribute("href", Settings.Domain)),
                new XElement("updated", Cache.FrontPagePosts.First().Timestamp.ToString("s") + "-05:00"),
                new XElement("id", Settings.Domain),
                Cache.FrontPagePosts
                    .Select(p => new XElement("entry",
                        new XElement("author",
                            new XElement("name", Settings.Title)),
                        new XElement("title", p.Title),
                        new XElement("summary", p.Description),
                        new XElement("link", new XAttribute("href", p.FullURL)),
                        new XElement("updated", p.Timestamp.ToString("s") + "-05:00"),
                        new XElement("id", p.FullURL)
                    )
                )
            );
            foreach (var element in xml.DescendantsAndSelf())
                element.Name = XName.Get(element.Name.LocalName, "http://www.w3.org/2005/Atom");
            return new ContentResult { Content = xml.ToString(), ContentType = MediaTypeHeaderValue.Parse("application/atom+xml") };
        }
    }
}