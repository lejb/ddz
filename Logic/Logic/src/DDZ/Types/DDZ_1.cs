using Logic.Core;

namespace Logic.DDZ.Types
{
    public class DDZ_1 : HandType
    {
        public CardPoint Point { get; }

        private DDZ_1(CompressedCardList compressedList) : base(compressedList)
        {
            Point = compressedList[0].Point;
        }

        public int CompareTo(DDZ_1 other)
        {
            return Point - other.Point;
        }

        public static HandType Creator(CompressedCardList compressedList)
        {
            if (compressedList.Count != 1 || compressedList[0].Count != 1) return ErrorType.ErrorTypeInstance;
            return new DDZ_1(compressedList);
        }
    }
}
