
using Candidates = System.Collections.Generic.List<char>;

namespace TextSudoku;

public sealed class SudokuManager {

    private Candidates[,] _boardCandidates;
    private Candidates[,] _candidateCandidates;
    private readonly SudokuBoard _board;

    public SudokuManager(SudokuBoard board) {
        _board = board;
        _boardCandidates = GenerateDefaultCandidates(board);
        _candidateCandidates = GenerateDefaultCandidates(_board);
    }

    private static Candidates[,] GenerateDefaultCandidates(SudokuBoard board) {

        Candidates[,] candidates = new Candidates[board.Width, board.Height];

        for(int c = 0; c < candidates.GetLength(0); c++) {
            for(int r = 0; r < candidates.GetLength(1); r++) {
                candidates[c, r] = new();

                foreach(char symbol in board.Symbols) {
                    candidates[c, r].Add(symbol);
                }
            }
        }

        return candidates;
    }

    public bool EleminateCandidates() {
        bool eleminatedAtLeastOneCandidate = false;

        for(int c = 0; c < _board.Width; c++) {
            for(int r = 0; r < _board.Height; r++) {
                if(!_board.IsEmptyAt(c, r)) {
                    _candidateCandidates[c, r].Clear();
                    _boardCandidates[c, r].Clear();
                    continue;
                }

                //board based elemination
                foreach(char candidate in _board.GetConstrainsAt(c, r)) {
                    if(_boardCandidates[c, r].Remove(candidate)) {
                        eleminatedAtLeastOneCandidate = true;
                    }
                }

                foreach(char candidate in _board.Constraints.SelectMany(con => con.GetConstraintAt(c, r, _boardCandidates, _board.Symbols))) {
                    if(_candidateCandidates[c, r].Remove(candidate)) {
                        eleminatedAtLeastOneCandidate = true;
                    }
                }
            }
        }

        return eleminatedAtLeastOneCandidate;
    }

    public bool AddElementsToBoard() {
        bool addedAnyElements = false;

        for(int c = 0; c < _board.Width; c++) {
            for(int r = 0; r < _board.Height; r++) {
                if(_boardCandidates[c, r].Count == 1) {
                    _board[c, r] = _boardCandidates[c, r][0];
                    addedAnyElements = true;
                }

                if(_candidateCandidates[c, r].Count == 1) {
                    _board[c, r] = _candidateCandidates[c, r][0];
                    addedAnyElements = true;
                }
            }
        }

        return addedAnyElements;
    }

    public bool SolveBoard() {

        bool flag = true;
        while(flag) {
            bool f1 = EleminateCandidates();
            bool f2 = AddElementsToBoard();
            flag = f1 || f2;
        }

        //TODO: Add guessing.

        return _board.IsSolved();
    }

    public void Reset() {
        _boardCandidates = GenerateDefaultCandidates(_board);
        _candidateCandidates = GenerateDefaultCandidates(_board);
    }

}