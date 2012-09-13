using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts.Definition;
using Ziggurat.Infrastructure;

namespace Ziggurat.Definition.Domain.Processes
{
    public sealed class FormCreationProcess
    {
        private readonly ICommandSender _commandSender;
        public FormCreationProcess(ICommandSender commandSender)
        {
            _commandSender = commandSender;
        }

        public void When(FormCreated evt)
        {
            var projectLayoutId = DefinitionIdGenerator.NewProjectLayoutId(evt.ProjectId);
            _commandSender.SendCommand(new AttachFormToProjectLayout(projectLayoutId, evt.FormId, evt.ProjectId));
        }
    }
}
