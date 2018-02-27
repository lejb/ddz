using Networking.Protocol;

namespace ProtocolTests
{
    public abstract class ProtocolTestBase<TProtocol> where TProtocol : IProtocol, new()
    {
        protected void Test(TProtocol p)
        {
            byte[] bytes = p.ConvertDataToBytes();
            TProtocol protocol = new TProtocol();
            protocol.ConstructFromBytesData(bytes, 0, bytes.Length);
            TestEqual(p, protocol);
        }

        protected abstract void TestEqual(TProtocol p1, TProtocol p2);
    }
}
