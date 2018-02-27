using System.Collections.Generic;

namespace Logic.Core
{
    public class CompressedCardList : List<CompressedCard>
    {
        public IList<CardPoint> SortedPointList { get; private set; }

        public IList<int> SortedCount { get; private set; }

        public CompressedCardList(Hand hand) : base()
        {
            CardPoint currentCardPoint = null;

            foreach (Card card in hand.SortedCards)
            {
                if (card.Point != currentCardPoint) base.Add(new CompressedCard(card.Point, 1));
                else base[base.Count - 1].Count++;
                currentCardPoint = card.Point;
            }

            base.Sort();
            CreateSubLists();
        }

        public CompressedCardList(IEnumerable<CompressedCard> sortedCards) : base(sortedCards)
        {
            CreateSubLists();
        }

        private void CreateSubLists()
        {
            SortedPointList = new List<CardPoint>();
            SortedCount = new List<int>();

            for (int i = 0; i < base.Count; i++)
            {
                SortedPointList.Add(base[i].Point);
                SortedCount.Add(base[i].Count);
            }
        }

        public static CompressedCardList EmptyCompressedCardList = new CompressedCardList(Hand.EmptyHand);
    }
}
