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
        private Square[,] _squares;

        protected Grid(short[,] matrix)
        {
            var length = matrix.GetLength(0);
            var width = matrix.GetLength(1);

            const short sudokuSize = Consts.SudokuGridSize;
            if (length != sudokuSize || width != sudokuSize)
            {
                throw new ArgumentException($"Expected {sudokuSize} x {sudokuSize} matrix", nameof(matrix));
            }

            InitializeSubContainers(matrix, sudokuSize);
        }

        private void InitializeSubContainers(short[,] matrix, short sudokuSize)
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

            // TODO: initialize squares - with test cases
            //var squareSize = sudokuSize / 3;
            //_squares = new Square[squareSize,squareSize];
            //for (var wid = 0; wid < squareSize; wid++)
            //{
            //    for (var len = 0; len < squareSize; len++)
            //    {
            //        var squareCells = new List<ICell>();
            //        for (var x = 0; x < squareSize; x++)
            //        {
            //            for (var y = 0; y < squareSize; y++)
            //            {
            //                var row = _columns[len + y];
            //                squareCells.Add(row.GetCell(x + wid));
            //            }
            //        }
            //        var square = new Square(squareCells);
            //        _squares[wid, len] = square;
            //    }
            //}
        }

        public Column GetColumn(int index)
        {
            return _columns[index];
        }

        public Square GetSquare(int col, int row)
        {
            return _squares[col, row];
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
