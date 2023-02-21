using System.Runtime.Serialization;

namespace TextSudoku.SudokuExceptions
{
    [Serializable]
    internal class IncorectBoardSizeException : Exception
    {
        private int v1;
        private int v2;
        private ushort bOARD_SIZE;

        public IncorectBoardSizeException()
        {
        }

        public IncorectBoardSizeException(string? message) : base(message)
        {
        }

        public IncorectBoardSizeException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public IncorectBoardSizeException(int v1, int v2, ushort bOARD_SIZE)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.bOARD_SIZE = bOARD_SIZE;
        }

        protected IncorectBoardSizeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}