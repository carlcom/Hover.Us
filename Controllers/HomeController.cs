using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using VTSV.Models;

namespace VTSV.Controllers
{
    public sealed class HomeController : ControllerBase
    {
        //
        // GET: /Home/

        public ActionResult Index(string t)
        {
            using (var db = new DB())
            {
                var images = new List<Image>();
                var selectedTags = new List<int>();
                if (string.IsNullOrEmpty(t))
                {
                    var featuredImage = db.Images.Include("Tags").Include("Tags.Type").Where(i => i.DateFeatured != null).OrderByDescending(i => i.DateFeatured).FirstOrDefault();
                    if (featuredImage != null) images.Add(featuredImage);
                }
                else
                    selectedTags = t.Split(',').Select(int.Parse).ToList();

                if (images.Any())
                    ViewBag.Featured = true;
                else
                    images = db.Images.Include("Tags").Include("Tags.Type").Where(i => i.Enabled && i.Tags.Any(tag => string.IsNullOrEmpty(t) || selectedTags.Contains(tag.ID))).OrderByDescending(i => i.DateTaken).ToList();

                return View(images);
            }
        }
    }
}
