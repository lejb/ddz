using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Logic.Core
{
    public class Card : IComparable<Card>
    {
        public CardSuit Suit { get; }

        public CardPoint Point { get; }

        public Card(CardSuit suit, CardPoint point)
        {
            Contract.Requires(point != null);
            Contract.Requires(suit != null || point == CardPoint.JOKER || point == CardPoint.joker);

            Suit = suit;
            Point = point;
        }

        int IComparable<Card>.CompareTo(Card other)
        {
            return this - other;
        }

        public static int operator -(Card c1, Card c2)
        {
            return c1.Point - c2.Point;
        }

        public override string ToString()
        {
            string point = Point.ToString();
            if (point == "jo" || point == "JO") return point;
            return Suit.ToString() + point;
        }
    }
}