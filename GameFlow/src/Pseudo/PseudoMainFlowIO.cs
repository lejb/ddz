using System.Collections.Generic;
using GameFlow.Core;
using GameFlow.Interfaces;
using Logic.Core;

namespace GameFlow.Pseudo
{
    public class PseudoMainFlowIO : PseudoIOBase, IMainFlowActions
    {
        public event MyBringOutEventHandler MyBringOut;

        public void OnStartMyTurn()
        {
            Log("start my turn");
        }

        public void OnEndMyTurn()
        {
            Log("end my turn");
        }

        public void OnBringOutCard(PlayerID player, IEnumerable<Card> cards)
        {
            Log("player bring out: " + player.ToString());
        }

        public void OnFinalCard(PlayerID player)
        {
            Log("final card: " + player.ToString());
        }

        public void OnIllegalCard()
        {
            Log("illegal card");
        }

        public void BringOut(IEnumerable<Card> cards)
        {
            MyBringOut?.Invoke(new HashSet<Card>(cards));
        }
    }
}
