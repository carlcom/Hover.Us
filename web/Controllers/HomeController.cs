using System.Linq;
using Microsoft.AspNet.Mvc;
using Web.Helpers;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly DB db;

        public HomeController()
        {
            db = new DB();
        }

        public IActionResult Index()
        {
            var pages = db.Pages.Where(p => p.Aggregate).OrderByDescending(p => p.Timestamp);
            return View(pages);
        }

        public IActionResult Page(string url)
        {
            var page = db.Pages.FirstOrDefault(p => p.Category.Matches("Home") && p.URL.Matches(url));
            if (page != null)
            {
                return SubPage(page);
            }

            var pages = db.Pages.Where(p => p.Category.Matches(url) && p.Aggregate).OrderByDescending(p => p.Timestamp);
            if (!pages.Any())
            {
                return Redirect("/");
            }

            ViewBag.Subtitle = pages.First().Category;
            return View("Index", pages);
        }

        public IActionResult SubPage(string category, string url)
        {
            var page = db.Pages.FirstOrDefault(p => p.Category.Matches(category) && p.URL.Matches(url));
            if (page == null)
            {
                return Redirect("/" + category);
            }

            return SubPage(page);
        }

        private IActionResult SubPage(Page page)
        {
            ViewBag.Subtitle = (page.Category.Matches("Home") ? "" : page.Category + " – ") + page.Title;
            return View("SubPage", page);
        }
    }
}