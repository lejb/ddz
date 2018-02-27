using System.Collections.Generic;
using GameFlow.Core;
using GameFlow.Interfaces;
using Logic.Core;

namespace GameFlow.Pseudo
{
    public class PseudoDDZFlowConnection : IDDZConnection
    {
        private IDictionary<PlayerID, PseudoPlayerInfo> dict = new Dictionary<PlayerID, PseudoPlayerInfo>();

        public event PlayerInfoUpdatedEventHandler PlayerInfoUpdated;
        public event DispatchCardEventHandler DispatchCard;
        public event RoleDecisionFlowStartEventHandler RoleDecisionFlowStart;
        public event PlayerRoleDecisionEventHandler PlayerRoleDecision;
        public event BringOutCardEventHandler BringOutCard;

        public PseudoDDZFlowConnection()
        {
            dict.Add(PlayerID.P0, new PseudoPlayerInfo(PlayerID.P0));
            dict.Add(PlayerID.P1, new PseudoPlayerInfo(PlayerID.P1));
            dict.Add(PlayerID.P2, new PseudoPlayerInfo(PlayerID.P2));
        }

        public void MasterDispatchCards(IList<IList<Card>> dispatch)
        {
            DispatchCard?.Invoke(dispatch);
        }

        public void SendBringOutCardMsg(PlayerID playerID, IEnumerable<Card> cards)
        {
            BringOutCard?.Invoke(playerID, cards);
        }

        public void SendPlayerUpdateMsg(IPlayerForInfo info)
        {
            dict[info.ID] = new PseudoPlayerInfo(info);
            PlayerInfoUpdated?.Invoke(new List<IPlayerForInfo>(dict.Values));
        }

        public void SendRoleDecisionMsg(PlayerID playerID, int roleLevel)
        {
            PlayerRoleDecision?.Invoke(playerID, roleLevel);
        }

        public void SendStartRoleDecisionMsg(PlayerID starter)
        {
            RoleDecisionFlowStart?.Invoke(starter);
        }
    }
}
