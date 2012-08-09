namespace Ziggurat.Contracts
{
    public interface IMessage { }
    public interface ICommand : IMessage { }
    public interface IEvent : IMessage { }

    public interface IPropertyEvent : IEvent { }
}
