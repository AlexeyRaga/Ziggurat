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

        public void When(ProjectCreated evt)
        {
            _commandSender.SendCommand(new CreateProjectLayout(evt.Id, new Guid()));
        }
    }
}
