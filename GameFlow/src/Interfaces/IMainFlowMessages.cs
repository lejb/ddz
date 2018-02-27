using System.Collections.Generic;
using Logic.Core;
using GameFlow.Core;

namespace GameFlow.Interfaces
{
    public delegate void BringOutCardEventHandler(PlayerID player, IEnumerable<Card> cards);

    public interface IMainFlowMessages
    {
        event BringOutCardEventHandler BringOutCard;

        void SendBringOutCardMsg(PlayerID playerID, IEnumerable<Card> cards);
    }
}
