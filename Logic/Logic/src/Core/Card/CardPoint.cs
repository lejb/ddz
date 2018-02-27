using System;
using System.Collections.Generic;

namespace Logic.Core
{
    public class CardPoint : IComparable<CardPoint>
    {
        public static CardPoint A = new CardPoint();
        public static CardPoint _2 = new CardPoint();
        public static CardPoint _3 = new CardPoint();
        public static CardPoint _4 = new CardPoint();
        public static CardPoint _5 = new CardPoint();
        public static CardPoint _6 = new CardPoint();
        public static CardPoint _7 = new CardPoint();
        public static CardPoint _8 = new CardPoint();
        public static CardPoint _9 = new CardPoint();
        public static CardPoint _10 = new CardPoint();
        public static CardPoint J = new CardPoint();
        public static CardPoint Q = new CardPoint();
        public static CardPoint K = new CardPoint();
        public static CardPoint joker = new CardPoint();
        public static CardPoint JOKER = new CardPoint();

        public static List<CardPoint> OrderList = new List<CardPoint>(15)
        {
            _3, _4, _5, _6, _7, _8, _9, _10, J, Q, K, A, _2, joker, JOKER
        };

        public int CompareTo(CardPoint other)
        {
            return this - other;
        }

        public static int operator -(CardPoint a, CardPoint b)
        {
            return OrderList.IndexOf(a) - OrderList.IndexOf(b);
        }

        public static Card operator &(CardPoint p, CardSuit s)
        {
            return new Card(s, p);
        }

        public override string ToString()
        {
            if (this == A) return "A";
            if (this == _2) return "2";
            if (this == _3) return "3";
            if (this == _4) return "4";
            if (this == _5) return "5";
            if (this == _6) return "6";
            if (this == _7) return "7";
            if (this == _8) return "8";
            if (this == _9) return "9";
            if (this == _10) return "0";
            if (this == J) return "J";
            if (this == Q) return "Q";
            if (this == K) return "K";
            if (this == joker) return "jo";
            if (this == JOKER) return "JO";
            return "error";
        }

        private CardPoint() { }
    }
}