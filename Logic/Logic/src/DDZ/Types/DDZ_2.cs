using Logic.Core;

namespace Logic.DDZ.Types
{
    public class DDZ_2 : HandType
    {
        public CardPoint Point { get; }

        private DDZ_2(CompressedCardList compressedList) : base(compressedList)
        {
            Point = compressedList[0].Point;
        }

        public int CompareTo(DDZ_2 other)
        {
            return Point - other.Point;
        }

        public static HandType Creator(CompressedCardList compressedList)
        {
            if (compressedList.Count != 1 || compressedList[0].Count != 2) return ErrorType.ErrorTypeInstance;
            return new DDZ_2(compressedList);
        }
    }
}
