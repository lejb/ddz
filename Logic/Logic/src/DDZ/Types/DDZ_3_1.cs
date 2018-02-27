﻿using System.Collections.Generic;
using Logic.Core;

namespace Logic.DDZ.Types
{
    public class DDZ_3_1 : HandType
    {
        public CardPoint Point { get; }

        private DDZ_3_1(CompressedCardList compressedList) : base(compressedList)
        {
            Point = compressedList[1].Point;
        }

        public int CompareTo(DDZ_3_1 other)
        {
            return Point - other.Point;
        }

        public static HandType Creator(CompressedCardList compressedList)
        {
            var expectedCount = new List<int>() { 1, 3 };

            if (!Tools.ListCompare(compressedList.SortedCount, expectedCount))
                return ErrorType.ErrorTypeInstance;

            return new DDZ_3_1(compressedList);
        }
    }
}
