using System;
using System.Collections.Generic;
using System.Linq;

namespace GameFlow.Core
{
    public class Players
    {
        public IDictionary<PlayerID, Player> AllPlayers { get; }

        public PlayerID MasterPlayer { get; } = PlayerID.P0;

        public Players()
        {
            AllPlayers = new Dictionary<PlayerID, Player>();
            foreach (var x in Enum.GetValues(typeof(PlayerID)).Cast<PlayerID>())
            {
                AllPlayers.Add(x, new Player(x));
            }
        }

        public Player this[PlayerID playerID] { get => AllPlayers.ContainsKey(playerID) ? AllPlayers[playerID] : null; }
    }
}
