using System.Linq;
using System.Collections.Generic;
using Logic.Core;
using static Logic.Core.CardPoint;

namespace Logic.DDZ
{
    public static class Tools
    {
        public static bool ListCompare<T>(IList<T> list1, IList<T> list2)
        {
            if (list1.Count != list2.Count) return false;
            for (int i = 0; i < list1.Count; i++) if (!list1[i].Equals(list2[i])) return false;
            return true;
        }

        public static bool CheckCompressedCards(this CompressedCardList compressedList,
            IList<CardPoint> points, IList<int> counts)
        {
            return ListCompare(compressedList.SortedPointList, points) &&
                ListCompare(compressedList.SortedCount, counts);
        }

        public static bool CheckStraight(this CompressedCardList compressedList, int minLength)
        {
            var pointList = compressedList.SortedPointList;

            for (int i = 0; i < compressedList.Count - 1; i++)
            {
                if (pointList[i + 1] - pointList[i] != 1)
                    return false;
            }

            return pointList.Count >= minLength && pointList[0] - _3 >= 0 && A - pointList[pointList.Count - 1] >= 0;
        }

        public static bool AllTheSame<T>(this IEnumerable<T> collection, T element)
        {
            foreach (T t in collection) if (!t.Equals(element)) return false;
            return true;
        }

        public static IList<Card> DisplayOrder(this IList<Card> cards)
        {
            Hand hand = new Hand(cards);
            CompressedCardList compression = new CompressedCardList(hand);
            return
            (
                from card in cards
                orderby compression.SortedPointList.IndexOf(card.Point) descending, 
                        card.Point descending,
                        card.Suit ascending
                select card
            ).ToList();
        }

        public static IList<Card> SortedOrder(this IList<Card> cards)
        {
            return
            (
                from card in cards
                orderby card.Point descending,
                        card.Suit ascending
                select card
            ).ToList();
        }
    }
}
