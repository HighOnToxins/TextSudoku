
using static TextSudoku.SudokuConstraints.SudokuArea;

namespace TextSudoku.SudokuConstraints;

internal record SymbolCell(int Column, int Row, char Symbol);

internal sealed class SudokuConstraint {

    private readonly SudokuArea _area;
    private readonly ISudokuRule _rule;

    public SudokuConstraint(SudokuArea area, ISudokuRule rule) {
        _area = area;
        _rule = rule;
    }

    public IEnumerable<char> GetConstraintAt(int c, int r, char[,] board, IReadOnlyList<char> symbols) {
        if(!_area.Contains(c, r)) return Array.Empty<char>();

        IReadOnlyList<SymbolCell> cells = _area.GetCells(board);
        return GetConstrainedCells(c, r, cells, symbols);
    }

    public IEnumerable<char> GetConstraintAt(int c, int r, List<char>[,] boardCandidates, IReadOnlyList<char> symbols) {
        if(!_area.Contains(c, r)) return Array.Empty<char>();

        IReadOnlyList<SymbolCell> cells = _area.GetCells(boardCandidates);
        return GetConstrainedCells(c, r, cells, symbols);
    }

    private IEnumerable<char> GetConstrainedCells(int c, int r, IReadOnlyList<SymbolCell> cells, IReadOnlyList<char> symbols) {

        for(int i = 0; i < symbols.Count; i++) {
            foreach(SymbolCell cell in cells) {
                if(!_rule.IsAllowed(c, r, symbols[i], cell.Row, cell.Column, cell.Symbol)) {
                    yield return symbols[i];
                }
            }
        }
    }

    public bool IsAllowed(int c, int r, char[,] board) {
        if(!_area.Contains(c, r)) return true;

        IReadOnlyList<SymbolCell> cells = _area.GetCells(board);
        foreach(SymbolCell cell in cells) {
            if(!_rule.IsAllowed(c, r, board[c, r], cell.Column, cell.Row, cell.Symbol)) {
                return false;
            }
        }
        return true;
    }

}