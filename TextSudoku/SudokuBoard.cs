
using System.Xml.Linq;

namespace TextSudoku;

internal sealed class SudokuBoard {

    public const ushort BOARD_SIZE = 9;

    private readonly bool[,] _isGiven;
    private readonly char[,] _board;

    public char this[int column, int row] {
        get {
            return _board[column, row];
        }
        set {
            if((value < '0' && value > '9') || value != ' ') {
                //TODO: Add incorectElementException.
            }

            if(column < 0 || column >= BOARD_SIZE || row < 0 || column >= BOARD_SIZE) {
                //TODO: Add outsideOfBoardException
            }

            if(_isGiven[column, row]) {
                //TODO: Add assignToGivenException
            }

            _board[column, row] = value;
        }
    }

    public SudokuBoard(char[,] board) {
        if(board.GetLength(0) != BOARD_SIZE || board.GetLength(1) != BOARD_SIZE) {
            //TODO: Add incorectBoardSizeException
        }

        _board = board;
        _isGiven = DetermineGivenByNewBoard(board);
    }

    public SudokuBoard() {
        _board = new char[BOARD_SIZE, BOARD_SIZE];
        _isGiven = new bool[BOARD_SIZE, BOARD_SIZE];
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

    public void GenerateNewBoard() {
        //TODO: Add sudoku generator.
    }

    public bool IsSatisfactory() {
        throw new NotImplementedException();
    }

    public bool IsSolved() {
        throw new NotImplementedException();
    }

}