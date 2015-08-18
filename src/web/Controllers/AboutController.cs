using Microsoft.AspNet.Mvc;

namespace web.Controllers
{
    [Route("about")]
    public class AboutController : Controller
    {
        public IActionResult Index() => View();

        [HttpGet("steve")]
        public IActionResult Steve() => View();

        [HttpGet("resume")]
        public IActionResult Resume(string _for) => View();

        [HttpGet("datacenter")]
        public IActionResult Datacenter() => View();
    }
}
