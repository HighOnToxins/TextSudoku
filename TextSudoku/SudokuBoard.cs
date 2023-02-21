
using TextSudoku.SudokuExceptions;

namespace TextSudoku;

internal sealed class SudokuBoard {

    //TODO: make board resizeable and other symbols, and changeable rules?

    public const ushort BOARD_SIZE = 9;

    public IReadOnlyList<char> Symbols { get; private init; }

    private readonly bool[,] _isGiven;
    private readonly char[,] _board;

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

    public SudokuBoard(char[,] board) {
        if(board.GetLength(0) != BOARD_SIZE || board.GetLength(1) != BOARD_SIZE) {
            throw new IncorectBoardSizeException(board.GetLength(0), board.GetLength(1), BOARD_SIZE);
        }

        Symbols = GetDefaultSymbols();
        _board = board;
        _isGiven = DetermineGivenByNewBoard(board);
    }

    private static IReadOnlyList<char> GetDefaultSymbols() {
        List<char> symbols = new();
        for(char s = '1'; s <= '9'; s++) {
            symbols.Add(s);
        }
        return symbols;
    }

    private static bool[,] DetermineGivenByNewBoard(char[,] newBoard) {
        bool[,] assignable = new bool[BOARD_SIZE, BOARD_SIZE];
        for(int c = 0; c < newBoard.GetLength(0); c++) {
            for(int r = 0; r < newBoard.GetLength(1); r++) {
                assignable[c, r] = newBoard[c, r] == ' ';
            }
        }
        return assignable;
    }

    public bool IsSatisfactory() {
        throw new NotImplementedException();
    }

    public bool IsSolved() {
        throw new NotImplementedException();
    }

}