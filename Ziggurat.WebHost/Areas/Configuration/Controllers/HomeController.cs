using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ziggurat.Definition.Client;
using Ziggurat.Web.Helpers;

namespace Ziggurat.Web.Areas.Configuration.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IViewModelReader _viewModelReader;
        public HomeController(IViewModelReader reader)
        {
            _viewModelReader = reader;
        }

        public HomeController() : this(Client.ViewModelReader) { }

        public ActionResult Index()
        {
            var currentProject = DomainHelper.GetCurrentProjectDomain(Request);

            var overview = _viewModelReader.LoadOrDefault<string, ProjectOverview>(currentProject);
            if (overview == null)
                return HttpNotFound(String.Format("Project {0} does not exist", currentProject));

            return View(overview);
        }

    }
}
