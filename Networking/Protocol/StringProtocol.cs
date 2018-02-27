using System;
using System.Text;

namespace Networking.Protocol
{
    public class StringProtocol : IProtocol
    {
        public string Message { get; private set; }

        public StringProtocol(string msg = "")
        {
            Message = msg;
        }

        public ProtocolType Type => ProtocolType.Test;

        public void ConstructFromBytesData(byte[] bytes, int startOffset, int size)
        {
            byte[] data = new byte[size];
            Array.Copy(bytes, startOffset, data, 0, size);
            Message = Encoding.UTF8.GetString(data);
        }

        public byte[] ConvertDataToBytes()
        {
            return Encoding.UTF8.GetBytes(Message);
        }
    }
}
