using System.Data;
using System.Linq;
using System.Web.Mvc;
using VTSV.Models;

namespace VTSV.Controllers
{
    public sealed class TagTypesController : ControllerBase
    {
        //
        // GET: /TagTypes/List

        [Admin]
        public ActionResult List()
        {
            using (var db = new DB())
            {
                return View(db.TagTypes.ToList());
            }
        }

        //
        // GET: /TagTypes/Details/5

        [Admin]
        public ActionResult Details(int id = 0)
        {
            using (var db = new DB())
            {
                var tagtype = db.TagTypes.Find(id);
                if (tagtype == null)
                    return HttpNotFound();
                return View(tagtype);
            }
        }

        //
        // GET: /TagTypes/Create

        [Admin]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /TagTypes/Create

        [Admin]
        [HttpPost]
        public ActionResult Create(TagType tagtype)
        {
            if (ModelState.IsValid)
            {
                using (var db = new DB())
                {
                    db.TagTypes.Add(tagtype);
                    db.SaveChanges();
                }
                return RedirectToAction("List");
            }

            return View(tagtype);
        }

        //
        // GET: /TagTypes/Edit/5

        [Admin]
        public ActionResult Edit(int id = 0)
        {
            using (var db = new DB())
            {
                var tagtype = db.TagTypes.Find(id);
                if (tagtype == null)
                    return HttpNotFound();
                return View(tagtype);
            }
        }

        //
        // POST: /TagTypes/Edit/5

        [Admin]
        [HttpPost]
        public ActionResult Edit(TagType tagtype)
        {
            if (ModelState.IsValid)
            {
                using (var db = new DB())
                {
                    db.Entry(tagtype).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("List");
            }
            return View(tagtype);
        }

        //
        // GET: /TagTypes/Delete/5

        [Admin]
        public ActionResult Delete(int id = 0)
        {
            using (var db = new DB())
            {
                var tagtype = db.TagTypes.Find(id);
                if (tagtype == null)
                    return HttpNotFound();
                return View(tagtype);
            }
        }

        //
        // POST: /TagTypes/Delete/5

        [Admin]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var db = new DB())
            {
                var tagtype = db.TagTypes.Find(id);
                db.TagTypes.Remove(tagtype);
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }
    }
}