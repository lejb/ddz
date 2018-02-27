using System.Collections.Generic;

namespace Logic.Core
{
    public class PointSuitComparer : IComparer<Card>
    {
        public int Compare(Card x, Card y)
        {
            if (x.Point == y.Point) return x.Suit - y.Suit;
            else return x.Point - y.Point;
        }
    }
}