using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ziggurat.Contracts.Definition;
using Ziggurat.Definition.Client;
using Ziggurat.Infrastructure;
using Ziggurat.Web.Areas.Configuration.Models;
using Ziggurat.Web.Helpers;

namespace Ziggurat.Web.Areas.Configuration.Controllers
{
    public class FormController : Controller
    {
        private IViewModelReader _viewModelReader;
        private ICommandSender _commandSender;
        
        public FormController(IViewModelReader reader, ICommandSender sender)
        {
            _viewModelReader = reader;
            _commandSender = sender;
        }

        public FormController()
            : this(Client.ViewModelReader, Client.CommandSender) { }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewForm(CreateFormModel model)
        {
            if (!ModelState.IsValid) return Json("");

            Response.StatusCode = 500;
            return Json("Invalid something");

            var currentProject = DomainHelper.GetCurrentProjectDomain(Request);
            var projectInfo = _viewModelReader.LoadOrDefault<string, ProjectInfo>(currentProject);

            if (projectInfo == null) throw new InvalidOperationException("Unknown project");

            var formId = DefinitionIdGenerator.NewFormId();

            var cmd = new CreateForm(projectInfo.ProjectId, formId, model.Name, model.UniqueName);

            return Json("OK");
        }
    }
}
