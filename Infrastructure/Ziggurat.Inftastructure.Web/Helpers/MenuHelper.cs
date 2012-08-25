using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Ziggurat.Inftastructure.Web.Helpers
{
    public static class MenuHelper
    {
        public static MvcHtmlString MenuItem(
            this HtmlHelper htmlHelper,
            string controller, 
            string action,
            string text,
            string id,
            IList<string> cssClasses = null)
        {
            var menu = new TagBuilder("li");
            var currentController = (string)htmlHelper.ViewContext.RouteData.Values["controller"];
            var currentAction = (string)htmlHelper.ViewContext.RouteData.Values["action"];

            if (!string.IsNullOrWhiteSpace(id))
                menu.Attributes.Add("id", id.Trim());

            if (string.Equals(controller, currentController, StringComparison.InvariantCultureIgnoreCase)
                && string.Equals(action, currentAction, StringComparison.InvariantCultureIgnoreCase))
            {
                menu.AddCssClass("active");
            }

            if (cssClasses != null)
                foreach (var css in cssClasses) menu.AddCssClass(css);            
            
            var link = htmlHelper.ActionLink(text, action, controller).ToString();

            menu.InnerHtml = link;
            return MvcHtmlString.Create(menu.ToString());
        }

        public static MvcHtmlString MenuItem(
            this HtmlHelper htmlHelper,
            string controller,
            string action,
            string text,
            IList<string> cssClasses = null)
        {
            return MenuItem(htmlHelper, controller, action, text, null, cssClasses);
        }
    }
}
