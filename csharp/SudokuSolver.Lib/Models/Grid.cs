using System;
using System.Linq;

namespace SudokuSolver.Lib.Models
{
    public interface IGrid
    {
        Row GetRow(int index);
        Column GetColumn(int index);
    }

    public class Grid : IGrid
    {
        private const int SudokuSize = 9;

        private readonly Row[] _rows;
        private readonly Column[] _columns;

        protected Grid(short[,] matrix)
        {
            var length = matrix.GetLength(0);
            var width = matrix.GetLength(1);

            if (length != SudokuSize || width != SudokuSize)
            {
                throw new ArgumentException("Expected 9 x 9 matrix", nameof(matrix));
            }

            _rows = new Row[SudokuSize];
            _columns = new Column[SudokuSize];

            for (var len = 0; len < SudokuSize; len++)
            {
                var currentRow = new short[SudokuSize];
                for (var wid = 0; wid < SudokuSize; wid++)
                {
                    currentRow[wid] = matrix[len, wid];
                }

                _rows[len] = new Row(currentRow);
            }

            for (var wid = 0; wid < SudokuSize; wid++)
            {
                var currentColumn = new short[SudokuSize];
                for (var len = 0; len < SudokuSize; len++)
                {
                    currentColumn[len] = matrix[len, wid];
                }

                _columns[wid] = new Column(currentColumn);
            }
        }

        public Column GetColumn(int index)
        {
            return _columns[index];
        }

        public Row GetRow(int index)
        {
            return _rows[index];
        }

        public static Grid FromSudokuMatrix(short[,] matrix)
        {
            return new Grid(matrix);
        }

        public override string ToString()
        {
            var mappedRows = _rows.Select(x => string.Join("|", x)).ToList();

            return string.Join("\r\n", mappedRows);
        }
    }
}
