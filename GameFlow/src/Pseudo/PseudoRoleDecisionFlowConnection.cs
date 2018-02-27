using GameFlow.Core;
using GameFlow.Interfaces;

namespace GameFlow.Pseudo
{
    public class PseudoRoleDecisionFlowConnection : IRoleDecisionFlowMessages
    {
        public event PlayerRoleDecisionEventHandler PlayerRoleDecision;
        public event RoleDecisionFlowStartEventHandler RoleDecisionFlowStart;

        public void StartRoleDecision(PlayerID starter)
        {
            SendStartRoleDecisionMsg(starter);
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
