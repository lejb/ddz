using System.Collections.Generic;
using Networking.Core;
using Networking.Protocol;
using Networking.DDZ.Protocols;
using GameFlow.Core;
using GameFlow.Interfaces;
using Logic.Core;

namespace Networking.DDZ
{
    public delegate void BootstrapEventHandler(PlayerID givenID);

    public class DDZClient : ProtocolBasedClient, IDDZConnection
    {
        public event PlayerInfoUpdatedEventHandler PlayerInfoUpdated;
        public event DispatchCardEventHandler DispatchCard;
        public event RoleDecisionFlowStartEventHandler RoleDecisionFlowStart;
        public event PlayerRoleDecisionEventHandler PlayerRoleDecision;
        public event BringOutCardEventHandler BringOutCard;
        public event BootstrapEventHandler Bootstrap;
        
        private IProtocolHandler protocolHandler;

        public DDZClient(IProtocolHandler protocolHandler)
        {
            this.protocolHandler = protocolHandler;
            ReadProtocol += OnProtocol;
        }

        public void SendPlayerUpdateMsg(IPlayerForInfo info)
        {
            SendProtocol(new UpdateInfoProtocol_C(info));
        }

        public void MasterDispatchCards(IList<IList<Card>> dispatch)
        {
            SendProtocol(new DispatchCardProtocol(dispatch));
        }

        public void SendStartRoleDecisionMsg(PlayerID starter)
        {
            SendProtocol(new RoleDecisionStartProtocol(starter));
        }

        public void SendRoleDecisionMsg(PlayerID playerID, int roleLevel)
        {
            SendProtocol(new RoleDecisionProtocol(playerID, roleLevel));
        }

        public void SendBringOutCardMsg(PlayerID playerID, IEnumerable<Card> cards)
        {
            SendProtocol(new BringOutCardProtocol(playerID, cards));
        }

        private void OnProtocol(IProtocol protocol)
        {
            switch (protocol)
            {
                case DispatchCardProtocol p1:
                    DispatchCard?.Invoke(p1.Dispatch);
                    break;
                case RoleDecisionStartProtocol p2:
                    RoleDecisionFlowStart?.Invoke(p2.Starter);
                    break;
                case RoleDecisionProtocol p3:
                    PlayerRoleDecision?.Invoke(p3.PlayerID, p3.Level);
                    break;
                case BringOutCardProtocol p4:
                    BringOutCard?.Invoke(p4.PlayerID, p4.Cards);
                    break;
                case UpdateInfoProtocol_S p5:
                    PlayerInfoUpdated?.Invoke(p5.Infos);
                    break;
                case BootstrapProtocol p6:
                    Bootstrap?.Invoke(p6.GivenID);
                    break;
                default: protocolHandler.ProcessProtocol(protocol); break;
            }
        }
    }
}
