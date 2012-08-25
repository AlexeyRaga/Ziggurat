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
            _commandSender.SendCommand(new AddFormToProject(evt.ProjectId, evt.FormId));
        }

        public void When(FormAddedToProject evt)
        {
            _commandSender.SendCommand(new AttachFormToProjectLayout(evt.FormId, evt.ProjectId, evt.ProjectLayoutId));
        }
    }
}
