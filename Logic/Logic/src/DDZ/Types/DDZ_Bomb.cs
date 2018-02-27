using Logic.Core;

namespace Logic.DDZ.Types
{
    public class DDZ_Bomb : HandType
    {
        public CardPoint Point { get; }

        private DDZ_Bomb(CompressedCardList compressedList) : base(compressedList)
        {
            Point = compressedList[0].Point;
        }

        public int CompareTo(DDZ_Bomb other)
        {
            return Point - other.Point;
        }

        public override int CompareTo(HandType other)
        {
            return other is DDZ_SuperBomb ? -1 : 1;
        }

        public static HandType Creator(CompressedCardList compressedList)
        {
            if (compressedList.Count != 1 || compressedList[0].Count != 4) return ErrorType.ErrorTypeInstance;
            return new DDZ_Bomb(compressedList);
        }
    }
}
