using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SudokuSolver.Lib.Common;
using SudokuSolver.Lib.Models.Abstract;

namespace SudokuSolver.Lib.Models
{
    public class Grid : IGrid
    {
        private Row[] _rows;
        private Column[] _columns;

        protected Grid(short[,] matrix)
        {
            var length = matrix.GetLength(0);
            var width = matrix.GetLength(1);

            const short sudokuSize = Consts.SudokuGridSize;
            if (length != sudokuSize || width != sudokuSize)
            {
                throw new ArgumentException($"Expected {sudokuSize} x {sudokuSize} matrix", nameof(matrix));
            }

            InitializeRowsAndColumns(matrix, sudokuSize);
        }

        private void InitializeRowsAndColumns(short[,] matrix, short sudokuSize)
        {
            _rows = new Row[sudokuSize];
            _columns = new Column[sudokuSize];

            for (var len = 0; len < sudokuSize; len++)
            {
                var currentRow = new short[sudokuSize];
                for (var wid = 0; wid < sudokuSize; wid++)
                {
                    currentRow[wid] = matrix[len, wid];
                }

                _rows[len] = new Row(currentRow);
            }

            for (var wid = 0; wid < sudokuSize; wid++)
            {
                var col = new List<ICell>();
                for (var len = 0; len < sudokuSize; len++)
                {
                    col.Add(_rows[len].GetCell(wid));
                }

                _columns[wid] = new Column(col);
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

        public IEnumerator<ICell> GetEnumerator()
        {
            var allCells = _rows.SelectMany(x => x);

            return allCells.GetEnumerator();
        }

        public override string ToString()
        {
            var mappedRows = _rows.Select(x => x.ToString()).ToList();

            return string.Join(Environment.NewLine, mappedRows);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
