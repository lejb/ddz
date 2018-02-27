using System;

namespace Logic.Core
{
    public class CompressedCard : IComparable<CompressedCard>
    {
        public CardPoint Point { get; }

        public int Count { get; internal set; }

        public CompressedCard(CardPoint point, int count)
        {
            Point = point;
            Count = count;
        }

        int IComparable<CompressedCard>.CompareTo(CompressedCard other)
        {
            if (Count == other.Count) return Point - other.Point;
            else return Count - other.Count;
        }
    }
}
