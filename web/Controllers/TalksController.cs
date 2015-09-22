using Microsoft.AspNet.Mvc;

namespace Web.Controllers
{
    public class TalksController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Subtitle = "Talks";
            return View();
        }
    }
}
