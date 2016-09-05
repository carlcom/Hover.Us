using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public sealed class AdminController : Controller
    {
        private readonly DB db;

        public AdminController()
        {
            db = new DB();
        }

        private bool authorized
        {
            get
            {
                var auth = Request.Headers["Authorization"];
                if (auth.Count != 1)
                    return false;

                var hash = Encoding.UTF8.GetString(SHA256.Create().ComputeHash(Convert.FromBase64String(auth.First().Substring(6))));
                return hash == Startup.GetCredentials()["AdminKey"];
            }
        }

        private IActionResult authorizationPrompt
        {
            get
            {
                Response.StatusCode = 401;
                Response.Headers.Add("WWW-Authenticate", "Basic");
                return new ContentResult();
            }
        }

        public IActionResult Index()
        {
            if (!authorized)
                return authorizationPrompt;

            var pages = Cache.Pages.OrderBy(p => p.Category).ThenByDescending(p => p.Timestamp);
            ViewBag.Subtitle = "Admin";
            return View(pages);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!authorized)
                return authorizationPrompt;

            var page = new Page { ID = db.Pages.Max(p => p.ID) + 1 };
            ViewBag.Subtitle = "Create";
            return View("Edit", page);
        }

        [HttpPost]
        public IActionResult Create(Page page)
        {
            if (!authorized)
                return authorizationPrompt;

            if (!ModelState.IsValid)
            {
                ViewBag.Subtitle = "Create";
                return View("Edit", page);
            }

            db.Pages.Add(page);
            db.SaveChanges();
            Cache.Reset();

            ViewBag.Success = true;
            return RedirectToAction("Edit", new { id = page.ID });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!authorized)
                return authorizationPrompt;

            var page = db.Pages.First(p => p.ID == id);
            ViewBag.Subtitle = "Edit – " + page.Title;
            return View(page);
        }

        [HttpPost]
        public IActionResult Edit(Page page)
        {
            if (!authorized)
                return authorizationPrompt;

            ViewBag.Subtitle = "Edit – " + page.Title;

            if (!ModelState.IsValid)
                return View(page);

            db.Pages.Update(page);
            db.SaveChanges();
            Cache.Reset();

            ViewBag.Success = true;
            return View(page);
        }

        public IActionResult Flush()
        {
            Cache.Reset();
            return RedirectToAction("Index");
        }
    }
}