using System;
using System.Collections.Generic;
using System.Linq;

namespace GameFlow.Core
{
    public static class Tools
    {
        public static IEnumerable<PlayerID> PlayerIDValues = Enum.GetValues(typeof(PlayerID)).Cast<PlayerID>();

        public static int NumberOfPlayers { get => PlayerIDValues.Count(); }

        public static PlayerID NextPlayerID(this PlayerID playerID)
        {
            if (playerID == PlayerIDValues.Last()) return PlayerIDValues.First();
            else return playerID + 1;
        }

        public static void ForEachPlayer(Action<PlayerID> action)
        {
            foreach (var x in PlayerIDValues)
            {
                action(x);
            }
        }
    }
}
