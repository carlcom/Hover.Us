using System.Linq;
using Microsoft.AspNet.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        readonly DB db;

        public HomeController()
        {
            db = new DB();
        }

        public IActionResult Index()
        {
            var pages = db.Pages.Where(p => p.Live).OrderByDescending(p => p.Timestamp);
            return View(pages);
        }

        public IActionResult Category(string category)
        {
            var pages = db.Pages.Where(p => p.Live && p.Category.ToLower() == category).OrderByDescending(p => p.Timestamp);
            if (!pages.Any())
            {
                return Redirect("/");
            }

            ViewBag.Subtitle = pages.First().Category;
            return View("Index", pages);
        }

        public IActionResult Page(string category, string url)
        {
            var page = db.Pages.FirstOrDefault(p => p.Category.ToLower() == category && p.URL == url);
            if (page == null)
            {
                return Redirect("/" + category);
            }

            ViewBag.Subtitle = page.Category + " — " + page.Title;
            return View("Page", page);
        }
    }
}
