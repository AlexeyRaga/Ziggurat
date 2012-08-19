using System;

namespace Ziggurat.Infrastructure
{
    public interface ICommandSender
    {
        void SendCommand(object command);
    }
}
