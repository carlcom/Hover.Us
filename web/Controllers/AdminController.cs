using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNet.Mvc;
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
            adminKey = System.IO.File.ReadAllText("D:\\www\\admin.key").Replace("\\u", "\\u");
        }

        bool authorized
        {
            get
            {
                var auth = Request.Headers["Authorization"];
                if (auth == null) return false;

                var hash = Encoding.UTF8.GetString(SHA256.Create().ComputeHash(Convert.FromBase64String(auth.Substring(6))));
                return hash == adminKey;
            }
        }

        IActionResult authorizationPrompt
        {
            get
            {
                Response.StatusCode = 401;
                Response.Headers.Append("WWW-Authenticate", "Basic");
                return new ContentResult();
            }
        }

        public IActionResult Index()
        {
            if (!authorized) return authorizationPrompt;

            var pages = db.Pages.OrderByDescending(p => p.ID);
            return View(pages);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            if (!authorized) return authorizationPrompt;

            var page = new Page { ID = db.Pages.Max(p => p.ID) + 1 };
            return View("Edit", page);
        }

        [HttpPost("create")]
        public IActionResult Create(Page page)
        {
            if (!authorized) return authorizationPrompt;
            if (!ModelState.IsValid) return View("Edit", page);

            db.Pages.Add(page);
            db.SaveChanges();
            ViewBag.Success = true;
            return RedirectToAction("Edit", new { id = page.ID });
        }

        [HttpGet("edit")]
        public IActionResult Edit(int id)
        {
            if (!authorized) return authorizationPrompt;

            var page = db.Pages.First(p => p.ID == id);
            return View(page);
        }

        [HttpPost("edit")]
        public IActionResult Edit(Page page)
        {
            if (!authorized) return authorizationPrompt;
            if (!ModelState.IsValid) return View(page);

            db.Pages.Update(page);
            db.SaveChanges();
            ViewBag.Success = true;
            return View(page);
        }
    }
}