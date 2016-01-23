using System.IO;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.Net.Http.Headers;
using Web.Helpers;
using Web.Models;

namespace Web.Controllers
{
    public class ResumeController : Controller
    {
        public IActionResult Index(string For)
        {
            if (For != null)
            {
                var active = System.IO.File.ReadAllLines(Path.Combine("D:\\", "www", "active.txt"));
                if (active.Contains(For))
                {
                    var contactPage = Cache.Pages.First(p => p.Category.Matches("Resume") && p.URL.Matches("Contact"));
                    ViewBag.ContactInfo = contactPage.Body;
                }
            }
            var resume = Cache.Pages.First(p => p.Category.Matches("Resume") && p.URL.Matches("Resume"));
            ViewBag.Subtitle = resume.Title;
            ViewBag.NoFooter = true;
            return View("Index", resume);
        }

        public IActionResult Contact()
        {
            var lolPage = Cache.Pages.First(p => p.Category.Matches("Resume") && p.URL.Matches("LOL"));
            var niceTry = new ContentResult
            {
                ContentType = new MediaTypeHeaderValue("text/plain"),
                Content = lolPage.Body
            };
            return niceTry;
        }
    }
}
