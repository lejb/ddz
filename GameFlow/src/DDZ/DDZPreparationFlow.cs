using GameFlow.Core;
using GameFlow.Interfaces;

namespace GameFlow.DDZ
{
    public class DDZPreparationFlow : PreparationFlow
    {
        private DDZGameData gameData;

        public override PlayerID MyPlayerID => gameData.MyPlayerID;

        public DDZPreparationFlow(IPreparationFlowMessages preparationFlowMessages,
            IPreparationFlowActions preparationFlowActions,
            IPreparationFlowErrors preparationFlowErrors,
            DDZGameData gameData) : base(preparationFlowMessages, preparationFlowActions, preparationFlowErrors)
        {
            this.gameData = gameData;
        }

        protected override Player GetPlayerFromID(PlayerID id)
        {
            return gameData.Players[id];
        }
    }
}
