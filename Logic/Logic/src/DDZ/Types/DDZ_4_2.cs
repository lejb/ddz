using System.Collections.Generic;
using Logic.Core;

namespace Logic.DDZ.Types
{
    public class DDZ_4_2 : HandType
    {
        public CardPoint Point { get; }

        private DDZ_4_2(CompressedCardList compressedList) : base(compressedList)
        {
            Point = compressedList[compressedList.Count - 1].Point;
        }

        public int CompareTo(DDZ_4_2 other)
        {
            return Point - other.Point;
        }

        public static HandType Creator(CompressedCardList compressedList)
        {
            var expectedCount1 = new List<int>() { 2, 4 };
            var expectedCount2 = new List<int>() { 1, 1, 4 };

            if (!Tools.ListCompare(compressedList.SortedCount, expectedCount1) &&
                !Tools.ListCompare(compressedList.SortedCount, expectedCount2))
                return ErrorType.ErrorTypeInstance;

            return new DDZ_4_2(compressedList);
        }
    }
}
