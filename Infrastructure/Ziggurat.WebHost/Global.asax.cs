using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ziggurat.Infrastructure;

namespace Ziggurat.WebHost
{
    public class MvcApplication : System.Web.HttpApplication
    {
	    private IBus _bus;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

			_bus = Client.Setup.SimpleBus.Create();
	        HandlerConfig.RegisterHandlers(_bus);
        }

		protected void Application_End()
		{
			_bus.Dispose();
		}
    }
}