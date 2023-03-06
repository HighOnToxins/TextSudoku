
namespace TextSudoku.SudokuConstraints;

internal record SymbolCell(int Column, int Row, char Symbol);

internal sealed class SudokuConstraint {

    private readonly SudokuArea _area;
    private readonly ISudokuRule _rule;

    public SudokuConstraint(SudokuArea area, ISudokuRule rule) {
        _area = area;
        _rule = rule;
    }

    public bool IsAllowed(int c, int r, char symbol, char[,] board, IReadOnlySet<char> symbols) {
        if(!_area.Contains(c, r)) return true;
        IReadOnlySet<SymbolCell> cells = _area.GetFilledCells(board, symbols);
        return DetermineIsAllowed(c, r, symbol, cells);
    }

    public bool IsAllowed(int c, int r, char symbol, IReadOnlyList<char>[,] candidates, IReadOnlySet<char> symbols) {
        if(!_area.Contains(c, r)) return true;
        IReadOnlySet<SymbolCell> cells = _area.GetFilledCells(candidates, symbols);
        return DetermineIsAllowed(c, r, symbol, cells);
    }

    private bool DetermineIsAllowed(int c, int r, char symbol, IReadOnlySet<SymbolCell> cells) {
        foreach(SymbolCell cell in cells) {
            if(!_rule.IsAllowed(c, r, symbol, cell.Column, cell.Row, cell.Symbol)) {
                return false;
            }
        }

        return true;
    }
}