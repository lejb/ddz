using System;

namespace Networking.Protocol
{
    public static class ProtocolTransformation
    {
        const int intSize = sizeof(int);

        public static byte[] WithHeader(this IProtocol protocol)
        {
            byte[] stream = protocol.ConvertDataToBytes();
            byte[] bytes = new byte[stream.Length + 2 * intSize];
            Array.Copy(BitConverter.GetBytes((int)protocol.Type), 0, bytes, 0, intSize);
            Array.Copy(BitConverter.GetBytes(stream.Length), 0, bytes, intSize, intSize);
            Array.Copy(stream, 0, bytes, 2 * intSize, stream.Length);
            return bytes;
        }
    }
}
