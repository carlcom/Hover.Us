using Microsoft.AspNet.Mvc;

namespace Web.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Steve() => View();
        public IActionResult Resume(string _for) => View();
        public IActionResult Datacenter() => View();
    }
}
