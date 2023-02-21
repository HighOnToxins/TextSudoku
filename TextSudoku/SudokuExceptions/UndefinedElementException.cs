using System.Runtime.Serialization;

namespace TextSudoku.SudokuExceptions
{
    [Serializable]
    internal class UndefinedElementException : Exception
    {
        private char value;

        public UndefinedElementException()
        {
        }

        public UndefinedElementException(string? message) : base(message)
        {
        }

        public UndefinedElementException(char value) {
            this.value = value;
        }

        public UndefinedElementException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UndefinedElementException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}