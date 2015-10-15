using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Web.Models;

namespace Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly string adminKey;
        private readonly DB db;

        public AdminController()
        {
            db = new DB();
            adminKey = System.IO.File.ReadAllText(Path.Combine("D:\\", "www", "admin.key"));
        }

        private bool authorized
        {
            get
            {
                var auth = Request.Headers["Authorization"];
                if (auth == null)
                    return false;

                var hash = Encoding.UTF8.GetString(SHA256.Create().ComputeHash(Convert.FromBase64String(auth.Substring(6))));
                return hash == adminKey;
            }
        }

        private IActionResult authorizationPrompt
        {
            get
            {
                Response.StatusCode = 401;
                Response.Headers.Append("WWW-Authenticate", "Basic");
                return new ContentResult();
            }
        }

        public async Task<IActionResult> Index()
        {
            if (!authorized)
                return authorizationPrompt;

            var pages = db.Pages.OrderBy(p => p.Category).ThenBy(p => p.Timestamp).ToListAsync();
            return View(await pages);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            if (!authorized)
                return authorizationPrompt;

            var page = new Page { ID = await db.Pages.MaxAsync(p => p.ID) + 1 };
            return View("Edit", page);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Page page)
        {
            if (!authorized)
                return authorizationPrompt;

            if (!ModelState.IsValid)
                return View("Edit", page);

            db.Pages.Add(page);
            await db.SaveChangesAsync();
            Cache.Flush();

            ViewBag.Success = true;
            return RedirectToAction("Edit", new { id = page.ID });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!authorized)
                return authorizationPrompt;

            var page = db.Pages.FirstAsync(p => p.ID == id);
            return View(await page);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Page page)
        {
            if (!authorized)
                return authorizationPrompt;

            if (!ModelState.IsValid)
                return View(page);

            db.Pages.Update(page);
            await db.SaveChangesAsync();
            Cache.Flush();

            ViewBag.Success = true;
            return View(page);
        }
    }
}