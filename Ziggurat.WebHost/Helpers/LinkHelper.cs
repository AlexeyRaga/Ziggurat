using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Ziggurat.Web.Helpers
{
    public static class LinkHelper
    {
        public static string ActionUrl(this UrlHelper helper, string area, string controller, string action)
        {
            var routeValues = area == null ? null : new { area = area };
            return helper.Action(action, controller, routeValues);
        }

        public static string ActionUrlForPath(this UrlHelper helper, string path)
        {
            var pathElements = path.Split(new[] { '/', '\\' }, 3);

            switch (pathElements.Length)
            {
                case 3:
                    return ActionUrl(helper,
                        pathElements[0], //area
                        pathElements[1], //controller
                        pathElements[2] //action
                        );
                case 2:
                    return ActionUrl(helper,
                        null, //area
                        pathElements[0], //controller
                        pathElements[1] //action
                        );
                case 1:
                    return ActionUrl(helper,
                        null, //area
                        null, //controller
                        pathElements[0] //action
                        );
                default:
                    return ActionUrl(helper,
                        null, //area
                        null, //controller
                        path //action
                        );
            }
        }
    }
}