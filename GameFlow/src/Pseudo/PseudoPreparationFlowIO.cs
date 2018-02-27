using System.Collections.Generic;
using GameFlow.Core;
using GameFlow.DDZ;
using GameFlow.Interfaces;
using static GameFlow.Core.Tools;

namespace GameFlow.Pseudo
{
    public class PseudoPreparationFlowIO : PseudoIOBase, IPreparationFlowActions
    {
        public event MyInfoChangeEventHandler MyInfoChange;

        public DDZGameData GameData { get; private set; }

        public PseudoPreparationFlowIO(DDZGameData gameData)
        {
            GameData = gameData;
        }

        public void OnGameStart()
        {
            Log("game started");
        }

        public void OnPlayerInfoUpdated(IList<IPlayerForInfo> info)
        {
            string str = "";
            foreach (var i in info) str += $"{PlayerForInfoString(i)};";
            Log(str);
        }

        public void MyInfoHasChanged()
        {
            MyInfoChange?.Invoke();
        }

        private string PlayerForInfoString(IPlayerForInfo info)
        {
            string str1 = info.ID.ToString();
            string str2 = info.Name;
            string str3 = info.Exist ? "exist" : "non-exist";
            string str4 = info.Ready ? "ready" : "not-ready";
            return $"{str1}:{str2},{str3},{str4}";
        }
    }
}
