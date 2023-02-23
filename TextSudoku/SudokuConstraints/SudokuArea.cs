
using System.Collections.Immutable;

namespace TextSudoku.SudokuConstraints;

internal sealed class SudokuArea {

    public record Cell(int Column, int Row);

    private readonly IReadOnlySet<Cell> _cells;

    public SudokuArea(int c1, int r1, int c2, int r2) {

        List<Cell> cells = new();
        for(int c = c1; c <= c2; c++) {
            for(int r = r1; r <= r2; r++) {
                cells.Add(new Cell(c, r));
            }
        }
        _cells = cells.ToImmutableHashSet();

    }

    public bool Contains(int c, int r) => _cells.Contains(new Cell(c, r));

    public IReadOnlyList<SymbolCell> GetFilledCells(char[,] board, IReadOnlySet<char> symbols) {
        List<SymbolCell> symbolCells = new();
        foreach(Cell cell in _cells) {
            if(symbols.Contains(board[cell.Column, cell.Row])) {
                symbolCells.Add(new SymbolCell(cell.Column, cell.Row, board[cell.Column, cell.Row]));
            }
        }
        return symbolCells;
    }

    public IReadOnlyList<SymbolCell> GetFilledCells(List<char>[,] boardCandidates, IReadOnlySet<char> symbols) {
        List<SymbolCell> symbolCells = new();
        foreach(Cell cell in _cells) {
            for(int i = 0; i < boardCandidates[cell.Column, cell.Row].Count; i++) {
                if(symbols.Contains(boardCandidates[cell.Column, cell.Row][i])) {
                    symbolCells.Add(new SymbolCell(cell.Column, cell.Row, boardCandidates[cell.Column, cell.Row][i]));
                }
            }
        }
        return symbolCells;
    }
}