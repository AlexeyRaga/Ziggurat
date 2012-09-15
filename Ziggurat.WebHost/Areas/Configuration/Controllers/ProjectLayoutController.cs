using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ziggurat.Contracts.Definition;
using Ziggurat.Infrastructure;
using Ziggurat.Definition.Client;
using Ziggurat.Web.Areas.Configuration.Models;
using Ziggurat.Web.Helpers;

namespace Ziggurat.Web.Areas.Configuration.Controllers
{
    [Authorize]
    public class ProjectLayoutController : Controller
    {
        private IViewModelReader _viewModelReader;
        private ICommandSender _commandSender;

        public ProjectLayoutController(IViewModelReader viewModelReader, ICommandSender commandSender)
        {
            _viewModelReader = viewModelReader;
            _commandSender = commandSender;
        }

        public ProjectLayoutController()
            : this(Client.ViewModelReader, Client.CommandSender) { }

        [HttpPost]
        public void ChangeFormPosition(FormPositionInLayout model)
        {
            var projectInfo = GetCurrentProjectInfo();

            var cmd = new MoveFormInProjectLayout(projectInfo.ProjectLayoutId, model.FormId, model.Header, model.Position);
            _commandSender.SendCommand(cmd);
        }

        [ChildActionOnly]
        public ActionResult FormList()
        {
            var projectInfo = GetCurrentProjectInfo();
            var layout = _viewModelReader.Load<Guid, FormsProjectLayout>(projectInfo.ProjectLayoutId);
            var forms = _viewModelReader.LoadOrDefault<Guid, FormsInProject>(projectInfo.ProjectId);

            var model = new ProjectFormsListModel();

            foreach (var rawGroup in layout.BlockHeaderForms)
            {
                var header = rawGroup.Key;
                var formsData = forms.Forms
                    .Where(x => rawGroup.Value.Contains(x.FormId))
                    .Select(x => new ProjectFormsListModel.Form(x.FormId, x.Name));

                var group = new ProjectFormsListModel.Group(header, formsData);
                model.FormGroups.Add(group);
            }

            return PartialView("_ProjectFormsList", model);
        }

        private ProjectInfo GetCurrentProjectInfo()
        {
            var projectDomainName = DomainHelper.GetCurrentProjectDomain(Request);
            var projectInfo = _viewModelReader.Load<string, ProjectInfo>(projectDomainName);
            return projectInfo;
        }
    }
}
