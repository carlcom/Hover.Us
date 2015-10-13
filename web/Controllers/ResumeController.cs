using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Microsoft.Net.Http.Headers;
using Web.Helpers;
using Web.Models;

namespace Web.Controllers
{
    public class ResumeController : Controller
    {
        private readonly DB db;

        public ResumeController()
        {
            db = new DB();
        }

        public async Task<IActionResult> Index(string For)
        {
            if (For != null)
            {
                var active = System.IO.File.ReadAllLines(Path.Combine("D:\\", "www", "active.txt"));
                if (active.Contains(For))
                {
                    var contactPage = db.Pages.FirstOrDefaultAsync(p => p.Category.Matches("Resume") && p.URL.Matches("Contact"));
                    ViewBag.ContactInfo = (await contactPage).Body;
                }
            }
            var resume = await db.Pages.FirstOrDefaultAsync(p => p.Category.Matches("Resume") && p.URL.Matches("Resume"));
            ViewBag.Subtitle = resume.Title;
            return View("Index", resume);
        }

        public async Task<IActionResult> Contact()
        {
            var lolPage = db.Pages.FirstOrDefaultAsync(p => p.Category.Matches("Resume") && p.URL.Matches("LOL"));
            var niceTry = new ContentResult
            {
                ContentType = new MediaTypeHeaderValue("text/plain"),
                Content = (await lolPage).Body
            };
            return niceTry;
        }
    }
}
