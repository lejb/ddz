using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logic.Core;
using GameFlow.Core;
using Networking.Protocol;

namespace Networking.DDZ.Protocols
{
    public class RoleDecisionProtocol : IDDZGameFlowProtocols
    {
        public PlayerID PlayerID { get; private set; }

        public int Level { get; private set; }

        public RoleDecisionProtocol(PlayerID starter, int level)
        {
            PlayerID = starter;
            Level = level;
        }

        public RoleDecisionProtocol() { }

        public ProtocolType Type => ProtocolType.DDZ_RoleDecision;

        public void ConstructFromBytesData(byte[] bytes, int startOffset, int size)
        {
            PlayerID = (PlayerID)BitConverter.ToInt32(bytes, startOffset);
            Level = BitConverter.ToInt32(bytes, startOffset + sizeof(int));
        }

        public byte[] ConvertDataToBytes()
        {
            byte[] data = new byte[sizeof(int) * 2];
            Array.Copy(BitConverter.GetBytes((int)PlayerID), 0, data, 0, sizeof(int));
            Array.Copy(BitConverter.GetBytes(Level), 0, data, sizeof(int), sizeof(int));
            return data;
        }
    }
}
