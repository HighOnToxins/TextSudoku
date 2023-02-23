
using System.Collections.Generic;
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

    public IEnumerable<char> GetConstraintAt(int c, int r, char[,] board, IReadOnlySet<char> symbols) {
        if(!_area.Contains(c, r)) return Array.Empty<char>();

        IReadOnlyList<SymbolCell> cells = _area.GetFilledCells(board, symbols);
        return GetConstrainedCells(c, r, cells, symbols);
    }

    public IEnumerable<char> GetConstraintAt(int c, int r, List<char>[,] boardCandidates, IReadOnlySet<char> symbols) {
        if(!_area.Contains(c, r)) return Array.Empty<char>();

        IReadOnlyList<SymbolCell> cells = _area.GetFilledCells(boardCandidates, symbols);
        return GetConstrainedCells(c, r, cells, symbols);
    }

    private IEnumerable<char> GetConstrainedCells(int c, int r, IReadOnlyList<SymbolCell> cells, IReadOnlySet<char> symbols) {
        foreach(char symbol in symbols) {
            foreach(SymbolCell cell in cells) {
                if(!_rule.IsAllowed(c, r, symbol, cell.Row, cell.Column, cell.Symbol)) {
                    yield return symbol;
                }
            }
        }
    }

    public bool IsAllowed(int c, int r, char[,] board, IReadOnlySet<char> symbols) {
        if(!_area.Contains(c, r)) return true;

        IReadOnlyList<SymbolCell> cells = _area.GetFilledCells(board, symbols);
        foreach(SymbolCell cell in cells) {
            if(!_rule.IsAllowed(c, r, board[c, r], cell.Column, cell.Row, cell.Symbol)) {
                return false;
            }
        }
        return true;
    }

}