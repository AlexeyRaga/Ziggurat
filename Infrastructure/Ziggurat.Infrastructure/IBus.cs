using System;

namespace Ziggurat.Infrastructure
{
	public interface IBus : IDisposable
	{
		void SubscribeToCommands(object handler);
		void SubscribeToEvents(object handler);

        void SendCommand(object command);
	}
}
