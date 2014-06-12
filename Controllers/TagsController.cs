using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using VTSV.Models;

namespace VTSV.Controllers
{
    public sealed class TagsController : ControllerBase
    {
        //
        // GET: /Tags/List

        [Admin]
        public ActionResult List()
        {
            using (var db = new DB())
            {
                return View(db.Tags.ToList());
            }
        }

        //
        // GET: /Tags/Details/5

        [Admin]
        public ActionResult Details(int id = 0)
        {
            using (var db = new DB())
            {
                var tag = db.Tags.Find(id);
                if (tag == null)
                    return HttpNotFound();
                return View(tag);
            }
        }

        //
        // GET: /Tags/Create

        [Admin]
        public ActionResult Create()
        {
            using (var db = new DB())
            {
                ViewBag.TagTypes = db.TagTypes.ToList();
                return View(new Tag());
            }
        }

        //
        // POST: /Tags/Create

        [Admin]
        [HttpPost]
        public ActionResult Create(Tag tag, FormCollection fc)
        {
            var id = int.Parse(fc["TagType"]);
            using (var db = new DB())
            {
                tag.Type = db.TagTypes.First(t => t.ID == id);
                db.Tags.Add(tag);
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

        //
        // GET: /Tags/Edit/5

        [Admin]
        public ActionResult Edit(int id = 0)
        {
            using (var db = new DB())
            {
                var tag = db.Tags.Find(id);
                if (tag == null)
                    return HttpNotFound();
                ViewBag.TagTypes = db.TagTypes;
                return View(tag);
            }
        }

        //
        // POST: /Tags/Edit/5

        [Admin]
        [HttpPost]
        public ActionResult Edit(Tag tag, FormCollection fc)
        {
            var id = int.Parse(fc["TagType"]);
            using (var db = new DB())
            {
                tag.Type = db.TagTypes.First(t => t.ID == id);
                db.Entry(tag).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

        //
        // GET: /Tags/Delete/5

        [Admin]
        public ActionResult Delete(int id = 0)
        {
            using (var db = new DB())
            {
                var tag = db.Tags.Find(id);
                if (tag == null)
                    return HttpNotFound();
                return View(tag);
            }
        }

        //
        // POST: /Tags/Delete/5

        [Admin]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var db = new DB())
            {
                var tag = db.Tags.Find(id);
                tag.Images.Clear();
                db.Tags.Remove(tag);
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

        public ActionResult Navbar(IList<Image> images)
        {
            return PartialView(images);
        }
    }
}