using System;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Web.Models;

namespace Web.Controllers
{
    public class PhotoController : Controller
    {
        readonly DB db;

        public PhotoController()
        {
            db = new DB();
        }

        public IActionResult Index()
        {
            ViewBag.Subtitle = "Photography";
            return View(db.Images
                .Where(i => i.Enabled)
                .OrderByDescending(i => i.DateTaken));
        }

        public IActionResult Image(int id)
        {
            var image = db.Images
                .Include(i => i.ImageTags)
                .ThenInclude(it => it.Tag)
                .ThenInclude(t => t.TagType)
                .FirstOrDefault(i => i.ID == Convert.ToInt32(id));

            if (image == null)
            {
                return Redirect("/photo");
            }

            ViewBag.Subtitle = "View Photo";
            return View(image);
        }
    }
}