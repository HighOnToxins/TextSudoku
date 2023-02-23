
using System.Data;
using TextSudoku.SudokuConstraints;
using TextSudoku.SudokuExceptions;

namespace TextSudoku;

internal sealed class SudokuBoard {

    //TODO: make board resizeable

    public const ushort BOARD_SIZE = 9;

    public IReadOnlyList<char> Symbols { get; private init; }

    private readonly bool[,] _isGiven;
    private readonly char[,] _board;

    public IReadOnlyList<SudokuConstraint> Constraints { get; }

    public int Width { get => _board.GetLength(0); }
    public int Height { get => _board.GetLength(1); }

    public char this[int column, int row] {
        get {
            return _board[column, row];
        }
        set {
            if((value < '0' && value > '9') || value != ' ') {
                throw new UndefinedElementException(value);
            }

            if(column < 0 || column >= BOARD_SIZE || row < 0 || column >= BOARD_SIZE) {
                throw new OutsideOfBoardException(column, row, BOARD_SIZE);
            }

            if(_isGiven[column, row]) {
                throw new UnassignableToGivenException(column, row, _board[column, row]);
            }

            _board[column, row] = value;
        }
    }

    public SudokuBoard(char[,] board, params SudokuConstraint[] constraint) {
        if(board.GetLength(0) != BOARD_SIZE || board.GetLength(1) != BOARD_SIZE) {
            throw new IncorectBoardSizeException(board.GetLength(0), board.GetLength(1), BOARD_SIZE);
        }

        Symbols = GetDefaultSymbols();
        _board = board;
        _isGiven = DetermineGivenByNewBoard(board);
        Constraints = constraint;
    }

    private static IReadOnlyList<char> GetDefaultSymbols() {
        List<char> symbols = new();
        for(char s = '1'; s <= '9'; s++) {
            symbols.Add(s);
        }
        return symbols;
    }

    private static bool[,] DetermineGivenByNewBoard(char[,] newBoard) {
        bool[,] assignable = new bool[newBoard.GetLength(0), newBoard.GetLength(1)];
        for(int c = 0; c < newBoard.GetLength(0); c++) {
            for(int r = 0; r < newBoard.GetLength(1); r++) {
                assignable[c, r] = newBoard[c, r] == ' ';
            }
        }
        return assignable;
    }

    public bool IsSolved() {
        for(int c = 0; c < Width; c++) {
            for(int r = 0; r < Height; r++) {
                foreach(SudokuConstraint constraint in Constraints) {
                    if(constraint.GetConstraintAt(c, r, _board, Symbols).Contains(_board[c, r])) {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    public bool IsCompleted() {
        for(int c = 0; c < Width; c++) {
            for(int r = 0; r < Height; r++) {
                if(IsEmptyAt(c, r)) {
                    return false;
                }
            }
        }
        return true;
    }

    public IEnumerable<char> GetConstrainsAt(int c, int r) =>
        Constraints.SelectMany(con => con.GetConstraintAt(c, r, _board, Symbols));

    public bool IsEmptyAt(int c, int r) => this[c, r] != 0 && this[c, r] != ' ';
}