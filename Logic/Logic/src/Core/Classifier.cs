using System.Collections.Generic;

namespace Logic.Core
{
    public delegate HandType TypeCreator(CompressedCardList compressedCards);

    public abstract class Classifier
    {
        protected abstract IEnumerable<TypeCreator> GetTypeCreators();

        public virtual HandType Classify(CompressedCardList compressedCards)
        {
            var errorInstance = ErrorType.ErrorTypeInstance;

            foreach (var creator in GetTypeCreators())
            {
                HandType handType = creator(compressedCards);
                if (handType != errorInstance) return handType;
            }

            return errorInstance;
        }
    }
}
