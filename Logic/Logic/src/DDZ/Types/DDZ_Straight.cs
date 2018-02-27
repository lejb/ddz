using Logic.Core;

namespace Logic.DDZ.Types
{
    public class DDZ_Straight : HandType
    {
        public CardPoint Point { get; }

        public int Length { get; }

        private DDZ_Straight(CompressedCardList compressedList) : base(compressedList)
        {
            Point = compressedList[0].Point;
            Length = compressedList.Count;
        }

        public int CompareTo(DDZ_Straight other)
        {
            if (Length != other.Length) return 0;
            return Point - other.Point;
        }

        public static HandType Creator(CompressedCardList compressedList)
        {
            if (!compressedList.SortedCount.AllTheSame(1) || 
                !compressedList.CheckStraight(5))
                return ErrorType.ErrorTypeInstance;
            return new DDZ_Straight(compressedList);
        }
    }
}
