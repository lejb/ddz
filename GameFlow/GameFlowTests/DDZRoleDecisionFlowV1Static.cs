using GameFlow.Core;
using GameFlow.DDZ;
using GameFlow.Interfaces;

namespace GameFlow.Tests
{
    public class DDZRoleDecisionFlowV1Static : DDZRoleDecisionFlowV1
    {
        public DDZRoleDecisionFlowV1Static(IRoleDecisionFlowMessages roleDecisionFlowMessages,
            IRoleDecisionFlowActions roleDecisionFlowActions,
            IRoleDecisionFlowErrors roleDecisionFlowErrors,
            DDZGameData gameData) : base(roleDecisionFlowMessages, roleDecisionFlowActions, roleDecisionFlowErrors, gameData)
        {
        }

        protected override PlayerID StarterFunction()
        {
            return PlayerID.P0;
        }
    }
}
