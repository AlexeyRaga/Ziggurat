using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Ziggurat.WebHost
{
    public class MvcApplication : System.Web.HttpApplication
    {
	    private Client.Setup.SimpleBus _client;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

			_client = Client.Setup.SimpleBus.Create();
	        HandlerConfig.RegisterHandlers(_client);
        }

		protected void Application_End()
		{
			_client.Dispose();
		}
    }
}