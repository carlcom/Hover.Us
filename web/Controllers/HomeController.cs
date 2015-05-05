using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Query;
using Microsoft.Framework.ConfigurationModel;
using Newtonsoft.Json;
using VTSV.Models;
using Image = VTSV.Models.Image;

namespace VTSV.Controllers
{
    public class HomeController : Controller
    {
        private readonly DB db;
        private readonly string imagePath;
        private readonly JsonSerializerSettings jsonSettings;

        public HomeController(IConfiguration configuration)
        {
            db = new DB(configuration);
            imagePath = configuration.Get("Images");
            jsonSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
        }

        [HttpGet("/navbar")]
        public string Navbar()
        {
            var navbar = new Navbar { TagTypes = db.TagTypes.Include(tt => tt.Tags).ThenInclude(t => t.ImageTags) };
            return JsonConvert.SerializeObject(navbar, jsonSettings);
        }

        [HttpGet("/images")]
        public string Images(int id)
        {
            IQueryable<Image> images = db.Images.Include(i => i.ImageTags);
            if (id > 0)
            {
                images = images.Where(i => i.ImageTags.Any(it => it.Tag_ID == id));
            }
            return JsonConvert.SerializeObject(images.OrderByDescending(i => i.DateTaken), jsonSettings);
        }

        [HttpGet("/image")]
        public FilePathResult Image(int id, int x, int y)
        {
            var image = db.Images.First(i => i.ID == id);
            var newPath = image.GetFile(imagePath, new Size(x, y));
            return File(newPath, "image/jpeg");
        }

        [HttpGet("/info")]
        public string Info(int id)
        {
            var image =
                db.Images.Include(i => i.ImageTags)
                    .ThenInclude(it => it.Tag)
                    .ThenInclude(t => t.TagType)
                    .First(i => i.ID == id);
            return JsonConvert.SerializeObject(image, jsonSettings);
        }
    }
}