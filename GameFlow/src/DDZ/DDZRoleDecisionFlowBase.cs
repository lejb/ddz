using System;
using GameFlow.Core;
using GameFlow.Interfaces;
using static GameFlow.Core.Tools;

namespace GameFlow.DDZ
{
    public delegate void EndWithAllGiveUpEventHandler();
    public delegate void EndRoleDecisionFlowEventHandler();

    public abstract class DDZRoleDecisionFlowBase
    {
        public event EndWithAllGiveUpEventHandler EndWithAllGiveUp;

        protected IRoleDecisionFlowMessages roleDecisionFlowMessages;
        protected IRoleDecisionFlowActions roleDecisionFlowActions;
        protected IRoleDecisionFlowErrors roleDecisionFlowErrors;
        protected DDZGameData gameData;

        protected abstract IDDZRoleRangeLogic RoleRangeLogic { get; }

        public event EndRoleDecisionFlowEventHandler EndRoleDecisionFlow;

        public PlayerID Turn { get; private set; }

        public PlayerID Starter { get; private set; }

        public bool AllGiveUp { get; private set; }

        public bool InMyTurn { get => Turn == gameData.MyPlayerID; }

        public DDZRoleDecisionFlowBase(IRoleDecisionFlowMessages roleDecisionFlowMessages,
            IRoleDecisionFlowActions roleDecisionFlowActions,
            IRoleDecisionFlowErrors roleDecisionFlowErrors, 
            DDZGameData gameData)
        {
            this.roleDecisionFlowMessages = roleDecisionFlowMessages;
            this.roleDecisionFlowActions = roleDecisionFlowActions;
            this.roleDecisionFlowErrors = roleDecisionFlowErrors;

            roleDecisionFlowMessages.PlayerRoleDecision += OnPlayerRoleDecisionMsg;
            roleDecisionFlowMessages.RoleDecisionFlowStart += OnRoleDecisionFlowStartMsg;

            roleDecisionFlowActions.MyRoleDecision += OnMyRoleDecisionAction;

            RoleRangeLogic.AllGiveUp += OnAllGiveUp;

            this.gameData = gameData;
        }

        public virtual void MasterChooseRoleDecisionStarter()
        {
            if (gameData.MyPlayerID == gameData.Players.MasterPlayer)
            {
                roleDecisionFlowMessages.SendStartRoleDecisionMsg(StarterFunction());
            }
        }

        protected virtual PlayerID StarterFunction()
        {
            Random rnd = new Random();
            return (PlayerID)rnd.Next(0, NumberOfPlayers);
        }

        protected virtual void OnAllGiveUp()
        {
            AllGiveUp = true;
            roleDecisionFlowActions.OnAllGiveUp();
            EndWithAllGiveUp?.Invoke();
        }

        protected virtual void OnRoleDecisionFlowStartMsg(PlayerID starter)
        {
            Starter = starter;
            Turn = starter;
            AllGiveUp = false;
            RoleRangeLogic.Reset();

            if (InMyTurn) roleDecisionFlowActions.OnStartMyRoleDecision(RoleRangeLogic.CurrentRange);
        }

        protected virtual void OnPlayerRoleDecisionMsg(PlayerID player, int roleLevel)
        {
            if (player == gameData.MyPlayerID) return;

            if (RoleRangeLogic.End)
            {
                roleDecisionFlowErrors.OnError("Invalid role decision message when decision ended");
                return;
            }
            if (player != Turn)
            {
                roleDecisionFlowErrors.OnError("Not in turn role decision exception");
                return;
            }

            if (!RoleRangeLogic.CurrentRange.Contains(roleLevel))
            {
                roleDecisionFlowErrors.OnError("Not in range decision exception");
                return;
            }

            roleDecisionFlowActions.OnRoleDecision(player, roleLevel);

            RoleRangeLogic.Accept(roleLevel);

            if (AllGiveUp) return;

            if (roleLevel > 0) gameData.DZPlayer = player;

            if (RoleRangeLogic.End)
            {
                roleDecisionFlowActions.OnRoleDecisionFinished();
                EndRoleDecisionFlow?.Invoke();
                return;
            }

            Turn = Turn.NextPlayerID();
            if (InMyTurn) roleDecisionFlowActions.OnStartMyRoleDecision(RoleRangeLogic.CurrentRange);
        }

        protected virtual void OnMyRoleDecisionAction(int roleLevel)
        {
            if (RoleRangeLogic.End)
            {
                roleDecisionFlowErrors.OnError("Invalid decisoin when decision ended");
                return;
            }

            if (!InMyTurn)
            {
                roleDecisionFlowErrors.OnError("Invalid decision when not my turn");
                return;
            }

            if (!RoleRangeLogic.CurrentRange.Contains(roleLevel))
            {
                roleDecisionFlowActions.OnIllegalRoleDecision();
                return;
            }

            roleDecisionFlowActions.OnEndMyRoleDecision();
            roleDecisionFlowMessages.SendRoleDecisionMsg(gameData.MyPlayerID, roleLevel);

            RoleRangeLogic.Accept(roleLevel);

            if (AllGiveUp) return;

            if (roleLevel > 0) gameData.DZPlayer = gameData.MyPlayerID;

            if (RoleRangeLogic.End)
            {
                roleDecisionFlowActions.OnRoleDecisionFinished();
                EndRoleDecisionFlow?.Invoke();
                return;
            }

            Turn = Turn.NextPlayerID();
        }
    }
}
