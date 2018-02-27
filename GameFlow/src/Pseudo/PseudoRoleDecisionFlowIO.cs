using System.Collections.Generic;
using GameFlow.Core;
using GameFlow.Interfaces;
using Logic.Core;

namespace GameFlow.Pseudo
{
    public class PseudoRoleDecisionFlowIO : PseudoIOBase, IRoleDecisionFlowActions
    {
        public event MyRoleDecisionEventHandler MyRoleDecision;

        public void Decide(int roleLevel)
        {
            MyRoleDecision?.Invoke(roleLevel);
        }

        public void OnStartMyRoleDecision(IList<int> availableDecisionLevels)
        {
            string str = "start my decision: available ";
            foreach (int x in availableDecisionLevels) str += x.ToString() + ";";
            Log(str);
        }

        public void OnEndMyRoleDecision()
        {
            Log("end my decision");
        }

        public void OnIllegalRoleDecision()
        {
            Log("out of range decision");
        }

        public void OnRoleDecision(PlayerID player, int roleLevel)
        {
            Log($"decision: {player.ToString()}->{roleLevel}");
        }

        public void OnRoleDecisionFinished()
        {
            Log("final decision");
        }

        public void OnAllGiveUp()
        {
            Log("all give up");
        }
    }
}
