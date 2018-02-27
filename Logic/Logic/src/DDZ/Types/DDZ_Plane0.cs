using Logic.Core;

namespace Logic.DDZ.Types
{
    public class DDZ_Plane0 : HandType
    {
        public CardPoint Point { get; }

        public int Length { get; }

        private DDZ_Plane0(CompressedCardList compressedList) : base(compressedList)
        {
            Point = compressedList[0].Point;
            Length = compressedList.Count;
        }

        public int CompareTo(DDZ_Plane0 other)
        {
            if (Length != other.Length) return 0;
            return Point - other.Point;
        }

        public static HandType Creator(CompressedCardList compressedList)
        {
            if (!compressedList.SortedCount.AllTheSame(3) || 
                !compressedList.CheckStraight(2))
                return ErrorType.ErrorTypeInstance;
            return new DDZ_Plane0(compressedList);
        }
    }
}
