using System.Collections.Generic;

namespace Logic.Core
{
    public class Hand
    {
        public IList<Card> OriginalCards { get; }

        public IList<Card> SortedCards { get; }

        public Hand(IList<Card> cards)
        {
            OriginalCards = cards;
            var sortedCards = new List<Card>(OriginalCards);
            sortedCards.Sort(new PointSuitComparer());
            SortedCards = sortedCards;
        }

        public static Hand EmptyHand = new Hand(new List<Card>());
    }
}
