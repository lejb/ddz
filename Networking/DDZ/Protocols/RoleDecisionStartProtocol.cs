using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logic.Core;
using GameFlow.Core;
using Networking.Protocol;

namespace Networking.DDZ.Protocols
{
    public class RoleDecisionStartProtocol : IDDZGameFlowProtocols
    {
        public PlayerID Starter { get; private set; }

        public RoleDecisionStartProtocol(PlayerID starter) => Starter = starter;

        public RoleDecisionStartProtocol() { }

        public ProtocolType Type => ProtocolType.DDZ_RoleDecisionStart;

        public void ConstructFromBytesData(byte[] bytes, int startOffset, int size)
        {
            Starter = (PlayerID)BitConverter.ToInt32(bytes, startOffset);
        }

        public byte[] ConvertDataToBytes()
        {
            return BitConverter.GetBytes((int)Starter);
        }
    }
}
