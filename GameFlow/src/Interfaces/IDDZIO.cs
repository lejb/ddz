using GameFlow.DDZ;

namespace GameFlow.Interfaces
{
    public interface IDDZIO : IPreparationFlowActions, ICardDispatchFlowActions,
        IRoleDecisionFlowActions, IMainFlowActions
    {
        DDZGameData GameData { get; }
    }
}
