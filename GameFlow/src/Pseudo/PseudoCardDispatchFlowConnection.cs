using System.Collections.Generic;
using Logic.Core;
using GameFlow.Interfaces;

namespace GameFlow.Pseudo
{
    public class PseudoCardDispatchFlowConnection : ICardDispatchFlowMessages
    {
        public event DispatchCardEventHandler DispatchCard;

        public void MasterDispatchCards(IList<IList<Card>> dispatch)
        {
            DispatchCard?.Invoke(dispatch);
        }
    }
}
