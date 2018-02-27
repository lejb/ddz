using System.Linq;
using Logic.Core;

namespace Logic.DDZ.Types
{
    public class DDZ_Plane2 : HandType
    {
        public CardPoint Point { get; }

        public int Length { get; }

        private DDZ_Plane2(CompressedCardList compressedList, CompressedCardList plane) : base(compressedList)
        {
            Point = plane[0].Point;
            Length = plane.Count;
        }

        public int CompareTo(DDZ_Plane2 other)
        {
            if (Length != other.Length) return 0;
            return Point - other.Point;
        }

        public static HandType Creator(CompressedCardList compressedList)
        {
            var plane = new CompressedCardList(compressedList.Where(c => c.Count == 3));
            if (!plane.CheckStraight(2))
                return ErrorType.ErrorTypeInstance;
            if (compressedList.Where(c => c.Count >= 4 || c.Count < 2).Count() > 0)
                return ErrorType.ErrorTypeInstance;
            if (compressedList.Where(c => c.Count == 2).Count() != plane.Count)
                return ErrorType.ErrorTypeInstance;
            return new DDZ_Plane2(compressedList, plane);
        }
    }
}
