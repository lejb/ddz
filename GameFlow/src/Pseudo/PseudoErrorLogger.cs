using System.Collections.Generic;
using GameFlow.Interfaces;

namespace GameFlow.Pseudo
{
    public class PseudoErrorLogger : List<string>, 
        IMainFlowErrors, IRoleDecisionFlowErrors, IPreparationFlowErrors, ICardDispatchFlowErrors
    {
        public void OnError(string message)
        {
            Add(message);
        }
    }
}
