using System.Collections.Generic;
using GameFlow.Core;

namespace GameFlow.Interfaces
{
    public delegate void MyRoleDecisionEventHandler(int roleLevel);

    public interface IRoleDecisionFlowActions
    {
        event MyRoleDecisionEventHandler MyRoleDecision;

        void OnStartMyRoleDecision(IList<int> availableDecisionLevels);

        void OnEndMyRoleDecision();

        void OnIllegalRoleDecision();

        void OnRoleDecision(PlayerID player, int roleLevel);

        void OnRoleDecisionFinished();

        void OnAllGiveUp();
    }
}
