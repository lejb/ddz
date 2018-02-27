using System.Collections.Generic;
using Logic.Core;
using GameFlow.Core;
using GameFlow.Interfaces;
using static GameFlow.Core.Tools;
using GameFlow.DDZ;

namespace GameFlow.Pseudo
{
    public class PseudoDDZFlowIO : PseudoIOBase, IDDZIO
    {
        public DDZGameData GameData { get; private set; }

        public event MyRoleDecisionEventHandler MyRoleDecision;
        public event MyInfoChangeEventHandler MyInfoChange;
        public event MyBringOutEventHandler MyBringOut;

        public PseudoDDZFlowIO(DDZGameData gameData)
        {
            GameData = gameData;
        }

        public void MyInfoHasChanged()
        {
            MyInfoChange?.Invoke();
        }

        public void Decide(int roleLevel)
        {
            MyRoleDecision?.Invoke(roleLevel);
        }

        public void BringOut(IEnumerable<Card> cards)
        {
            MyBringOut?.Invoke(new HashSet<Card>(cards));
        }

        public void OnStartMyRoleDecision(IList<int> availableDecisionLevels)
        {
            string str = "start my decision: available ";
            foreach (int x in availableDecisionLevels) str += x.ToString() + ";";
            Log(str);
        }

        public void OnEndMyRoleDecision()
        {
            Log("end my decision");
        }

        public void OnIllegalRoleDecision()
        {
            Log("out of range decision");
        }

        public void OnRoleDecision(PlayerID player, int roleLevel)
        {
            Log($"decision: {player.ToString()}->{roleLevel}");
        }

        public void OnRoleDecisionFinished()
        {
            Log("final decision");
        }

        public void OnAllGiveUp()
        {
            Log("all give up");
        }

        public void OnPlayerInfoUpdated(IList<IPlayerForInfo> info)
        {
            string str = "";
            foreach (var i in info) str += $"{PlayerForInfoString(i)};";
            Log(str);
        }

        private string PlayerForInfoString(IPlayerForInfo info)
        {
            string str1 = info.ID.ToString();
            string str2 = info.Name;
            string str3 = info.Exist ? "exist" : "non-exist";
            string str4 = info.Ready ? "ready" : "not-ready";
            return $"{str1}:{str2},{str3},{str4}";
        }

        public void OnGameStart()
        {
            Log("game started");
        }

        public void OnDispatchFinished()
        {
            Log("dispatch finished");
        }

        public void OnStartMyTurn()
        {
            Log("start my turn");
        }

        public void OnEndMyTurn()
        {
            Log("end my turn");
        }

        public void OnIllegalCard()
        {
            Log("illegal card");
        }

        public void OnBringOutCard(PlayerID player, IEnumerable<Card> cards)
        {
            Log("player bring out: " + player.ToString());
        }

        public void OnFinalCard(PlayerID player)
        {
            Log("final card: " + player.ToString());
        }
    }
}
