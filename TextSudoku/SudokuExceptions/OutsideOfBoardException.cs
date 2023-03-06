using System.Runtime.Serialization;

namespace TextSudoku.SudokuExceptions {
    [Serializable]
    internal class OutsideOfBoardException: Exception {
        private int column;
        private int row;
        private ushort bOARD_SIZE;

        public OutsideOfBoardException() {
        }

        public OutsideOfBoardException(string? message) : base(message) {
        }

        public OutsideOfBoardException(string? message, Exception? innerException) : base(message, innerException) {
        }

        public OutsideOfBoardException(int column, int row, ushort bOARD_SIZE) {
            this.column = column;
            this.row = row;
            this.bOARD_SIZE = bOARD_SIZE;
        }

        protected OutsideOfBoardException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}