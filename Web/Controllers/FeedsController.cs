﻿using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public sealed class FeedsController : Controller
    {
        private readonly IEnumerable<Page> livePosts;

        public FeedsController()
        {
            livePosts = Cache.Pages.Where(p => p.Category == "Blog" && p.Crawl).OrderByDescending(p => p.Timestamp);
        }

        public IActionResult rss()
        {
            var xml = new XElement("rss",
                new XAttribute("version", "2.0"),
                new XElement("channel",
                    new XElement("title", Settings.Title),
                    new XElement("description", Settings.Description),
                    new XElement("link", Settings.Domain),
                    livePosts.Select(p => new XElement("item",
                            new XElement("title", p.Title),
                            new XElement("description", p.Description),
                            new XElement("link", p.FullURL),
                            new XElement("guid", p.FullURL),
                            new XElement("pubDate", p.Timestamp.ToString("r"))
                        )
                    )
                )
            );
            return new ContentResult { Content = xml.ToString(), ContentType = "application/rss+xml" };
        }

        public IActionResult atom()
        {
            var xml = new XElement("feed",
                new XElement("title", Settings.Title),
                new XElement("subtitle", Settings.Description),
                new XElement("link", new XAttribute("href", Settings.Domain)),
                new XElement("updated", livePosts.First().Timestamp.ToString("s") + "-05:00"),
                new XElement("id", Settings.Domain),
                livePosts
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
            return new ContentResult { Content = xml.ToString(), ContentType = "application/atom+xml" };
        }
    }
}