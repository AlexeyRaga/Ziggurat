using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ziggurat.Definition.Client;
using Ziggurat.Web.Helpers;

namespace Ziggurat.Web
{
    public interface IRequireProjectContext
    {
        ProjectInfo ProjectContext { get; set; }
    }

    public sealed class ProjectContextAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var requireProjectContextController = filterContext.Controller as IRequireProjectContext;
            if (requireProjectContextController == null) return;

            //do nothing if already set
            if (requireProjectContextController.ProjectContext != null) return;

            var currentProject = DomainHelper.GetCurrentProjectDomain(filterContext.RequestContext.HttpContext.Request);
            if (currentProject == null) return;

            var projectInfo = Client.ViewModelReader.LoadOrDefault<string, ProjectInfo>(currentProject);

            requireProjectContextController.ProjectContext = projectInfo;
        }
    }
}