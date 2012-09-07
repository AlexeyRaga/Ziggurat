using System.Web.Mvc;

namespace Ziggurat.Web.Areas.Forms
{
    public class FormsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Forms";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Forms_default",
                "Forms/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
