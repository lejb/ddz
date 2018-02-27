using System.Collections.Generic;
using Logic.Core;
using GameFlow.Core;
using GameFlow.Interfaces;

namespace GameFlow.Pseudo
{
    public class PseudoMainFlowConnection : IMainFlowMessages
    {
        public event BringOutCardEventHandler BringOutCard;

        public void SendBringOutCardMsg(PlayerID playerID, IEnumerable<Card> cards)
        {
            BringOutCard?.Invoke(playerID, cards);
        }
    }
}
