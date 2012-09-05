using Ziggurat.Infrastructure;

namespace Ziggurat.Infrastructure
{
    /// <summary>
    /// Routes commands to the specified dispatcher.
    /// NOT SURE IF WE NEED IT AT ALL. Commands should be just routed to queues, shouldn't they?
    /// </summary>
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