using System.Collections.Generic;

namespace Logic.Core
{
    public interface IGameLogic
    {
        HandType Current { get; }

        void Reset();

        bool Accept(IEnumerable<Card> cards);
    }
}
