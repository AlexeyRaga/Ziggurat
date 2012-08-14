namespace Ziggurat.Client.Setup
{
	public interface IClientEndpoint
	{
		void SubscribeToCommands(object handler);
		void SubscribeToEvents(object handler);
	}
}
