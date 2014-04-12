using System.Web.Mvc;

namespace VTSV.Controllers
{
    public sealed class AdminAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!filterContext.RequestContext.HttpContext.Request.IsLocal)
                throw new HttpAntiForgeryException();
        }
    }
}