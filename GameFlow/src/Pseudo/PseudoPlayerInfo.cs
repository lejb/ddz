using GameFlow.Core;

namespace GameFlow.Pseudo
{
    public class PseudoPlayerInfo : IPlayerForInfo
    {
        public PlayerID ID { get; }

        public string Name { get; set; }

        public bool Exist { get; set; }

        public bool Ready { get; set; }

        public PseudoPlayerInfo(IPlayerForInfo info)
        {
            ID = info.ID;
            Name = info.Name;
            Exist = info.Exist;
            Ready = info.Ready;
        }

        public PseudoPlayerInfo(PlayerID id)
        {
            ID = id;
        }
    }
}
