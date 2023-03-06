using TextSudoku;

namespace SudokuTest {
    public class SudokuTest {

        [Test]
        public void SolvingWorks() {

            char[,] board = {
                { ' ', ' ', ' ',   '6', ' ', ' ',   '1', ' ', '7',},

                { '6', '8', ' ',   '9', '5', '1',   '3', ' ', ' ',},

                { ' ', ' ', '3',   ' ', ' ', '2',   '5', '6', '8',},


                { ' ', '4', ' ',   '8', '1', ' ',   ' ', '2', ' ',},

                { ' ', ' ', ' ',   ' ', ' ', ' ',   '8', '5', ' ',},

                { ' ', '9', ' ',   ' ', '6', '5',   ' ', '7', '3',},


                { '4', ' ', '9',   ' ', ' ', '3',   ' ', '8', '5',},

                { '1', '6', '2',   ' ', ' ', '9',   ' ', '3', ' ',},

                { '5', ' ', ' ',   '7', ' ', '6',   ' ', ' ', ' ',},
            };

            board = Transpose(board);

            SudokuBoard sudoku = new(board);
            SudokuManager solver = new(sudoku);

            Assert.That(solver.SolveBoard());

        }

        [Test]
        public void CanFindSymbol() {

            char[,] board = {
                { '1', '2', '3', '4', '5', '6', '7', '8', '9',},

                { '4', '5', '6', '7', '8', '9', '1', '2', '3',},

                { '7', '8', '9', '1', '2', '3', '4', '5', '6',},

                { '3', '1', '2', '6', '4', '5', '9', '7', '8',},

                { '6', '4', '5', '9', ' ', '8', '3', '1', '2',},

                { '9', '7', '8', '3', '1', '2', '6', '4', '5',},

                { '2', '3', '1', '5', '6', '4', '8', '9', '7',},

                { '5', '6', '4', '8', '9', '7', '2', '3', '1',},

                { '8', '9', '7', '2', '3', '1', '5', '6', '4',},
            };

            board = Transpose(board);

            SudokuBoard sudoku = new(board);
            SudokuManager solver = new(sudoku);

            Assert.That(solver.SolveBoard());

        }

        [Test]
        public void IsNotSolved() {

            char[,] board = {
                { '1', '2', '3', '4', '5', '6', '7', '8', '9',},

                { '4', '5', '6', '7', '8', '9', '1', '2', '3',},

                { '7', '8', '9', '1', '2', '3', '4', '5', '6',},

                { '3', '1', '2', '6', '4', '5', '9', '7', '8',},

                { '6', '4', '5', '9', '6', '8', '3', '1', '2',},

                { '9', '7', '8', '3', '1', '2', '6', '4', '5',},

                { '2', '3', '1', '5', '6', '4', '8', '9', '7',},

                { '5', '6', '4', '8', '9', '7', '2', '3', '1',},

                { '8', '9', '7', '2', '3', '1', '5', '6', '4',},
            };

            board = Transpose(board);

            SudokuBoard sudoku = new(board);

            Assert.That(sudoku.IsSolved(), Is.False);

        }

        [Test]
        public void IsSolved() {

            char[,] board = {
                { '1', '2', '3', '4', '5', '6', '7', '8', '9',},

                { '4', '5', '6', '7', '8', '9', '1', '2', '3',},

                { '7', '8', '9', '1', '2', '3', '4', '5', '6',},

                { '3', '1', '2', '6', '4', '5', '9', '7', '8',},

                { '6', '4', '5', '9', '7', '8', '3', '1', '2',},

                { '9', '7', '8', '3', '1', '2', '6', '4', '5',},

                { '2', '3', '1', '5', '6', '4', '8', '9', '7',},

                { '5', '6', '4', '8', '9', '7', '2', '3', '1',},

                { '8', '9', '7', '2', '3', '1', '5', '6', '4',},
            };

            board = Transpose(board);

            SudokuBoard sudoku = new(board);

            Assert.That(sudoku.IsSolved());

        }

        [Test]
        public void BoardSetUpWorks() {

            char[,] board = {
                { '1', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '2',},

                { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',},

                { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',},

                { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',},

                { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',},

                { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',},

                { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',},

                { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',},

                { '3', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '4',},
            };

            board = Transpose(board);

            SudokuBoard sudoku = new(board);

            Assert.That(sudoku[0, 0], Is.EqualTo('1'));
            Assert.That(sudoku[^0, 0], Is.EqualTo('2'));
            Assert.That(sudoku[0, ^0], Is.EqualTo('3'));
            Assert.That(sudoku[^0, ^0], Is.EqualTo('4'));

        }

        private static T[,] Transpose<T>(T[,] array) {
            T[,] result = new T[array.GetLength(0), array.GetLength(1)];
            for(int x = 0; x < array.GetLength(0); x++) {
                for(int y = 0; y < array.GetLength(1); y++) {
                    result[y, x] = array[x, y];
                }
            }
            return result;
        }
    }
}