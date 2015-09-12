using System.Drawing;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Microsoft.Framework.Configuration;
using Newtonsoft.Json;
using web.Models;

namespace web.Controllers
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
            return View(db.Images);
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