using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ziggurat.Contracts.Definition;
using Ziggurat.Definition.Client;
using Ziggurat.Infrastructure;
using Ziggurat.Web.Areas.Configuration.Models;

namespace Ziggurat.Web.Areas.Configuration.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly IViewModelReader _modelReader;
        private readonly ICommandSender _commandSender;

        public ProjectController(IViewModelReader modelReader, ICommandSender commandSender)
        {
            _modelReader = modelReader;
            _commandSender = commandSender;
        }

        public ProjectController()
            : this(Client.ViewModelReader, Client.CommandSender) { }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(CreateProjectModel model)
        {
            if (!ModelState.IsValid) return View();
            if (!ValidateUniqueShortName(model.ShortName))
            {
                ModelState.AddModelError("", String.Format("Short name {0} is already taken.", model.ShortName));
                return View();
            }

            var newProjectId = DefinitionIdGenerator.NewProjectId();
            var cmd = new CreateNewProject(newProjectId, model.Name, model.ShortName);

            _commandSender.SendCommand(cmd);

            return View("ProjectIsBeingCreated", newProjectId);

        }

        [HttpGet]
        [OutputCache(Duration=0)]
        public ActionResult Exists(Guid id)
        {
            var projectExists = false;
            var list = _modelReader.LoadOrDefault<string, ProjectList>("all");
            
            if (list != null) 
                projectExists = list.Projects.Any(x => x.Id == id);

            return Json(projectExists, JsonRequestBehavior.AllowGet);
        }

        private bool ValidateUniqueShortName(string shortName)
        {
            var list = _modelReader.LoadOrDefault<string, ProjectList>("all");
            if (list == null) return true; //no project at all => unique

            return !list.Projects
                .Any(x => String.Equals(x.ShortName, shortName, StringComparison.InvariantCultureIgnoreCase));
        }

        public ActionResult Configure(Guid id)
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Switch(string id)
        {
            var currentHost = Request.Url.Host;
            var twoLastParts = currentHost.Split('.').Reverse().Take(2).Reverse();
            var baseDomain = String.Join(".", twoLastParts);
            var newHost = id + "." + baseDomain;

            var urlToGo = (Request.UrlReferrer != null)
                ? Request.UrlReferrer.ToString().Replace(currentHost, newHost)
                : "http://" + newHost;

            return Redirect(urlToGo);
        }

        [ChildActionOnly]
        public ActionResult List()
        {
            var list = _modelReader.LoadOrDefault<string, ProjectList>("all") ?? new ProjectList();

            return PartialView(list);
        }
    }
}
