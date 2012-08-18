using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Ziggurat.Client.Setup;
using Ziggurat.Infrastructure;
using Ziggurat.Infrastructure.Evel;
using Ziggurat.Infrastructure.EventStore;
using Ziggurat.Infrastructure.Projections;

namespace Ziggurat.WebHost
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