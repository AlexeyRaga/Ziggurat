using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Ziggurat.Web.Helpers
{
    public static class MenuHelper
    {
        public static MvcHtmlString MenuItemForPath(
            this HtmlHelper htmlHelper,
            string path,
            string text,
            IList<string> cssClasses = null)
        {
            return MenuItemForPath(htmlHelper, path, text, null, cssClasses);
        }

        public static MvcHtmlString MenuItemForPath(
            this HtmlHelper htmlHelper,
            string path,
            string text,
            string id,
            IList<string> cssClasses = null)
        {
            var pathElements = path.Split(new[] { '/', '\\' }, 3);

            switch (pathElements.Length)
            {
                case 3:
                    return MenuItem(htmlHelper,
                        pathElements[0], //area
                        pathElements[1], //controller
                        pathElements[2], //action
                        text, id, cssClasses);
                case 2:
                    return MenuItem(htmlHelper,
                        null, //area
                        pathElements[0], //controller
                        pathElements[1], //action
                        text, id, cssClasses);
                case 1:
                    return MenuItem(htmlHelper,
                        null, //area
                        null, //controller
                        pathElements[0], //action
                        text, id, cssClasses);
                default:
                    return MenuItem(htmlHelper,
                        null, //area
                        null, //controller
                        path, //action
                        text, id, cssClasses);
            }
        }

        public static MvcHtmlString MenuItem(
            this HtmlHelper htmlHelper,
            string area,
            string controller, 
            string action,
            string text,
            string id,
            IList<string> cssClasses = null)
        {
            var menu = new TagBuilder("li");
            var currentController = (string)htmlHelper.ViewContext.RouteData.Values["controller"];
            var currentAction = (string)htmlHelper.ViewContext.RouteData.Values["action"];
            var currentArea = (string)htmlHelper.ViewContext.RouteData.DataTokens["area"];

            if (!string.IsNullOrWhiteSpace(id))
                menu.Attributes.Add("id", id.Trim());

            if (string.Equals(controller, currentController, StringComparison.InvariantCultureIgnoreCase)
                && string.Equals(action, currentAction, StringComparison.InvariantCultureIgnoreCase)
                && string.Equals(area, currentArea, StringComparison.InvariantCultureIgnoreCase))
            {
                menu.AddCssClass("active");
            }

            if (cssClasses != null)
                foreach (var css in cssClasses) menu.AddCssClass(css);

            var routeValues = area == null
                ? null
                : new { area = area };

            var link = htmlHelper.ActionLink(text, action, controller, routeValues, null).ToString();

            menu.InnerHtml = link;
            return MvcHtmlString.Create(menu.ToString());
        }

        public static MvcHtmlString MenuItem(
            this HtmlHelper htmlHelper,
            string area,
            string controller,
            string action,
            string text,
            IList<string> cssClasses = null)
        {
            return MenuItem(htmlHelper, area, controller, action, text, null, cssClasses);
        }

        public static MvcHtmlString MenuItem(
            this HtmlHelper htmlHelper,
            string controller,
            string action,
            string text,
            IList<string> cssClasses = null)
        {
            return MenuItem(htmlHelper, null, controller, action, text, null, cssClasses);
        }
    }
}
