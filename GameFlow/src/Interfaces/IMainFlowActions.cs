using System.Collections.Generic;
using Logic.Core;
using GameFlow.Core;

namespace GameFlow.Interfaces
{
    public delegate void MyBringOutEventHandler(ISet<Card> myCards);

    public interface IMainFlowActions
    {
        event MyBringOutEventHandler MyBringOut;

        void OnStartMyTurn();

        void OnEndMyTurn();

        void OnIllegalCard();

        void OnBringOutCard(PlayerID player, IEnumerable<Card> cards);

        void OnFinalCard(PlayerID player);
    }
}
