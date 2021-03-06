﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SudokuSolver.Lib.Common;
using SudokuSolver.Lib.Extensions;
using SudokuSolver.Lib.Models.Abstract;
using SudokuSolver.Lib.Models.Contexts;

namespace SudokuSolver.Lib.Models
{
    public class Grid : IGrid, IEquatable<Grid>
    {
        private Row[] _rows;
        private Column[] _columns;
        private Square[,] _squares;

        private Grid(short[,] matrix)
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

                var context = new RowConstructorContext
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

            InitializeSquares();
        }

        private void InitializeSquares()
        {
            const short squareSize = Consts.SudokuSquareSideSize;
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
                        Cells = squareCells,
                        ColumnIndex = (short) horizontalSide,
                        RowIndex = (short) verticalSide
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

        public IGrid UpdateCell(ICell cell)
        {
            var currentValues = _rows.Select(x => x.Select(c => c.Value).ToArray()).ToArray();
            var matrix = currentValues.To2D();
            matrix[cell.Y, cell.X] = cell.Value;
    
            return new Grid(matrix);
        }

        public bool HasEmptyCells()
        {
            return this.Any(x => x.GetCandidates().Any());
        }

        public ICell GetCellWithLeastAvailableValues()
        {
            var cells = this.Where(x => x.GetCandidates().Any())
                .OrderBy(x => x.GetCandidates().Count)
                .ToList();
            return cells.FirstOrDefault();
        }

        public IEnumerable<Square> GetSquares()
        {
            return _squares.Cast<Square>();
        }

        public IEnumerable<Column> GetColumns()
        {
            return _columns.ToList();
        }

        public IEnumerable<Row> GetRows()
        {
            return _rows.ToList();
        }

        public IEnumerable<Square> GetSquaresInRow(int rowIndex)
        {
            yield return _squares[rowIndex, 0];
            yield return _squares[rowIndex, 1];
            yield return _squares[rowIndex, 2];
        }

        public IEnumerable<Square> GetSquaresInColumn(int columnIndex)
        {
            yield return _squares[0, columnIndex];
            yield return _squares[1, columnIndex];
            yield return _squares[2, columnIndex];
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

        public bool Equals(Grid other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(_rows, other._rows);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Grid) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (_rows != null ? _rows.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (_columns != null ? _columns.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (_squares != null ? _squares.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
