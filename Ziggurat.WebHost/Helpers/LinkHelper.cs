using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Ziggurat.Web.Helpers
{
    public static class LinkHelper
    {
        public static string ActionUrl(this UrlHelper helper, string area, string controller, string action, object routeValues = null)
        {
            var rVals = routeValues == null ? new RouteValueDictionary() : new RouteValueDictionary(routeValues);
            if (area != null) rVals["area"] = area;

            return helper.Action(action, controller, rVals);
        }

        public static string ActionUrlForPath(this UrlHelper helper, string path, object routeValues = null)
        {
            var pathElements = path.Split(new[] { '/', '\\' }, 3);

            switch (pathElements.Length)
            {
                case 3:
                    return ActionUrl(helper,
                        pathElements[0], //area
                        pathElements[1], //controller
                        pathElements[2], //action
                        routeValues
                        );
                case 2:
                    return ActionUrl(helper,
                        null, //area
                        pathElements[0], //controller
                        pathElements[1], //action
                        routeValues
                        );
                case 1:
                    return ActionUrl(helper,
                        null, //area
                        null, //controller
                        pathElements[0], //action
                        routeValues
                        );
                default:
                    return ActionUrl(helper,
                        null, //area
                        null, //controller
                        path, //action
                        routeValues);
            }
        }
    }
}