using Ziggurat.Infrastructure;

namespace Ziggurat.Infrastructure
{
    public sealed class ToDispatcherCommandSender : ICommandSender
    {
        private readonly IMessageDispatcher _sender;

        public void SendCommand(object command)
        {
            _sender.DispatchToOneAndOnlyOne(command);
        }

        public ToDispatcherCommandSender(IMessageDispatcher commandSender)
		{
            _sender = commandSender;
		}
    }
}