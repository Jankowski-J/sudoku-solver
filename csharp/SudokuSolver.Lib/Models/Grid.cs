using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SudokuSolver.Lib.Common;
using SudokuSolver.Lib.Models.Abstract;
using SudokuSolver.Lib.Models.Contexts;

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
                var rowValues = new short[sudokuSize];
                for (var wid = 0; wid < sudokuSize; wid++)
                {
                    rowValues[wid] = matrix[len, wid];
                }

                var context = new RowConstructorContext()
                {
                    RowIndex = (short)len,
                    Values = rowValues
                };
                _rows[len] = new Row(context);
            }

            for (var wid = 0; wid < sudokuSize; wid++)
            {
                var columnCells = new List<ICell>();
                for (var len = 0; len < sudokuSize; len++)
                {
                    columnCells.Add(_rows[len].GetCell(wid));
                }
                var context = new ColumnConstructorContext
                {
                    Cells = columnCells
                };
                _columns[wid] = new Column(context);
            }

            InitializeSquares(sudokuSize);
        }

        private void InitializeSquares(short sudokuSize)
        {
            var squareSize = sudokuSize / 3;
            _squares = new Square[squareSize, squareSize];
            for (var horizontalSide = 0; horizontalSide < squareSize; horizontalSide++)
            {
                for (var verticalSide = 0; verticalSide < squareSize; verticalSide++)
                {
                    var squareCells = new List<ICell>();
                    for (var x = 0; x < squareSize; x++)
                    {
                        for (var y = 0; y < squareSize; y++)
                        {
                            var row = _columns[(verticalSide * squareSize) + y];
                            squareCells.Add(row.GetCell((horizontalSide * squareSize) + x));
                        }
                    }
                    var context = new SquareConstructorContext
                    {
                        Cells =squareCells,
                        ColumnIndex = (short)horizontalSide,
                        RowIndex = (short)verticalSide
                    };
                    _squares[horizontalSide, verticalSide] = new Square(context);
                }
            }
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
