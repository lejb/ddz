using System.Collections.Generic;
using Logic.Core;

namespace GameFlow.Core
{
    public class Player : IPlayerForFlow, IPlayerForInfo
    {
        public PlayerID ID { get; }

        public string Name { get; set; }

        public bool Exist { get; set; }

        public bool Ready { get; set; }

        public ISet<Card> Cards { get; }

        public Player(PlayerID id)
        {
            ID = id;
            Cards = new HashSet<Card>();
        }

        public void AddCards(ISet<Card> cards)
        {
            Cards.UnionWith(cards);
        }

        public void RemoveCards(ISet<Card> cards)
        {
            Cards.ExceptWith(cards);
        }

        public void ClearCards()
        {
            Cards.Clear();
        }

        public void UpdateInfo(IPlayerForInfo info)
        {
            Name = info.Name;
            Exist = info.Exist;
            Ready = info.Ready;
        }
    }
}
