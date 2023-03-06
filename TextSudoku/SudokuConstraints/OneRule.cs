
namespace TextSudoku.SudokuConstraints;

internal sealed class OneRule: ISudokuRule {
    //TODO: Change from IsAllowed to IsValid.
    public bool IsAllowed(int c1, int r1, char symbol1, int c2, int r2, char symbol2) =>
        (c1 == c2 && r1 == r2) || symbol1 != symbol2;

}