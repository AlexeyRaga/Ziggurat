using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts.Definition;
using Ziggurat.Infrastructure;

namespace Ziggurat.Definition.Domain.Processes
{
    public sealed class ProjectCreationProcess
    {
        private readonly ICommandSender _commandSender;
        public ProjectCreationProcess(ICommandSender commandSender)
        {
            _commandSender = commandSender;
        }

        public void When(NewProjectRegistered evt)
        {
            var projectLayoutId = DefinitionIdGenerator.NewProjectLayoutId(evt.ProjectId);
            _commandSender.SendCommand(new CreateLayoutForProject(evt.ProjectId, projectLayoutId));
        }

        public void When(ProjectLayoutCreated evt)
        {
            _commandSender.SendCommand(new AssignProjectLayoutToProject(evt.ProjectId, evt.ProjectLayoutId));
        }
    }
}
