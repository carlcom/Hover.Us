using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Microsoft.Framework.Configuration;
using Web.Models;

namespace Web.Controllers
{
    [Route("photo")]
    public class PhotoController : Controller
    {
        readonly DB db;

        public PhotoController(IConfiguration configuration)
        {
            db = new DB(configuration);
        }

        public IActionResult Index()
        {
            return View(db.Images
                .Where(i => i.Enabled)
                .OrderByDescending(i => i.DateTaken));
        }

        [HttpGet("view")]
        public IActionResult View(int id)
        {
            var image = db.Images
                .Include(i => i.ImageTags)
                .ThenInclude(it => it.Tag)
                .ThenInclude(t => t.TagType)
                .First(i => i.ID == id);
            return View(image);
        }
    }
}