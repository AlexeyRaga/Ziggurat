using System.Web;
using System.Web.Mvc;

namespace Ziggurat.Configuration.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}