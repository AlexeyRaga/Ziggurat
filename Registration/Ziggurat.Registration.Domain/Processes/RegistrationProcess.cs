using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts.Registration;
using Ziggurat.Infrastructure;

namespace Ziggurat.Registration.Domain.Processes
{
    public sealed class RegistrationProcess
    {
        private readonly ICommandSender _commandSender;
        public RegistrationProcess(ICommandSender commandSender)
        {
            if (commandSender == null) throw new ArgumentNullException("commandSender");
            _commandSender = commandSender;
        }

        public void When(RegistrationCreated evt)
        {
            
        }
    }
}
