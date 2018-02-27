using System.Collections.Generic;
using Logic.Core;

namespace Logic.DDZ.Types
{
    public class DDZ_4_2_2 : HandType
    {
        public CardPoint Point { get; }

        private DDZ_4_2_2(CompressedCardList compressedList) : base(compressedList)
        {
            Point = compressedList[2].Point;
        }

        public int CompareTo(DDZ_4_2_2 other)
        {
            return Point - other.Point;
        }

        public static HandType Creator(CompressedCardList compressedList)
        {
            var expectedCount = new List<int>() { 2, 2, 4 };

            if (!Tools.ListCompare(compressedList.SortedCount, expectedCount))
                return ErrorType.ErrorTypeInstance;

            return new DDZ_4_2_2(compressedList);
        }
    }
}
