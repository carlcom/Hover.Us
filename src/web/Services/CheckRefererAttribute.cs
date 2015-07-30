using System;
using Microsoft.AspNet.Mvc;

namespace web.Services
{
    public class CheckRefererAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(AuthorizationContext context)
        {
            var httpContext = context.HttpContext;
            var httpRequest = httpContext.Request;
            var headerDictionary = httpRequest.Headers;
            var referer = headerDictionary["Referer"];
            if (!(referer.StartsWith("http://stevedesmond.ca") || referer.StartsWith("http://beta.stevedesmond.ca") || referer.StartsWith("http://localhost")))
            {
                throw new Exception();
            }
        }
    }
}