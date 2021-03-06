﻿using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SudokuSolver.Lib.Models;
using SudokuSolver.Lib.Models.Abstract;
using SudokuSolver.Lib.Tests.Data;
using Xunit;

namespace SudokuSolver.Lib.Tests.Models
{
    public class GridTests
    {
        [Fact]
        public void TestDeserialization()
        {
            var grid = GetEasyGrid();
            Assert.NotNull(grid);
        }

        [Fact]
        public void CheckColumns()
        {
            var columns = new List<Column>
            {
                new Column(new short[] {0, 0, 0, 0, 9, 0, 0, 6, 0}, 0),
                new Column(new short[] {2, 0, 7, 0, 0, 0, 0, 0, 1}, 1),
                new Column(new short[] {0, 0, 9, 0, 0, 8, 7, 3, 0}, 2),
                new Column(new short[] {6, 0, 0, 5, 4, 0, 0, 0, 0}, 3),
                new Column(new short[] {0, 5, 0, 6, 0, 2, 0, 9, 0}, 4),
                new Column(new short[] {0, 0, 0, 0, 3, 1, 0, 0, 2}, 5),
                new Column(new short[] {0, 9, 5, 4, 0, 0, 1, 0, 0}, 6),
                new Column(new short[] {7, 0, 0, 0, 0, 0, 3, 0, 8}, 7),
                new Column(new short[] {0, 1, 0, 0, 7, 0, 0, 0, 0}, 8)
            };

            var grid = GetEasyGrid();

            var allCorrect = columns.Select((column, index) =>
            {
                var matchingColumn = grid.GetColumn(index);
                return matchingColumn.SequenceEqual(column);
            }).All(x => x);

            Assert.True(allCorrect);
        }

        [Fact]
        public void CheckRows()
        {
            var rows = new List<Row>
            {
                new Row(new short[] {0, 2, 0, 6, 0, 0, 0, 7, 0}, 0),
                new Row(new short[] {0, 0, 0, 0, 5, 0, 9, 0, 1}, 1),
                new Row(new short[] {0, 7, 9, 0, 0, 0, 5, 0, 0}, 2),
                new Row(new short[] {0, 0, 0, 5, 6, 0, 4, 0, 0}, 3),
                new Row(new short[] {9, 0, 0, 4, 0, 3, 0, 0, 7}, 4),
                new Row(new short[] {0, 0, 8, 0, 2, 1, 0, 0, 0}, 5),
                new Row(new short[] {0, 0, 7, 0, 0, 0, 1, 3, 0}, 6),
                new Row(new short[] {6, 0, 3, 0, 9, 0, 0, 0, 0}, 7),
                new Row(new short[] {0, 1, 0, 0, 0, 2, 0, 8, 0}, 8)
            };

            var grid = GetEasyGrid();

            var allCorrect = rows.Select((row, index) =>
            {
                var matchingRow = grid.GetRow(index);
                return matchingRow.SequenceEqual(row);
            }).All(x => x);

            Assert.True(allCorrect);
        }

        [Fact]
        public void CheckSquares()
        {
            var squares = new List<Square>
            {
                new Square(new short[] {0, 2, 0, 0, 0, 0, 0, 7, 9}, 0, 0),
                new Square(new short[] {6, 0, 0, 0, 5, 0, 0, 0, 0}, 0, 1),
                new Square(new short[] {0, 7, 0, 9, 0, 1, 5, 0, 0}, 0, 2),
                new Square(new short[] {0, 0, 0, 9, 0, 0, 0, 0, 8}, 1, 0),
                new Square(new short[] {5, 6, 0, 4, 0, 3, 0, 2, 1}, 1, 1),
                new Square(new short[] {4, 0, 0, 0, 0, 7, 0, 0, 0}, 1, 2),
                new Square(new short[] {0, 0, 7, 6, 0, 3, 0, 1, 0}, 2, 0),
                new Square(new short[] {0, 0, 0, 0, 9, 0, 0, 0, 2}, 2, 1),
                new Square(new short[] {1, 3, 0, 0, 0, 0, 0, 8, 0}, 2, 2)
            };

            var grid = GetEasyGrid();

            var allCorrect = squares.Select((square, index) =>
            {
                var matchingSquare = grid.GetSquare(index / 3, index % 3);
                var areEqual = matchingSquare.SequenceEqual(square);
                return areEqual;
            }).All(x => x);

            Assert.True(allCorrect);
        }

