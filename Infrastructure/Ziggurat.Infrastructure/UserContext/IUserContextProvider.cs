namespace Ziggurat.Infrastructure.UserContext
{
    public interface IUserContextProvider
    {
        IUserContext GetCurrentContext();
    }
}
