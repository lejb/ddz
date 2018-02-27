namespace Networking.Protocol
{
    public interface IProtocol
    {
        ProtocolType Type { get; }

        byte[] ConvertDataToBytes();

        void ConstructFromBytesData(byte[] bytes, int startOffset, int size);
    }
}
