using System.Collections.Generic;
using Logic.Core;
using GameFlow.Core;
using GameFlow.Interfaces;

namespace GameFlow.DDZ
{
    public class DDZFlow
    {
        private DDZErrorHandler errorHandler;
        private DDZPreparationFlow preparationFlow;
        private DDZCardDispatchFlow cardDispatchFlow;
        private DDZRoleDecisionFlowV1 roleDecisionFlow;
        private DDZMainFlow mainFlow;
        private DDZGameData gameData;
        private IDDZIO io;
        private IDDZConnection connection;

        public DDZFlow(IDDZConnection connection, IDDZIO io, DDZGameData gameData)
        {
            this.connection = connection;
            this.io = io;
            this.gameData = gameData;

            errorHandler = new DDZErrorHandler();
            preparationFlow = new DDZPreparationFlow(connection, io, errorHandler, gameData);
            cardDispatchFlow = new DDZCardDispatchFlow(connection, io, errorHandler, gameData);
            roleDecisionFlow = new DDZRoleDecisionFlowV1(connection, io, errorHandler, gameData);
            mainFlow = new DDZMainFlow(connection, io, errorHandler, gameData);

            preparationFlow.EndPreparationFlow += OnEndPreparationFlow;
            cardDispatchFlow.EndCardDispatchFlow += OnEndCardDispatchFlow;
            roleDecisionFlow.EndRoleDecisionFlow += OnEndRoleDecisionFlow;
            roleDecisionFlow.EndWithAllGiveUp += OnEndWithAllGiveUp;
        }

        public virtual void Reset()
        {
            preparationFlow.Reset();
        }

        public virtual void ReDispatch()
        {
            cardDispatchFlow.StartMasterDispatchCard();
        }

        protected virtual void OnEndPreparationFlow()
        {
            cardDispatchFlow.StartMasterDispatchCard();
        }

        protected virtual void OnEndCardDispatchFlow()
        {
            roleDecisionFlow.MasterChooseRoleDecisionStarter();
        }

        protected virtual void OnEndRoleDecisionFlow()
        {
            gameData.Players[gameData.DZPlayer].AddCards(new HashSet<Card>(gameData.SecretCards));
            mainFlow.StartMainFlow(gameData.DZPlayer);
        }

        protected virtual void OnEndWithAllGiveUp()
        {
            ReDispatch();
        }
    }
}
