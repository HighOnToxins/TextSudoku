
using TextSudoku.SudokuConstraints;
using Candidates = System.Collections.Generic.List<char>;

namespace TextSudoku;

public sealed class SudokuManager {

    private Candidates[,] _candidates;
    private Candidates[,] _shortTermCandidates;
    private readonly SudokuBoard _board;

    public SudokuManager(SudokuBoard board) {
        _board = board;
        _candidates = GenerateDefaultCandidates(board);
        _shortTermCandidates = GenerateDefaultCandidates(_board);
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
        bool aCandidateHasBeenEleminated = false;
        _shortTermCandidates = new Candidates[_candidates.GetLength(0), _candidates.GetLength(1)];
       
        for(int c = 0; c < _board.Width; c++) {
            for(int r = 0; r < _board.Height; r++) {
                _shortTermCandidates[c, r] = _candidates[c, r].ToList();

                //board based elemination
                if(!_board.IsEmptyAt(c, r)) {
                    _shortTermCandidates[c, r].Clear();
                    _candidates[c, r].Clear();
                    continue;
                }

                for(int i = 0; i < _candidates[c, r].Count; i++) {
                    if(!_board.IsAllowed(c, r, _candidates[c, r][i]) && _candidates[c, r].Remove(_candidates[c, r][i])) {
                        i--;
                        aCandidateHasBeenEleminated = true;
                    }
                }

                //candidate based elemination
                for(int i = 0; i < _shortTermCandidates[c, r].Count; i++){
                    if(!_board.IsAllowed(c, r, _shortTermCandidates[c, r][i]) && _candidates[c, r].Remove(_shortTermCandidates[c, r][i])) {
                        _shortTermCandidates[c, r].Remove(_shortTermCandidates[c, r][i]);
                        i--;
                        continue;
                    }
                    foreach(SudokuConstraint constraint in _board.Constraints) {
                        if(!constraint.IsAllowed(c, r, _shortTermCandidates[c, r][i], _candidates, _board.Symbols)) {
                            _shortTermCandidates[c, r].Remove(_shortTermCandidates[c, r][i]);
                            i--;
                            break;
                        }
                    }
                }
            }
        }

        return aCandidateHasBeenEleminated;
    }

    public bool AddElementsToBoard() {
        bool anElementHasBeenAdded = false;

        for(int c = 0; c < _board.Width; c++) {
            for(int r = 0; r < _board.Height; r++) {
                if(_candidates[c, r].Count == 1) {
                    _board[c, r] = _candidates[c, r][0];
                    anElementHasBeenAdded = true;
                }

                if(_shortTermCandidates[c, r].Count == 1) {
                    _board[c, r] = _shortTermCandidates[c, r][0];
                    anElementHasBeenAdded = true;
                }
            }
        }

        return anElementHasBeenAdded;
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
        _candidates = GenerateDefaultCandidates(_board);
        _shortTermCandidates = GenerateDefaultCandidates(_board);
    }

}