        [Fact]
        public void GetSquaresInRow_ShouldReturnCorrectSquares()
        {
            var squares = new List<Square>
            {
                new Square(new short[] {0, 0, 0, 9, 0, 0, 0, 0, 8}, 1, 0),
                new Square(new short[] {5, 6, 0, 4, 0, 3, 0, 2, 1}, 1, 1),
                new Square(new short[] {4, 0, 0, 0, 0, 7, 0, 0, 0}, 1, 2),
            };

            var grid = GetEasyGrid();
            var actualSquares = grid.GetSquaresInRow(1).ToList();

            var allCorrect = squares.Select((square, index) =>
            {
                var matchingSquare = actualSquares[index];
                var areEqual = matchingSquare.SequenceEqual(square);
                return areEqual;
            }).All(x => x);

            Assert.True(allCorrect);
        }
        
        [Fact]
        public void GetSquaresInColumn_ShouldReturnCorrectSquares()
        {
            var squares = new List<Square>
            {
                new Square(new short[] {6, 0, 0, 0, 5, 0, 0, 0, 0}, 0, 1),
                new Square(new short[] {5, 6, 0, 4, 0, 3, 0, 2, 1}, 1, 1),
                new Square(new short[] {0, 0, 0, 0, 9, 0, 0, 0, 2}, 2, 1),
            };

            var grid = GetEasyGrid();
            var actualSquares = grid.GetSquaresInColumn(1).ToList();

            var allCorrect = squares.Select((square, index) =>
            {
                var matchingSquare = actualSquares[index];
                var areEqual = matchingSquare.SequenceEqual(square);
                return areEqual;
            }).All(x => x);

            Assert.True(allCorrect);
        }


        [Fact]
        public void UpdateCell_ShouldCreateCorrectGrid()
        {
            var rows = new List<Row>
            {
                new Row(new short[] {0, 2, 0, 6, 0, 0, 0, 7, 0}, 0),
                new Row(new short[] {0, 0, 4, 0, 5, 0, 9, 0, 1}, 1),
                new Row(new short[] {0, 7, 9, 0, 0, 0, 5, 0, 0}, 2),
                new Row(new short[] {0, 0, 0, 5, 6, 0, 4, 0, 0}, 3),
                new Row(new short[] {9, 0, 0, 4, 0, 3, 0, 0, 7}, 4),
                new Row(new short[] {0, 0, 8, 0, 2, 1, 0, 0, 0}, 5),
                new Row(new short[] {0, 0, 7, 0, 0, 0, 1, 3, 0}, 6),
                new Row(new short[] {6, 0, 3, 0, 9, 0, 0, 0, 0}, 7),
                new Row(new short[] {0, 1, 0, 0, 0, 2, 0, 8, 0}, 8)
            };

            var originalGrid = GetEasyGrid();
            var updatedCell = new Cell(4, 2, 1);
            var updatedGrid = originalGrid.UpdateCell(updatedCell);

            var allCorrect = rows.Select((row, index) =>
            {
                var matchingRow = updatedGrid.GetRow(index);
                var areEqual = matchingRow.SequenceEqual(row);
                return areEqual;
            }).All(x => x);

            Assert.True(allCorrect);
        }

        private static IGrid GetEasyGrid()
        {
            return SudokuGrids.EasyGridOne;
        }
    }
}