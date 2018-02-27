using System;

namespace Networking.Protocol
{
    public class HeaderProtocolInterpreter : StreamInterpreter
    {
        public override event NewMessageEventHandler NewMessage;

        public static HeaderProtocolInterpreter Instance { get; } = new HeaderProtocolInterpreter();

        protected override void OnData()
        {
            if (bufferPointer < 2 * sizeof(int)) return;

            ProtocolType protocolType = (ProtocolType)BitConverter.ToInt32(buffer, 0);
            int dataLength = BitConverter.ToInt32(buffer, sizeof(int));
            int headerLength = 2 * sizeof(int);
            int totalLength = dataLength + headerLength;

            if (bufferPointer >= totalLength)
            {
                IProtocol protocol = protocolType.GetNewInstance();
                protocol.ConstructFromBytesData(buffer, headerLength, dataLength);
                NewMessage?.Invoke(protocol);
                Eat(totalLength);
            //    Console.WriteLine($"protocol {protocolType} generated, {bufferPointer} bytes left");
                OnData();
            }
        }
    }
}
