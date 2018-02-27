using GameFlow.Interfaces;

namespace GameFlow.DDZ
{
    public class DDZErrorHandler
        : IMainFlowErrors, IRoleDecisionFlowErrors, IPreparationFlowErrors, ICardDispatchFlowErrors
    {
        public void OnError(string message)
        {
            throw new DDZException(message);
        }
    }
}
