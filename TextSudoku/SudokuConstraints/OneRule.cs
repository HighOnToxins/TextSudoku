
namespace TextSudoku.SudokuConstraints;

internal sealed class OneRule: ISudokuRule {
    //TODO: Change from IsAllowed to IsValid.
    public bool IsAllowed(int c1, int r1, char symbol1, int c2, int r2, char symbol2) =>
        (c1 == c2 && r1 == r2) || symbol1 != symbol2;

    //TODO: Try to use contains, instead of is valid. Or maybe both. And an or based contains. Assignment and con-/disjunction?

    /* METHOD FOR CHECKING THE ENTIRE SUDOKU
    public bool IsAllowed(IReadOnlySet<SymbolCell> cells){
        
        for(int i = 0; i < cells.Count; i++){
            for(int j = i+1; j < cells.Count; j++) {

                SymbolCell cellA = cells.ElementAt(i);
                SymbolCell cellB = cells.ElementAt(j);

                if(!IsAllowed(cellA.Column, cellA.Row, cellA.Symbol, cellB.Column, cellB.Row, cellB.Symbol)) {
                    return false;
                }

            }
        }

        return true;
    }
    */
}