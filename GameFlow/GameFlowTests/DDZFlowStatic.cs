using System.Collections.Generic;
using Logic.Core;
using GameFlow.DDZ;
using GameFlow.Interfaces;

namespace GameFlow.Tests
{
    public class DDZFlowStatic
    {
        private DDZErrorHandler errorHandler;
        private DDZPreparationFlow preparationFlow;
        private DDZCardDispatchFlow cardDispatchFlow;
        private DDZRoleDecisionFlowV1Static roleDecisionFlow;
        private DDZMainFlow mainFlow;
        private DDZGameData gameData;
        private IDDZIO io;
        private IDDZConnection connection;

        public DDZFlowStatic(IDDZConnection connection, IDDZIO io, DDZGameData gameData)
        {
            this.connection = connection;
            this.io = io;
            this.gameData = gameData;

            errorHandler = new DDZErrorHandler();
            preparationFlow = new DDZPreparationFlow(connection, io, errorHandler, gameData);
            cardDispatchFlow = new DDZCardDispatchFlow(connection, io, errorHandler, gameData);
            roleDecisionFlow = new DDZRoleDecisionFlowV1Static(connection, io, errorHandler, gameData);
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

        private void OnEndPreparationFlow()
        {
            cardDispatchFlow.StartMasterDispatchCard();
        }

        private void OnEndCardDispatchFlow()
        {
            roleDecisionFlow.MasterChooseRoleDecisionStarter();
        }

        private void OnEndRoleDecisionFlow()
        {
            if (gameData.MyPlayerID == gameData.DZPlayer)
                gameData.Players[gameData.MyPlayerID].AddCards(new HashSet<Card>(gameData.SecretCards));
            mainFlow.StartMainFlow(gameData.DZPlayer);
        }

        private void OnEndWithAllGiveUp()
        {
            ReDispatch();
        }
    }
}
