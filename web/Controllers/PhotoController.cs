using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Web.Models;

namespace Web.Controllers
{
    public class PhotoController : Controller
    {
        private readonly DB db;
        private readonly List<Tag> allTags;

        public PhotoController()
        {
            db = new DB();

            allTags = db.Tags
               .Include(t => t.TagType)
               .Include(t => t.ImageTags)
               .ThenInclude(it => it.Image)
               .ToList();
        }

        public IActionResult Index()
        {
            ViewBag.Subtitle = "Photography";

            var images = db.Images
                .Where(i => i.Enabled)
                .OrderByDescending(i => i.DateTaken);

            return Images(images);
        }

        private ViewResult Images(IEnumerable<Image> images)
        {
            ViewBag.Subjects = getTags("Subject");
            ViewBag.Locations = getTags("Location");
            ViewBag.Mediums = getTags("Medium");
            ViewBag.Treatments = getTags("Treatment");
            return View("Index", images);
        }

        private IEnumerable<Tag> getTags(string type)
        {
            return allTags
                .Where(t => t.TagType.Name == type && t.ImageTags.Any(it => it.Image.Enabled))
                .OrderBy(t => t.Name);
        }

        public IActionResult Tag(int id)
        {
            var tag = db.Tags.SingleOrDefault(t => t.ID == id);
            if (tag == null)
                return Redirect("/photo");

            ViewBag.Subtitle = "Photography – " + tag.Name;

            var images = db.Images.Include(i => i.ImageTags)
                .Where(i => i.Enabled && i.ImageTags.Any(it => it.Tag_ID == tag.ID))
                .OrderByDescending(i => i.DateTaken);

            return Images(images);
        }

        public IActionResult Subject(int id) => Tag(id);
        public IActionResult Location(int id) => Tag(id);
        public IActionResult Medium(int id) => Tag(id);
        public IActionResult Treatment(int id) => Tag(id);

        public IActionResult Image(int id)
        {
            var image = db.Images
                .Include(i => i.ImageTags)
                .ThenInclude(it => it.Tag)
                .ThenInclude(t => t.TagType)
                .SingleOrDefault(i => i.ID == Convert.ToInt32(id));

            if (image == null)
            {
                return Redirect("/photo");
            }

            ViewBag.Subtitle = "Photography – " + image.Tag("Subject") + " – " + image.Tag("Location")  + " – " + image.DateTaken.ToString("d");
            return View(image);
        }
    }
}