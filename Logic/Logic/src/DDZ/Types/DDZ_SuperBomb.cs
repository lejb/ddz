using System.Collections.Generic;
using Logic.Core;
using static Logic.Core.CardPoint;

namespace Logic.DDZ.Types
{
    public class DDZ_SuperBomb : HandType
    {
        private DDZ_SuperBomb(CompressedCardList compressedList) : base(compressedList)
        {
        }

        public int CompareTo(DDZ_SuperBomb other)
        {
            return 0;
        }

        public override int CompareTo(HandType other)
        {
            return 1;
        }

        public static HandType Creator(CompressedCardList compressedList)
        {
            var expectedPoints = new List<CardPoint>() { joker, JOKER };
            var expectedCount = new List<int>() { 1, 1 };

            if (!compressedList.CheckCompressedCards(expectedPoints, expectedCount))
                return ErrorType.ErrorTypeInstance;

            return new DDZ_SuperBomb(compressedList);
        }
    }
}
