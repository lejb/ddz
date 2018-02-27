using System.Collections.Generic;
using Logic.Core;

namespace GameFlow.Core
{
    public interface IPlayerForFlow
    {
        PlayerID ID { get; }

        ISet<Card> Cards { get; }

        void AddCards(ISet<Card> cards);

        void RemoveCards(ISet<Card> cards);

        void ClearCards();
    }
}
