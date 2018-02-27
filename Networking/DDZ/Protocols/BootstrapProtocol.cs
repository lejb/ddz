using System;
using GameFlow.Core;
using Networking.Protocol;

namespace Networking.DDZ.Protocols
{
    public class BootstrapProtocol : IDDZGameFlowProtocols
    {
        public PlayerID GivenID { get; private set; }

        public BootstrapProtocol(PlayerID givenID) => GivenID = givenID;

        public BootstrapProtocol() { }

        public ProtocolType Type => ProtocolType.DDZ_Bootstrap;

        public void ConstructFromBytesData(byte[] bytes, int startOffset, int size)
        {
            GivenID = (PlayerID)BitConverter.ToInt32(bytes, startOffset);
        }

        public byte[] ConvertDataToBytes()
        {
            return BitConverter.GetBytes((int)GivenID);
        }
    }
}
