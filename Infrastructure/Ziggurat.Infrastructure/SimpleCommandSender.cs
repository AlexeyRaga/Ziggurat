using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Ziggurat.Infrastructure;
using Ziggurat.Infrastructure.Evel;

namespace Ziggurat.Infrastructure
{
    public sealed class SimpleCommandSender : ICommandSender
    {
        private readonly IMessageDispatcher _sender;

        public void SendCommand(object command)
        {
            _sender.DispatchToOneAndOnlyOne(command);
        }

        public SimpleCommandSender(IMessageDispatcher commandSender)
		{
            _sender = commandSender;
		}
    }
}