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
            if (!(referer.StartsWith("http://localhost") || referer.StartsWith("http://stevedesmond.ca")))
            {
                throw new Exception();
            }
        }
    }
}