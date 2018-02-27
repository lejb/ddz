using GameFlow.Core;
using GameFlow.Interfaces;

namespace GameFlow.DDZ
{
    public class DDZRoleDecisionFlowV1 : DDZRoleDecisionFlowBase
    {
        protected override IDDZRoleRangeLogic RoleRangeLogic { get; } = new DDZRoleRangeLogicV1();

        public DDZRoleDecisionFlowV1(IRoleDecisionFlowMessages roleDecisionFlowMessages,
            IRoleDecisionFlowActions roleDecisionFlowActions,
            IRoleDecisionFlowErrors roleDecisionFlowErrors,
            DDZGameData gameData) : base(roleDecisionFlowMessages, roleDecisionFlowActions, roleDecisionFlowErrors, gameData)
        {
        }
    }
}
