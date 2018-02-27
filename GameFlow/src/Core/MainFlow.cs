using System.Collections.Generic;
using Logic.Core;
using GameFlow.Interfaces;

namespace GameFlow.Core
{
    public abstract class MainFlow
    {
        private IMainFlowMessages mainFlowMessages;
        private IMainFlowActions mainFlowActions;
        private IMainFlowErrors mainFlowErrors;

        public abstract IGameLogic GameLogic { get; }

        public abstract PlayerID MyPlayerID { get; }

        public PlayerID Turn { get; private set; }

        public bool InMyTurn { get => Turn == MyPlayerID; }

        public bool GameEnded { get; private set; }

        public MainFlow(IMainFlowMessages mainFlowMessages, IMainFlowActions mainFlowActions, 
            IMainFlowErrors mainFlowErrors)
        {
            this.mainFlowMessages = mainFlowMessages;
            this.mainFlowActions = mainFlowActions;
            this.mainFlowErrors = mainFlowErrors;

            mainFlowMessages.BringOutCard += OnBringOutCardMsg;

            mainFlowActions.MyBringOut += OnMyBringOutAction;
        }

        public void StartMainFlow(PlayerID starter)
        {
            GameEnded = false;
            Turn = starter;
            GameLogic.Reset();

            if (InMyTurn) mainFlowActions.OnStartMyTurn();
        }

        protected virtual void OnBringOutCardMsg(PlayerID player, IEnumerable<Card> cards)
        {
            if (player == MyPlayerID) return;

            if (GameEnded)
            {
                mainFlowErrors.OnError("Invalid bring out message when game ended");
                return;
            }

            if (player != Turn)
            {
                mainFlowErrors.OnError("Not in turn exception");
                return;
            }

            if (!GameLogic.Accept(cards))
            {
                mainFlowErrors.OnError("Not accepted card exception");
                return;
            }

            mainFlowActions.OnBringOutCard(player, cards);

            RemoveCards(player, new HashSet<Card>(cards));
            if (GameEnded) return;

            Turn = Turn.NextPlayerID();
            if (InMyTurn) mainFlowActions.OnStartMyTurn();
        }

        protected virtual void OnMyBringOutAction(ISet<Card> myCards)
        {
            if (GameEnded)
            {
                mainFlowErrors.OnError("Invalid bring out when game ended");
                return;
            }

            if (!InMyTurn)
            {
                mainFlowErrors.OnError("Invalid bring out when not my turn");
                return;
            }

            if (!GameLogic.Accept(myCards))
            {
                mainFlowActions.OnIllegalCard();
                return;
            }

            mainFlowActions.OnEndMyTurn();
            mainFlowMessages.SendBringOutCardMsg(MyPlayerID, myCards);

            RemoveCards(MyPlayerID, myCards);
            if (GameEnded) return;

            Turn = Turn.NextPlayerID();
        }

        protected virtual void RemoveCards(PlayerID player, ISet<Card> cards)
        {
            GetPlayerFromID(player).RemoveCards(cards);
            if (GetPlayerFromID(player).Cards.Count == 0)
            {
                mainFlowActions.OnFinalCard(player);
                GameEnded = true;
            }
        }

        protected abstract Player GetPlayerFromID(PlayerID id);
    }
}
