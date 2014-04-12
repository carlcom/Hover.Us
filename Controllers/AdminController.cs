using System.Web.Mvc;
using VTSV.Models;

namespace VTSV.Controllers
{
    public sealed class AdminController : ControllerBase
    {
        //
        // GET: /Admin/
        [Admin]
        public ActionResult Index()
        {
            using (var db = new DB())
            {
                return IsAdmin ? View(db) : null;
            }
        }
    }
}
