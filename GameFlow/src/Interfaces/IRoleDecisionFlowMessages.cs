using GameFlow.Core;

namespace GameFlow.Interfaces
{
    public delegate void RoleDecisionFlowStartEventHandler(PlayerID starter);
    public delegate void PlayerRoleDecisionEventHandler(PlayerID player, int roleLevel);

    public interface IRoleDecisionFlowMessages
    {
        event RoleDecisionFlowStartEventHandler RoleDecisionFlowStart;
        event PlayerRoleDecisionEventHandler PlayerRoleDecision;

        void SendRoleDecisionMsg(PlayerID playerID, int roleLevel);

        void SendStartRoleDecisionMsg(PlayerID starter);
    }
}
