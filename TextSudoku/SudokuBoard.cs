using TextSudoku.SudokuConstraints;
using TextSudoku.SudokuExceptions;

namespace TextSudoku;

public sealed class SudokuBoard {

    //TODO: make board resizeable

    public const ushort BOARD_SIZE = 9;

    public IReadOnlySet<char> Symbols { get; private init; }

    internal IReadOnlyList<SudokuConstraint> Constraints { get; }

    private readonly bool[,] _isGiven;
    private readonly char[,] _board;

    public int Width { get => _board.GetLength(0); }
    public int Height { get => _board.GetLength(1); }

    public char this[int column, int row] {
        get {
            return _board[column, row];
        }
        set {
            if(value < '0' && value > '9' && !char.IsWhiteSpace(value)) {
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

    public char this[Index column, int row] => this[column.IsFromEnd ? BOARD_SIZE - column.Value - 1 : column.Value, row];
    public char this[int column, Index row] => this[column, row.IsFromEnd ? BOARD_SIZE - row.Value - 1 : row.Value];
    public char this[Index column, Index row] => this[column.IsFromEnd ? BOARD_SIZE - column.Value - 1 : column.Value, row.IsFromEnd ? BOARD_SIZE - row.Value - 1 : row.Value];

    internal SudokuBoard(char[,] board, params SudokuConstraint[] constraint) : this(board) {
        Constraints = constraint;
    }

    public SudokuBoard(char[,] board) {
        if(board.GetLength(0) != BOARD_SIZE || board.GetLength(1) != BOARD_SIZE) {
            throw new IncorectBoardSizeException(board.GetLength(0), board.GetLength(1), BOARD_SIZE);
        }

        Symbols = GetDefaultSymbols();
        _board = board;
        _isGiven = DetermineGivenByNewBoard(board);
        Constraints = DefaultConstraints();
    }

    private static IReadOnlyList<SudokuConstraint> DefaultConstraints() {
        List<SudokuConstraint> constraints = new();

        //adding boxes
        for(int i = 0; i < BOARD_SIZE; i += 3) {
            for(int j = 0; j < BOARD_SIZE; j += 3) {
                constraints.Add(new SudokuConstraint(
                    new SudokuArea(i * BOARD_SIZE / 3, j * BOARD_SIZE / 3, (i + 1) * BOARD_SIZE / 3 - 1, (j + 1) * BOARD_SIZE / 3 - 1),
                    new OneRule()
                ));
            }
        }

        //adding columns
        for(int i = 0; i < BOARD_SIZE; i++) {
            constraints.Add(new SudokuConstraint(
                new SudokuArea(i, 0, i, BOARD_SIZE - 1),
                new OneRule()
            ));
        }

        //adding rows
        for(int i = 0; i < BOARD_SIZE; i++) {
            constraints.Add(new SudokuConstraint(
                new SudokuArea(i, 0, i, BOARD_SIZE - 1),
                new OneRule()
            ));
        }

        return constraints;
    }

    private static IReadOnlySet<char> GetDefaultSymbols() {
        HashSet<char> symbols = new();
        for(char s = '1'; s <= '9'; s++) {
            symbols.Add(s);
        }
        return symbols;
    }

    private static bool[,] DetermineGivenByNewBoard(char[,] newBoard) {
        bool[,] isGiven = new bool[newBoard.GetLength(0), newBoard.GetLength(1)];
        for(int c = 0; c < newBoard.GetLength(0); c++) {
            for(int r = 0; r < newBoard.GetLength(1); r++) {
                isGiven[c, r] = !char.IsWhiteSpace(newBoard[c, r]);
            }
        }
        return isGiven;
    }

    public bool IsSolved() {
        if(!IsCompleted()) {
            return false;
        }

        for(int c = 0; c < Width; c++) {
            for(int r = 0; r < Height; r++) {
                foreach(SudokuConstraint constraint in Constraints) {
                    if(!constraint.IsAllowed(c, r, _board[c, r], _board, Symbols)) {
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

    public bool IsEmptyAt(int c, int r) => char.IsWhiteSpace(this[c, r]);

    internal bool IsAllowed(int c, int r, char candidate) {
        foreach(SudokuConstraint constraint in Constraints) {
            if(!constraint.IsAllowed(c, r, candidate, _board, Symbols)) {
                return false;
            }
        }
        return true;
    }
}