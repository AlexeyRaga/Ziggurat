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
            var hostParts = helper.RequestContext.HttpContext.Request.Url.Host.Split('.');
            if (hostParts.Length != 3) return String.Empty;

            return hostParts[0];
        }
    }
}