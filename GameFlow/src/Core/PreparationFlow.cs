using System.Collections.Generic;
using GameFlow.Interfaces;
using static GameFlow.Core.Tools;

namespace GameFlow.Core
{
    public delegate void EndPreparationFlowEventHandler();

    public abstract class PreparationFlow
    {
        private IPreparationFlowMessages preparationFlowMessages;
        private IPreparationFlowActions preparationFlowActions;
        private IPreparationFlowErrors preparationFlowErrors;

        public event EndPreparationFlowEventHandler EndPreparationFlow;

        public abstract PlayerID MyPlayerID { get; }

        public bool GameStarted { get; private set; } = false;

        public PreparationFlow(IPreparationFlowMessages preparationFlowMessages,
            IPreparationFlowActions preparationFlowActions, 
            IPreparationFlowErrors preparationFlowErrors)
        {
            this.preparationFlowMessages = preparationFlowMessages;
            this.preparationFlowActions = preparationFlowActions;
            this.preparationFlowErrors = preparationFlowErrors;

            this.preparationFlowActions.MyInfoChange += OnMyInfoChangeAction;
            this.preparationFlowMessages.PlayerInfoUpdated += OnPlayerInfoUpdatedMsg;
        }

        public void Reset()
        {
            GameStarted = false;
            GetPlayerFromID(MyPlayerID).Exist = true;
            GetPlayerFromID(MyPlayerID).Ready = false;
            OnMyInfoChangeAction();
        }

        private void OnMyInfoChangeAction()
        {
            preparationFlowMessages.SendPlayerUpdateMsg(GetPlayerFromID(MyPlayerID));
        }

        private void OnPlayerInfoUpdatedMsg(IList<IPlayerForInfo> playerInfo)
        {
            int nReady = 0, nExist = 0;
            foreach (var p in playerInfo)
            {
                nExist += p.Exist ? 1 : 0;
                nReady += p.Ready ? 1 : 0;
                if(p.ID != MyPlayerID) GetPlayerFromID(p.ID).UpdateInfo(p);
            }

            preparationFlowActions.OnPlayerInfoUpdated(playerInfo);

            if (nReady == NumberOfPlayers && nReady == nExist)
            {
                preparationFlowActions.OnGameStart();
                if (!GameStarted) EndPreparationFlow?.Invoke();
                GameStarted = true;
            }
        }

        protected abstract Player GetPlayerFromID(PlayerID id);
    }
}
