using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class PhotoController : Controller
    {
        private static IEnumerable<Tag> tags => Cache.Images
            .SelectMany(i => i.ImageTags.Select(t => t.Tag))
            .Distinct()
            .OrderBy(t => t.Name)
            .ToList();

        public IActionResult Index()
        {
            var images = Cache.Images
                .Where(i => i.Enabled)
                .OrderByDescending(i => i.DateTaken);

            ViewBag.Subtitle = "Photography";
            return Images(images);
        }

        private IActionResult Images(IEnumerable<Image> images)
        {
            ViewBag.Subjects = tags.Where(t => t.TagType.Name == "Subject");
            ViewBag.Locations = tags.Where(t => t.TagType.Name == "Location");
            ViewBag.Mediums = tags.Where(t => t.TagType.Name == "Medium");
            ViewBag.Treatments = tags.Where(t => t.TagType.Name == "Treatment");
            ViewBag.Orientations = tags.Where(t => t.TagType.Name == "Orientation");
            return View("Index", images);
        }

        public IActionResult Tag(int id)
        {
            var tag = tags.FirstOrDefault(t => t.ID == id);
            if (tag == null)
                return Redirect("/photo");

            ViewBag.Subtitle = "Photography – " + tag.Name;

            var images = Cache.Images
                .Where(i => i.Enabled && i.ImageTags.Any(it => it.Tag_ID == tag.ID))
                .OrderByDescending(i => i.DateTaken)
                .ToList();

            return Images(images);
        }

        public IActionResult Subject(int id) => Tag(id);
        public IActionResult Location(int id) => Tag(id);
        public IActionResult Medium(int id) => Tag(id);
        public IActionResult Treatment(int id) => Tag(id);
        public IActionResult Orientation(int id) => Tag(id);

        public IActionResult Image(int id)
        {
            var image = Cache.Images.FirstOrDefault(i => i.ID == Convert.ToInt32(id));

            if (image == null)
                return Redirect("/photo");

            ViewBag.Subtitle = "Photography – "
                + image.Tag("Subject")
                + (string.IsNullOrEmpty(image.Tag("Subject")) ? "" : " – ")
                + image.Tag("Location")
                + (string.IsNullOrEmpty(image.Tag("Location")) ? "" : " – ")
                + image.DateTaken.ToString("d");
            return View(image);
        }
    }
}