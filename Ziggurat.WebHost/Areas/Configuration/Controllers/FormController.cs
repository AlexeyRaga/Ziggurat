using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Ziggurat.Contracts.Definition;
using Ziggurat.Definition.Client;
using Ziggurat.Infrastructure;
using Ziggurat.Web.Areas.Configuration.Models;
using Ziggurat.Web.Helpers;

namespace Ziggurat.Web.Areas.Configuration.Controllers
{
    [Authorize]
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

        public ActionResult Overview(Guid id)
        {
            var formInfo = _viewModelReader.LoadOrDefault<Guid, FormInfo>(id);
            if (formInfo == null)
            {
                Thread.Sleep(5000);
                formInfo = _viewModelReader.LoadOrDefault<Guid, FormInfo>(id);
            }

            if (formInfo == null) return View("FormIsBeingCreated", id);

            return View(formInfo);
        }

        [HttpGet]
        [OutputCache(Duration = 0)]
        public ActionResult Exists(Guid id)
        {
            var data = _viewModelReader.LoadOrDefault<Guid, ProjectList>(id);

            return Json(data != null, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddNewForm(CreateFormModel model)
        {
            if (!ModelState.IsValid) {
                var errors = ModelState
                    .SelectMany(x => x.Value.Errors)
                    .Select(x=>x.ErrorMessage)
                    .ToArray();

                Response.StatusCode = 500;
                return Json(errors);
            }

            var currentProject = DomainHelper.GetCurrentProjectDomain(Request);
            var projectInfo = _viewModelReader.LoadOrDefault<string, ProjectInfo>(currentProject);

            if (projectInfo == null) throw new InvalidOperationException("Unknown project");

            var formId = DefinitionIdGenerator.NewFormId();

            var cmd = new CreateForm(projectInfo.ProjectId, formId, model.Name, model.UniqueName);
            _commandSender.SendCommand(cmd);

            return Json(formId);
        }
    }
}
