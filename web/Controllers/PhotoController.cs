using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Web.Models;

namespace Web.Controllers
{
    public class PhotoController : Controller
    {
        private readonly DB db;
        private readonly Task<List<Tag>> allTags;

        public PhotoController()
        {
            db = new DB();

            allTags = db.Tags
               .Include(t => t.TagType)
               .Include(t => t.ImageTags)
               .ThenInclude(it => it.Image)
               .ToListAsync();
        }

        public async Task<IActionResult> Index()
        {
            var images = db.Images
                .Where(i => i.Enabled)
                .OrderByDescending(i => i.DateTaken)
                .ToListAsync();

            ViewBag.Subtitle = "Photography";
            return await Images(images);
        }

        private async Task<IActionResult> Images(Task<List<Image>> images)
        {
            ViewBag.Subjects = getTags("Subject");
            ViewBag.Locations = getTags("Location");
            ViewBag.Mediums = getTags("Medium");
            ViewBag.Treatments = getTags("Treatment");
            return View("Index", await images);
        }

        private async Task<IEnumerable<Tag>> getTags(string type)
        {
            var tags = await allTags;
            return tags
                .Where(t => t.TagType.Name == type && t.ImageTags.Any(it => it.Image.Enabled))
                .OrderBy(t => t.Name);
        }

        public async Task<IActionResult> Tag(int id)
        {
            var tag = await db.Tags.FirstOrDefaultAsync(t => t.ID == id);
            if (tag == null)
                return Redirect("/photo");

            ViewBag.Subtitle = "Photography – " + tag.Name;

            var images = db.Images.Include(i => i.ImageTags)
                .Where(i => i.Enabled && i.ImageTags.Any(it => it.Tag_ID == tag.ID))
                .OrderByDescending(i => i.DateTaken)
                .ToListAsync();

            return await Images(images);
        }

        public async Task<IActionResult> Subject(int id) => await Tag(id);
        public async Task<IActionResult> Location(int id) => await Tag(id);
        public async Task<IActionResult> Medium(int id) => await Tag(id);
        public async Task<IActionResult> Treatment(int id) => await Tag(id);

        public async Task<IActionResult> Image(int id)
        {
            var image = await db.Images
                .Include(i => i.ImageTags)
                .ThenInclude(it => it.Tag)
                .ThenInclude(t => t.TagType)
                .FirstOrDefaultAsync(i => i.ID == Convert.ToInt32(id));

            if (image == null)
                return Redirect("/photo");

            ViewBag.Subtitle = "Photography – " + image.Tag("Subject") + " – " + image.Tag("Location") + " – " + image.DateTaken.ToString("d");
            return View(image);
        }
    }
}