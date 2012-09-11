using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ziggurat.Web.Helpers
{
    public static class DomainHelper
    {
        public static string GetCurrentProjectDomain(this UrlHelper helper)
        {
            return GetCurrentProjectDomain(helper.RequestContext.HttpContext.Request);
        }

        public static string GetCurrentProjectDomain(this HttpRequestBase request)
        {
            var hostParts = request.Url.Host.Split('.');
            if (hostParts.Length != 3) return String.Empty;

            return hostParts[0];
        }
    }
}