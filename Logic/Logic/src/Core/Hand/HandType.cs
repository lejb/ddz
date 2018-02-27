using System;

namespace Logic.Core
{
    public abstract class HandType : IComparable<HandType>
    {
        public CompressedCardList CompressedList { get; }

        public HandType(CompressedCardList compressedList)
        {
            CompressedList = compressedList;
        }

        public virtual int CompareTo(HandType other)
        {
            return 0;
        }

        public virtual int CompareTo(ErrorType other)
        {
            return 0;
        }

        public static bool operator >(HandType hand1, HandType hand2)
        {
            return Compare(hand1, hand2) > 0;
        }

        public static bool operator <(HandType hand1, HandType hand2)
        {
            return Compare(hand1, hand2) <= 0;
        }

        public static int Compare(HandType hand1, HandType hand2)
        {
            dynamic d1 = hand1;
            dynamic d2 = hand2;
            return d1.CompareTo(d2);
        }
    }
}
