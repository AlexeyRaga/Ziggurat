using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ziggurat.Web;
using Ziggurat.Web.Areas.Account;
using Ziggurat.Web.Areas.Configuration;
using Ziggurat.Web.Areas.Contacts;
using Ziggurat.Web.Areas.Forms;

namespace Ziggurat.WebHost.Tests.Routes
{
    [TestClass]
    public class RoutingTestBase
    {
        protected RouteCollection Routes = new RouteCollection();

        [TestInitialize]
        public void RegisterRoutes()
        {
            var areaRegistrations = GetAllKnownAreas();
            foreach (var areaRegistration in areaRegistrations)
            {
                var areaRegistrationContext = new AreaRegistrationContext(areaRegistration.AreaName, Routes);
                areaRegistration.RegisterArea(areaRegistrationContext);
            }

            RouteConfig.RegisterRoutes(Routes);
        }

        private IEnumerable<AreaRegistration> GetAllKnownAreas()
        {
            //NOTE: The order of the area registration is important as the first match first return
            yield return new AccountAreaRegistration();
            yield return new ConfigurationAreaRegistration();
            yield return new ContactsAreaRegistration();
            yield return new FormsAreaRegistration();
        }
    }
}
