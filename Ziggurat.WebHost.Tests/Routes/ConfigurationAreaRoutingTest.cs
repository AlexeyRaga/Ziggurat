using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ziggurat.Web;
using Ziggurat.Web.Areas.Configuration;

namespace Ziggurat.WebHost.Tests.Routes
{
    [TestClass]
    public class ConfigurationAreaRoutingTest
    {
        [TestMethod]
        public void Configuration_property_incoming_route()
        {
            //TODO: LEISHI extra to the base
            var routes = new RouteCollection();
            //TODO: LEISHI all the areas should be registered otherwise the route testing is kind of useless as 
            var areaRegistration = new ConfigurationAreaRegistration();
            Assert.AreEqual("Configuration", areaRegistration.AreaName);

            var areaRegistrationContext = new AreaRegistrationContext(areaRegistration.AreaName, routes);
            areaRegistration.RegisterArea(areaRegistrationContext);

            RouteConfig.RegisterRoutes(routes);

            var formId = "af3c7b50-fadd-407c-b28a-0fab60be264c";
            var propertyId = "2e9939df-4462-4717-a5dc-a174a29d11c1";

            var context = new Mock<HttpContextBase>();
            context.Setup(c => c.Request.AppRelativeCurrentExecutionFilePath)
                .Returns("~/configuration/form/" + formId + "/p/" + propertyId);

            var routeData = routes.GetRouteData(context.Object);

            Assert.IsNotNull(routeData, "Should have found the route");
            Assert.AreEqual("Configuration", routeData.DataTokens["area"]);
            Assert.AreEqual("Property", routeData.Values["controller"]);
            Assert.AreEqual("Overview", routeData.Values["action"]);
            Assert.AreEqual(formId, routeData.Values["formId"]);
            Assert.AreEqual(propertyId, routeData.Values["propertyId"]);
        }
    }
}
