using System.Web.Mvc;
using VTSV.Properties;

namespace VTSV.Controllers
{
    public class ControllerBase : Controller
    {
        protected bool IsAdmin
        {
            get { return Request.IsLocal; }
        }

        protected bool IsFromValidReferrer
        {
            get
            {
                return Request.UrlReferrer != null &&
                    (Request.IsLocal || Request.UrlReferrer.Host.EndsWith(Settings.Default.DomainName) || Request.UrlReferrer.Host.StartsWith("192.168.1."));
            }
        }
    }
}
