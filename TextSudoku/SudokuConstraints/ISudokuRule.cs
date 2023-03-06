
namespace TextSudoku.SudokuConstraints;

internal interface ISudokuRule {
    public bool IsAllowed(int c1, int r1, char symbol1, int c2, int r2, char symbol2);
}

//TODO: Add a method for checking if the entire area is valid.
//TODO: Find a way to determine must haves.
//TODO: 