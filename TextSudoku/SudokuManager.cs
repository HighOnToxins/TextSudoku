
using Candidates = System.Collections.Generic.List<char>;

namespace TextSudoku;

internal sealed class SudokuManager {

    private readonly Candidates[,] _candidates;
    private readonly SudokuBoard _board;

    public SudokuManager(SudokuBoard board) { 
        _board = board;
        _candidates = GenerateDefaultCandidates(board);
    }

    private static Candidates[,] GenerateDefaultCandidates(SudokuBoard board) {

        Candidates[,] candidates = new Candidates[board.Width, board.Height];

        for(int c = 0; c < candidates.GetLength(0); c++) {
            for(int r = 0; r < candidates.GetLength(1); r++) {
                candidates[c, r] = new();
                
                for(int i = 0; i < board.Symbols.Count; i++) {
                    candidates[c, r].Add(board.Symbols[i]);
                }
            }
        }

        return candidates;
    }



}