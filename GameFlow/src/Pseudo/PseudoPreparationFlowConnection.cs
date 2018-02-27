using System.Collections.Generic;
using GameFlow.Core;
using GameFlow.Interfaces;

namespace GameFlow.Pseudo
{
    public class PseudoPreparationFlowConnection : IPreparationFlowMessages
    {
        private IDictionary<PlayerID, PseudoPlayerInfo> dict = new Dictionary<PlayerID, PseudoPlayerInfo>();

        public event PlayerInfoUpdatedEventHandler PlayerInfoUpdated;

        public void SendPlayerUpdateMsg(IPlayerForInfo info)
        {
            dict[info.ID] = new PseudoPlayerInfo(info);
            PlayerInfoUpdated?.Invoke(new List<IPlayerForInfo>(dict.Values));
        }

        public PseudoPreparationFlowConnection()
        {
            dict.Add(PlayerID.P0, new PseudoPlayerInfo(PlayerID.P0));
            dict.Add(PlayerID.P1, new PseudoPlayerInfo(PlayerID.P1));
            dict.Add(PlayerID.P2, new PseudoPlayerInfo(PlayerID.P2));
        }
    }
}
