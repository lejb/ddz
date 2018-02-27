namespace Networking.Protocol
{
    public interface IProtocolHandler
    {
        void ProcessProtocol(IProtocol protocol);
    }
}
