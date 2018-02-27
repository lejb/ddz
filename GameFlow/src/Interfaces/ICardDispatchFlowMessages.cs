using System.Collections.Generic;
using Logic.Core;

namespace GameFlow.Interfaces
{
    public delegate void DispatchCardEventHandler(IList<IList<Card>> dispatch);

    public interface ICardDispatchFlowMessages
    {
        event DispatchCardEventHandler DispatchCard;

        void MasterDispatchCards(IList<IList<Card>> dispatch);
    }
}
