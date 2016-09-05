using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Web.Helpers;
using Web.Models;

namespace Web.Controllers
{
    public sealed class HomeController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult Page(string url)
        {
            var pages = Cache.Pages.Where(p => p.Category.Matches(url) && p.Aggregate).OrderByDescending(p => p.Timestamp);
            if (!pages.Any())
                return Redirect("/");

            ViewBag.Subtitle = pages.First().Category;
            return View(pages);
        }

        public IActionResult SubPage(string category, string url)
        {
            var page = Cache.Pages.FirstOrDefault(p => p.Category.Matches(category) && p.URL.Matches(url));
            return page != null
                ? SubPage(page)
                : Redirect("/" + category);
        }

        private IActionResult SubPage(Page page)
        {
            ViewBag.Subtitle = page.Category + " – " + page.Title;
            ViewBag.Description = page.Description;
            return View("SubPage", page);
        }
    }
}