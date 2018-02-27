using System.Net;
using Networking.Protocol;

namespace Networking.Core
{
    public delegate void ReadProtocolEventHandler(IProtocol protocol);

    public class ProtocolBasedClient : LocalClient
    {
        private HeaderProtocolInterpreter interpreter = HeaderProtocolInterpreter.Instance;

        public event ReadProtocolEventHandler ReadProtocol;

        public ProtocolBasedClient()
        {
            ReadData += OnReadData;
            interpreter.NewMessage += OnNewMessage;
            interpreter.BufferOverflow += OnBufferOverflow;
        }

        public void SendProtocol(IProtocol protocol)
        {
            byte[] bytes = protocol.WithHeader();
            Send(bytes, bytes.Length);
        }

        private void OnReadData(EndPoint endPoint, byte[] data, int size)
        {
            interpreter.AddToBuffer(data, 0, size);
        }

        private void OnNewMessage(IProtocol protocol)
        {
            ReadProtocol?.Invoke(protocol);
        }

        private void OnBufferOverflow()
        {
            throw new System.Exception("DDZ-Buffer Overflow");
        }
    }
}
