using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using VTSV.EXIF;
using VTSV.Models;
using VTSV.Properties;

namespace VTSV.Controllers
{
    public sealed class ImagesController : ControllerBase
    {
        public ActionResult Index(string id = "")
        {
            //get the list of images
            var images = index(id);

            //return the list in JSON format
            return Json(images, JsonRequestBehavior.AllowGet);
        }

        private IList<Image> index(string id)
        {
            //get the list of tag IDs currently filtered by
            var tagIDs = !string.IsNullOrEmpty(id)
                ? id.Split(',').Where(i => !string.IsNullOrEmpty(i)).Select(i => Convert.ToInt32(i)).ToList()
                : new List<int>();

            using (var db = new DB())
            {
                //find all images that have all tags specified
                var tags = tagIDs.Any() ? db.Tags.Where(t => tagIDs.Contains(t.ID)) : null;
                var images = tags != null ? db.Images.Where(i => tags.All(tag => i.Tags.Contains(tag))) : db.Images;
                return images.Where(i => i.Enabled).ToList();
            }
        }

        //
        // GET: /Images/List

        [Admin]
        public ActionResult List()
        {
            SetViewBag();
            using (var db = new DB())
            {
                //return a list of all images
                var images = db.Images.Include("Tags").Include("Tags.Type").OrderByDescending(i => i.ID).ToList();
                return View(images);
            }
        }

        //
        // GET: /Images/Details/5

        [Admin]
        public ActionResult Details(int id = 0)
        {
            using (var db = new DB())
            {
                //return admin details of a single image
                var image = db.Images.Find(id);
                if (image == null)
                    return HttpNotFound();
                return View(image);
            }
        }

        //
        // GET: /Images/Create

        [Admin]
        public ActionResult Create()
        {
            //go to form to add a new image to the DB
            SetViewBag();
            return View();
        }

        [Admin]
        void SetViewBag()
        {
            using (var db = new DB())
            {
                //TODO: ViewBag is the worst -- do this better
                ViewBag.TagTypes = db.TagTypes.Include("Tags").Where(t => !t.IsSystem).ToList();
                var path = Path.Combine(Settings.Default.ImagePath, "full");
                ViewBag.AllImages = Directory.GetFiles(path).Select(f => f.Replace(path + "\\", "").Split('.')[0]).Where(f => !db.Images.Select(i => i.Name).Contains(f)).ToList();
            }
        }

        //
        // POST: /Images/Create

        [Admin]
        [HttpPost]
        public ActionResult Create(Image image, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                using (var db = new DB())
                {
                    //model bindings fail for some reason, gotta do it manually
                    updateImage(image, fc, db);

                    //add it to the DB
                    db.Images.Add(image);
                    db.SaveChanges();
                }
                return RedirectToAction("List");
            }

            return View(image);
        }

        private DateTime getDateTaken(string name)
        {
            try
            {
                //get EXIF data from the actual original image file
                var exif = getEXIF(name);

                //pull out the date taken field
                var dateTaken = exif["Date Time"].ToString().Split(' ')[0].Replace(':', '-');
                return DateTime.Parse(dateTaken);
            }
            catch
            {
                return DateTime.Today;
            }
        }

        private EXIFextractor getEXIF(System.Drawing.Bitmap image)
        {
            return new EXIFextractor(ref image, "");
        }

        private EXIFextractor getEXIF(string name)
        {
            //get the image
            var image = getFullImage(name);

            //yoink the EXIF info
            return getEXIF(image);
        }

        private System.Drawing.Bitmap getFullImage(string name)
        {
            //pulls the actual original image file into memory
            return new System.Drawing.Bitmap(Path.Combine(Settings.Default.ImagePath, "full", name + ".jpg"));
        }

        //
        // GET: /Images/Edit/5

        [Admin]
        public ActionResult Edit(int id = 0)
        {
            //for admin purposes only
            using (var db = new DB())
            {
                var image = db.Images.Include("Tags").Include("Tags.Type").Find(id);
                if (image == null)
                    return HttpNotFound();
                SetViewBag();
                return View(image);
            }
        }

        //
        // POST: /Images/Edit/5

