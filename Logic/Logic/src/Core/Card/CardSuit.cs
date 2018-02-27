using System;
using System.Collections.Generic;

namespace Logic.Core
{
    public class CardSuit : IComparable<CardSuit>
    {
        public static CardSuit Spade = new CardSuit();
        public static CardSuit Heart = new CardSuit();
        public static CardSuit Club = new CardSuit();
        public static CardSuit Diamond = new CardSuit();

        public static List<CardSuit> OrderList = new List<CardSuit>(4) { Spade, Heart, Club, Diamond };

        public int CompareTo(CardSuit other)
        {
            return this - other;
        }

        public static int operator -(CardSuit a, CardSuit b)
        {
            return OrderList.IndexOf(a) - OrderList.IndexOf(b);
        }

        public static Card operator &(CardSuit s, CardPoint p)
        {
            return new Card(s, p);
        }

        public override string ToString()
        {
            if (this == Spade) return "S";
            if (this == Heart) return "H";
            if (this == Club) return "C";
            if (this == Diamond) return "D";
            return "error";
        }

        private CardSuit() { }
    }
}