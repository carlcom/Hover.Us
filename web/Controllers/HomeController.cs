using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
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

        public async Task<IActionResult> Index()
        {
            var pages = db.Pages.Where(p => p.Aggregate).OrderByDescending(p => p.Timestamp).ToListAsync();
            return View(await pages);
        }

        public async Task<IActionResult> Page(string url)
        {
            var page = await db.Pages.FirstOrDefaultAsync(p => p.Category.Matches("Home") && p.URL.Matches(url));
            if (page != null)
                return SubPage(page);

            var pages = await db.Pages.Where(p => p.Category.Matches(url) && p.Aggregate).OrderByDescending(p => p.Timestamp).ToListAsync();
            if (!pages.Any())
                return Redirect("/");

            ViewBag.Subtitle = pages.First().Category;
            return View("Index", pages);
        }

        public async Task<IActionResult> SubPage(string category, string url)
        {
            var page = await db.Pages.FirstOrDefaultAsync(p => p.Category.Matches(category) && p.URL.Matches(url));
            return page != null
                ? SubPage(page)
                : Redirect("/" + category);
        }

        private IActionResult SubPage(Page page)
        {
            ViewBag.Subtitle = (page.Category.Matches("Home") ? "" : page.Category + " – ") + page.Title;
            return View("SubPage", page);
        }
    }
}