        [Admin]
        [HttpPost]
        public ActionResult Edit(Image image, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                using (var db = new DB())
                {
                    updateImage(image, fc, db);
                    db.SaveChanges();
                }
                return RedirectToAction("List");
            }
            return View(image);
        }

        private void updateImage(Image image, NameValueCollection fc, DB db)
        {
            //TODO: this entire function should not exist if model binding were working properly -- what's up with that?
            //probably the many-to-many mappings...
            var existing = db.Images.Include("Tags").Include("Tags.Type").Find(image.ID);
            if (existing != null) existing.DateTaken = image.DateTaken;
            image.Tags = existing != null ? existing.Tags : new List<Tag>();
            var checkedTagIDs = fc.AllKeys.Where(k => k.StartsWith("tag", StringComparison.Ordinal) && fc[k].Contains("true")).Select(t => int.Parse(t.Substring(3)));
            var checkedTags = checkedTagIDs.Select(t => db.Tags.Include("Type").Find(t)).ToList();

            foreach (var tag in checkedTags)
                image.Tags.Add(tag);

            var deadTags = image.Tags.Where(t => !checkedTags.Contains(t)).ToList();
            foreach (var tag in deadTags)
                image.Tags.Remove(tag);

            // ReSharper disable LoopCanBePartlyConvertedToQuery
            foreach (var tag in fc.AllKeys.Where(k => k.StartsWith("new", StringComparison.Ordinal) & fc[k] != ""))
            // ReSharper restore LoopCanBePartlyConvertedToQuery
            {
                var tagType = db.TagTypes.Find(int.Parse(tag.Substring(3)));
                var newTag = new Tag { ID = db.Tags.Max(t => t.ID) + 1, Name = fc[tag], Type = tagType };
                db.Tags.Add(newTag);
                image.Tags.Add(newTag);
            }
        }

        //
        // GET: /Images/Delete/5

        [Admin]
        public ActionResult Delete(int id = 0)
        {
            using (var db = new DB())
            {
                var image = db.Images.Find(id);
                if (image == null)
                    return HttpNotFound();
                return View(image);
            }
        }

        //
        // POST: /Images/Delete/5

        [Admin]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var db = new DB())
            {
                var image = db.Images.Find(id);
                image.Tags.Clear();
                db.Images.Remove(image);
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

        public ActionResult Get(int id, int x, int y)
        {
            //the user requested image [id], and their browser window can handle a resolution up to [x]x[y]
            using (var db = new DB())
            {
                var image = db.Images.Find(id);
                if (image == null)
                    return HttpNotFound();
                return IsFromValidReferrer
                    ? getImage(image.Name, x, y)
                    : new HttpUnauthorizedResult();
            }
        }

        ActionResult getImage(string i, int x, int y)
        {
            //check if the file already exists
            var file = getBestFile(i, x, y);

            //if not, create it
            string fileName = file.ImagePath;
            if (!System.IO.File.Exists(fileName))
                fileName = makeFile(file);

            //return to them the image
            return File(fileName, "image/jpeg");
        }

        private string makeFile(ImageFileInfo file)
        {
            //the fun part

            var i = file.Name;
            var image = getFullImage(i);

            //don't increase the size of the original image, that's just silly
            var x = Math.Min(file.x, image.Width);
            var y = Math.Min(file.y, image.Height);

            //do all the resizing magic
            var newImage = new System.Drawing.Bitmap(x, y);
            var graphics = System.Drawing.Graphics.FromImage(newImage);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(image, 0, 0, x, y);

            //save it to the expected location
            var newFileName = Path.Combine(Settings.Default.ImagePath, "web", i + "-" + x + "x" + y + ".jpg");
            var jpegEncoder = ImageCodecInfo.GetImageEncoders().First(e => e.FormatID == ImageFormat.Jpeg.Guid);
            newImage.Save(newFileName, jpegEncoder, null);

            return newFileName;
        }

        ImageFileInfo getBestFile(string imgName, int x, int y)
        {
            //find the highest resolution file based on the user's browser window
            var max = Math.Max(x, y);
            var min = Math.Min(x, y);
            var windowAspect = Convert.ToDecimal(max) / Convert.ToDecimal(min);

            //check if we've already rendered this for someone before
            var existing = getFileWithWidthOrHeight(imgName, min, max);

            //if so, give them the info for that one
            if (existing != null)
                return existing;

            //otherwise, create some new info
            var image = getFullImage(imgName);
            var imageAspect = Convert.ToDecimal(image.Width) / Convert.ToDecimal(image.Height);

            int imgX, imgY;
            if (imageAspect >= 1)
            {
                //landscape image, use landscape view
                if (imageAspect > windowAspect)
                {
                    //top+bottom letterbox
                    imgX = max;
                    imgY = Convert.ToInt16(max / imageAspect);
                }
                else
                {
                    //left+right letterbox
                    imgY = min;
                    imgX = Convert.ToInt16(imgY * imageAspect);
                }
            }
            else
            {
                //portrait image, use portrait view
                windowAspect = 1.0m / windowAspect;
                if (imageAspect < windowAspect)
                {
                    //left+right letterbox
                    imgY = max;
                    imgX = Convert.ToInt16(imgY * imageAspect);
                }
                else
                {
                    //top+bottom letterbox
                    imgX = min;
                    imgY = Convert.ToInt16(imgX / imageAspect);
                }
            }

            return new ImageFileInfo { Name = imgName, x = imgX, y = imgY };
        }

        private ImageFileInfo getFileWithWidthOrHeight(string imgName, int min, int max)
        {
            //do any images already exist with width=[maxX] or height=[maxY]?
            var path = Path.Combine(Settings.Default.ImagePath, "web");
            var files = Directory.GetFiles(path, imgName + "-*.jpg").Select(f => new ImageFileInfo(f.Replace(path + "\\", "").Replace(".jpg", "")));
            return files.FirstOrDefault(f =>
                (f.x > f.y && (f.x == max || f.y == min))
                || (f.y > f.x && (f.y == max || f.x == min))
                );
        }

        [Admin]
        public ActionResult GetFile(string id, int x = 0, int y = 0)
        {
            return getImage(id, x, y);
        }

        [Admin]
        public ActionResult GetDateTaken(string id)
        {
            return new ContentResult { ContentType = "text/plain", Content = getDateTaken(id).ToShortDateString(), ContentEncoding = Encoding.ASCII };
        }

        public ActionResult Thumb(int id)
        {
            //TODO: try to push this to the client
            using (var db = new DB())
            {
                var image = db.Images.Find(id);
                return PartialView(image);
            }
        }

        public ActionResult Gallery(string id = "")
        {
            //TODO: try to push this to the client
            var images = index(id);
            return PartialView(images);
        }

        public ActionResult Info(int id)
        {
            //TODO: try to push this to the client
            using (var db = new DB())
            {
                var image = db.Images.Include("Tags").Include("Tags.Type").Find(id);
                return PartialView(image);
            }
        }

        public ActionResult Featured(Image featured)
        {
            //TODO: figure out what I want out of this feature (no pun intended)
            return PartialView(featured);
        }
    }
}