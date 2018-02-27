namespace Logic.Core
{
    public class ErrorType : HandType
    {
        public static ErrorType ErrorTypeInstance = new ErrorType();

        private ErrorType() : base(CompressedCardList.EmptyCompressedCardList)
        {
        }
    }
}
