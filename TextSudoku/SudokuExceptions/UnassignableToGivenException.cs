using System.Runtime.Serialization;

namespace TextSudoku.SudokuExceptions {
    [Serializable]
    internal class UnassignableToGivenException: Exception {
        private int column;
        private int row;
        private char v;

        public UnassignableToGivenException() {
        }

        public UnassignableToGivenException(string? message) : base(message) {
        }

        public UnassignableToGivenException(string? message, Exception? innerException) : base(message, innerException) {
        }

        public UnassignableToGivenException(int column, int row, char v) {
            this.column = column;
            this.row = row;
            this.v = v;
        }

        protected UnassignableToGivenException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}