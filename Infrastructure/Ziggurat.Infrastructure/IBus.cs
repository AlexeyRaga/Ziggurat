using System;

namespace Ziggurat.Infrastructure
{
    public interface ICommandSender
    {
        void SendCommand(object command);
    }

	public interface IBus : ICommandSender, IDisposable
	{
		void SubscribeToCommands(object handler);
		void SubscribeToEvents(object handler);
	}
}
