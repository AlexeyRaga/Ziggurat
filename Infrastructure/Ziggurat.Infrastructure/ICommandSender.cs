using System;

namespace Ziggurat.Infrastructure
{
    /// <summary>
    /// Can send commands
    /// </summary>
    public interface ICommandSender
    {
        void SendCommand(object command);
    }
}
