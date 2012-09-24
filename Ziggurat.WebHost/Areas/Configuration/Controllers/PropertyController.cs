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
    [Authorize]
    public class PropertyController : Controller
    {
        private readonly IViewModelReader _viewModelReader;
        private readonly ICommandSender _commandSender;
        public PropertyController(IViewModelReader viewModelReader, ICommandSender commandSender)
        {
            _viewModelReader = viewModelReader;
            _commandSender = commandSender;
        }

        public PropertyController()
            : this(Client.ViewModelReader, Client.CommandSender) { }

        public ActionResult Overview(Guid formId, Guid propertyId)
        {
            var key = PropertyData.CreateKey(formId, propertyId);
            var propData = _viewModelReader.Load<string, PropertyData>(key);

            var viewName = "Overview-" + propData.Type.ToString();

            return View(viewName, propData);
        }

        [HttpPost]
        [OutputCache(Duration = 0)]
        public ActionResult AddNew(NewPropertyModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage)
                    .ToArray();

                Response.StatusCode = 500;
                return Json(errors);
            }

            var currentProject = DomainHelper.GetCurrentProjectDomain(Request);
            var projectInfo = _viewModelReader.LoadOrDefault<string, ProjectInfo>(currentProject);

            var propertyId = DefinitionIdGenerator.NewPropertyId();

            var cmd = new AddNewPropertyToForm(model.FormId, propertyId, model.Type, model.Name);
            _commandSender.SendCommand(cmd);

            return Json(propertyId);
        }

        [HttpPost]
        public void MakeUsed(Guid formId, Guid propertyId)
        {
            var cmd = new MakePropertyUsed(formId, propertyId);
            _commandSender.SendCommand(cmd);
        }

        [HttpPost]
        public void MakeUnused(Guid formId, Guid propertyId)
        {
            var cmd = new MakePropertyUnused(formId, propertyId);
            _commandSender.SendCommand(cmd);
        }
    }
}
