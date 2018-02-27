using System.Collections.Generic;
using Logic.Core;
using GameFlow.Core;

namespace GameFlow.DDZ
{
    public class DDZGameData
    {
        public IList<Card> SecretCards { get; set; }

        public PlayerID DZPlayer { get; set; }

        public PlayerID MyPlayerID { get; }

        public Players Players { get; private set; }

        public DDZGameData(PlayerID myPlayerID)
        {
            MyPlayerID = myPlayerID;
            ClearPlayers();
        }

        public void ClearPlayers()
        {
            Players = new Players();
        }

        public void ResetPlayers()
        {
            foreach (Player p in Players.AllPlayers.Values)
            {
                p.Ready = false;
                p.ClearCards();
            }
        }
    }
}
