using Logic.Core;
using Logic.DDZ;
using GameFlow.Core;
using GameFlow.Interfaces;

namespace GameFlow.DDZ
{
    public class DDZMainFlow : MainFlow
    {
        private DDZLogic ddzLogic = new DDZLogic();
        private DDZGameData gameData;

        public override IGameLogic GameLogic { get { return ddzLogic; } }

        public override PlayerID MyPlayerID => gameData.MyPlayerID;

        public DDZMainFlow(IMainFlowMessages mainFlowMessages, IMainFlowActions mainFlowActions, 
            IMainFlowErrors mainFlowErrors, DDZGameData gameData)
            : base(mainFlowMessages, mainFlowActions, mainFlowErrors)
        {
            this.gameData = gameData;
        }

        protected override Player GetPlayerFromID(PlayerID id)
        {
            return gameData.Players[id];
        }
    }
}
