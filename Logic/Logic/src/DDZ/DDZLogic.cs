using System.Collections.Generic;
using System.Linq;
using Logic.Core;

namespace Logic.DDZ
{
    public class DDZLogic : IGameLogic
    {
        private HandType prev2, prev1;
        private static DDZClassifier classifier = new DDZClassifier();

        public HandType Current { get; private set; }

        public bool Accept(IEnumerable<Card> cards)
        {
            IList<Card> cardList = cards.ToList();
            if (cardList.Count == 0) return AcceptEmpty();

            Hand hand = new Hand(cardList);
            CompressedCardList compression = new CompressedCardList(hand);
            HandType type = classifier.Classify(compression);
            return Accept(type);
        }

        private bool AcceptEmpty()
        {
            return Accept(handType: null);
        }

        private bool Accept(HandType handType)
        {
            Current = handType;

            if (handType == ErrorType.ErrorTypeInstance) return false;

            if (Current != null)
            {
                if (prev2 == null && prev1 == null) return AcceptAndShift();
                HandType lastHand = prev1 ?? prev2;
                if (Current > lastHand) return AcceptAndShift();
                else return false;
            }
            else
            {
                if (prev2 == null && prev1 == null) return false;
                else return AcceptAndShift();
            }
        }

        private bool AcceptAndShift()
        {
            prev2 = prev1;
            prev1 = Current;
            Current = null;
            return true;
        }

        public void Reset()
        {
            prev1 = prev2 = Current = null;
        }
    }
}
