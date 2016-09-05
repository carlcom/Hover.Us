using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Web.Helpers;
using Web.Models;

namespace Web.Controllers
{
    public sealed class ResumeController : Controller
    {
        public IActionResult Index(string For)
        {
            setViewBagInfo(For);
            var resume = Cache.Pages.First(p => p.Category.Matches("Resume") && p.URL.Matches("Resume"));
            return View(resume);
        }

        public IActionResult Draft(string For)
        {
            setViewBagInfo(For);
            var resume = Cache.Pages.First(p => p.Category.Matches("Resume") && p.URL.Matches("Draft"));
            return View("Index", resume);
        }

        private void setViewBagInfo(string For)
        {
            ViewBag.Subtitle = "Resume";
            ViewBag.NoFooter = true;
            if (For != null)
            {
                var contactUsers = Startup.GetCredentials()["ContactUsers"].Split(',');
                if (contactUsers.Contains(For))
                {
                    var contactPage = Cache.Pages.First(p => p.Category.Matches("Resume") && p.URL.Matches("Contact"));
                    ViewBag.ContactInfo = contactPage.Body;
                }
            }
        }

        public IActionResult Contact()
        {
            var lolPage = Cache.Pages.First(p => p.Category.Matches("Resume") && p.URL.Matches("LOL"));
            var niceTry = new ContentResult
            {
                ContentType = "text/plain",
                Content = lolPage.Body
            };
            return niceTry;
        }
    }
}
