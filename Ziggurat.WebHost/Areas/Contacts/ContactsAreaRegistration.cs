using System.Web.Mvc;

namespace Ziggurat.Web.Areas.Contacts
{
    public class ContactsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Contacts";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Contacts_default",
                "Contacts/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
