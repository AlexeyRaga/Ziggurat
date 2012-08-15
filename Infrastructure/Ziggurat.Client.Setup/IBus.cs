namespace Ziggurat.Client.Setup
{
	public interface IBus
	{
		void SubscribeToCommands(object handler);
		void SubscribeToEvents(object handler);

        void SendCommand(object command);
	}
}
