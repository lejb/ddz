using System;
using System.Collections.Generic;
using System.Linq;
using Logic.Core;
using GameFlow.Core;
using GameFlow.Interfaces;

namespace GameFlow.DDZ
{
    public delegate void EndCardDispatchFlowEventHandler();

    public class DDZCardDispatchFlow
    {
        private ICardDispatchFlowMessages cardDispatchFlowMessages;
        private ICardDispatchFlowActions cardDispatchFlowActions;
        private ICardDispatchFlowErrors cardDispatchFlowErrors;
        private DDZGameData gameData;

        public event EndCardDispatchFlowEventHandler EndCardDispatchFlow;

        public DDZCardDispatchFlow(ICardDispatchFlowMessages cardDispatchFlowMessages,
            ICardDispatchFlowActions cardDispatchFlowActions,
            ICardDispatchFlowErrors cardDispatchFlowErrors, DDZGameData gameData)
        {
            this.cardDispatchFlowMessages = cardDispatchFlowMessages;
            this.cardDispatchFlowActions = cardDispatchFlowActions;
            this.cardDispatchFlowErrors = cardDispatchFlowErrors;
            
            this.cardDispatchFlowMessages.DispatchCard += OnDispatchCardMsg;

            this.gameData = gameData;
        }

        public void StartMasterDispatchCard()
        {
            if (gameData.MyPlayerID != gameData.Players.MasterPlayer)
            {
                return;
            }

            Random rnd = new Random();

            var tuples =
            (
                from card in Cards.AllCards
                select new Tuple<Card, int>(card, rnd.Next())
            ).ToList();

            tuples.Sort((tuple1, tuple2) => tuple1.Item2 - tuple2.Item2);

            var shuffle =
            (
                from tuple in tuples
                select tuple.Item1
            ).ToList();

            var dispatch = new List<IList<Card>>
            {
                new List<Card>(shuffle.GetRange(0, 17)),
                new List<Card>(shuffle.GetRange(17, 17)),
                new List<Card>(shuffle.GetRange(34, 17)),
                new List<Card>(shuffle.GetRange(51, 3)),
            };

            cardDispatchFlowMessages.MasterDispatchCards(dispatch);
        }

        private void OnDispatchCardMsg(IList<IList<Card>> dispatch)
        {
            if (dispatch.Count != 4)
            {
                cardDispatchFlowErrors.OnError($"invalid dispatch with length {dispatch.Count}");
                return;
            }

            gameData.Players[PlayerID.P0].Cards.Clear();
            gameData.Players[PlayerID.P1].Cards.Clear();
            gameData.Players[PlayerID.P2].Cards.Clear();
            gameData.Players[PlayerID.P0].AddCards(new HashSet<Card>(dispatch[0]));
            gameData.Players[PlayerID.P1].AddCards(new HashSet<Card>(dispatch[1]));
            gameData.Players[PlayerID.P2].AddCards(new HashSet<Card>(dispatch[2]));
            gameData.SecretCards = dispatch[3];

            cardDispatchFlowActions.OnDispatchFinished();
            EndCardDispatchFlow?.Invoke();
        }
    }
}
