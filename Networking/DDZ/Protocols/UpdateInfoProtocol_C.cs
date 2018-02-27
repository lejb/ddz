using GameFlow.Core;
using Networking.Protocol;

namespace Networking.DDZ.Protocols
{
    public class UpdateInfoProtocol_C : IDDZGameFlowProtocols
    {
        public IPlayerForInfo Info { get; private set; }

        public UpdateInfoProtocol_C(IPlayerForInfo info) => Info = info;

        public UpdateInfoProtocol_C() { }

        public ProtocolType Type => ProtocolType.DDZ_UpdateInfo_C;

        public void ConstructFromBytesData(byte[] bytes, int startOffset, int size)
        {
            var info = new PlayerInfo();
            info.FromBytes(bytes, startOffset, size);
            Info = info;
        }

        public byte[] ConvertDataToBytes()
        {
            var info = new PlayerInfo(Info);
            return info.ToBytes();
        }
    }
}
