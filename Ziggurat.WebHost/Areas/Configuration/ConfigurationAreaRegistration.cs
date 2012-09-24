using System.Web.Mvc;

namespace Ziggurat.Web.Areas.Configuration
{
    public class ConfigurationAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Configuration";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //context.MapRoute(
            //    "Configuration_Index",
            //    "Configuration/{controller}/{id}",
            //    new { action = "Overview" });

            context.MapRoute(
                "Configuration_Property",
                "Configuration/Form/{formId}/p/{propertyId}",
                new { controller = "Property", action = "Overview" });

            context.MapRoute(
                "Configuration_default",
                "Configuration/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
