using System;
using System.Collections.Generic;
using System.Linq;
using SudokuSolver.Lib.Common;
using SudokuSolver.Lib.Extensions;
using SudokuSolver.Lib.Models;
using SudokuSolver.Lib.Models.Abstract;
using SudokuSolver.Lib.Models.Contexts;
using SudokuSolver.Lib.Services.Abstract;

namespace SudokuSolver.Lib.Services
{
    public class SolverService : ISolverService
    {
        public SolvingResult Solve(IGrid baseGrid)
        {
            var context = CreateContext(baseGrid);

            while (context.ContinueSolving)
            {
                ResetContext(context);
                if (!context.ContinueSolving) break;

                SearchForSinglesInContainers(context);

                if (context.GoToLoopStart) continue;

                SearchForPairs(context);

                var targetCell = context.TargetCell;
                var availableValues = targetCell.GetCandidates();
                if (availableValues.Count != 1) continue;

                var updatedCell = new Cell(availableValues.First(), targetCell.X, targetCell.Y);
                context.Grid = context.Grid.UpdateCell(updatedCell);
                context.FilledInCellsCount++;
            }

            return ToSolvingResult(context);
        }

        private static SolvingResult ToSolvingResult(SolvingContext context)
        {
            return new SolvingResult
            {
                IsSuccess = context.IsSuccess,
                SudokuGrid = context.Grid,
                FilledInCellsCount = context.FilledInCellsCount
            };
        }

        private static SolvingContext CreateContext(IGrid baseGrid)
        {
            var context = new SolvingContext
            {
                Grid = baseGrid
            };
            ResetContext(context);
            return context;
        }

        private static void ResetContext(SolvingContext context)
        {
            context.CellsAffectedWithCrossOut = 0;
            context.HasEmptyCells = context.Grid.HasEmptyCells();
            context.GoToLoopStart = false;
            context.Pairs = new List<Tuple<short, short>>();
            if (!context.HasEmptyCells)
            {
                context.ContinueSolving = false;
            }
        }

        private static void SearchForSinglesInContainers(SolvingContext context)
        {
            SearchSingleNumbersInSquares(context);

            foreach (var column in context.Grid.GetColumns())
            {
                SearchForSingleInGroup(context, column);
            }

            foreach (var row in context.Grid.GetRows())
            {
                SearchForSingleInGroup(context, row);
            }
        }

        public static void SearchSingleNumbersInSquares(SolvingContext context)
        {
            foreach (var square in context.Grid.GetSquares())
            {
                SearchForSingleInGroup(context, square);

                var notFilledInNumbers = Enumerable.Range(1, 9)
                    .Select(x => (short) x)
                    .Except(square.Select(x => x.Value).Distinct())
                    .ToList();

                foreach (var number in notFilledInNumbers)
                {
                    var elligibleCells = square.Where(x => x.CanPutCandidate(number)).ToList();

                    if (elligibleCells.Count != 2) continue;

                    var first = elligibleCells.First();
                    var second = elligibleCells.Last();

                    if (first.X == second.X)
                    {
                        var column = context.Grid.GetColumn(first.X);
                        var pairGroup = new List<ICell> {first, second};
                        var otherCells = column.Except(pairGroup).ToList();
                        var updatedCells = otherCells.Aggregate(0, (a, b) => a + (b.RemoveCandidate(number) ? 1 : 0));
                        context.CellsAffectedWithCrossOut += updatedCells;
                    }

                    if (first.Y == second.Y)
                    {
                        var row = context.Grid.GetRow(first.Y);
                        var pairGroup = new List<ICell> {first, second};
                        var otherCells = row.Except(pairGroup).ToList();
                        var updatedCells = otherCells.Aggregate(0, (a, b) => a + (b.RemoveCandidate(number) ? 1 : 0));
                        context.CellsAffectedWithCrossOut += updatedCells;
                    }
                }
            }
        }

        private static void SearchForSingleInGroup(SolvingContext context, ICellGroup group)
        {
            var allNumbers = Enumerable.Range(1, 9)
                .Select(x => (short) x)
                .Except(group.Select(x => x.Value).Distinct())
                .ToList();

            var singles = allNumbers.Select(x => new
                {
                    Value = x,
                    ElligibleCellsCount = group.Count(y => y.CanPutCandidate(x))
                })
                .Where(x => x.ElligibleCellsCount == 1)
                .Select(x => x.Value)
                .ToList();

            var single = singles.FirstOrDefault();

            if (single == 0) return;

            var targetCell = group.FirstOrDefault(x => x.CanPutCandidate(single));

            if (targetCell == null) return;
            var newCell = new Cell(single, targetCell.X, targetCell.Y);
            context.Grid = context.Grid.UpdateCell(newCell);
            context.FilledInCellsCount++;
            context.GoToLoopStart = true;
        }

        private static bool SearchForPairs(SolvingContext context)
        {
            context.TargetCell = context.Grid.GetCellWithLeastAvailableValues();

            if (context.TargetCell == null || context.TargetCell.GetCandidates().Count <= 1) return false;

            SearchForPairsInRows(context);
            SearchForPairsInColumns(context);
            SearchForPairsInSquares(context);

            if (context.PairFound && context.CellsAffectedWithCrossOut > 0) return true;

            context.IsSuccess = false;
            context.ContinueSolving = false;
            return true;
        }

        private static void SearchForPairsInColumns(SolvingContext context)
        {
            foreach (var columnIndex in Enumerable.Range(0, Consts.SudokuGridSize))
            {
                var column = context.Grid.GetColumn(columnIndex);
                var combinations = GetValidPairsCombinations(column);
                var pairs = HandleCellPairs(context, combinations, column).ToList();

                FindCandidatesPairsInCells(context, column);
            }
        }

        public static void SearchForPairsInRows(SolvingContext context)
        {
            foreach (var rowIndex in Enumerable.Range(0, Consts.SudokuGridSize))
            {
                var row = context.Grid.GetRow(rowIndex);
                var combinations = GetValidPairsCombinations(row);
                var pairs = HandleCellPairs(context, combinations, row).ToList();

                FindCandidatesPairsInCells(context, row);
            }
        }

        private static void FindCandidatesPairsInCells(SolvingContext context, ICellGroup cellGroup)
        {
            var allNumbers = GetAllSudokuNumbers();
            var unassignedValues = allNumbers.Except(cellGroup.Select(x => x.Value)).ToList();
            var allNumbersCombinations = unassignedValues
                .SelectMany(x => unassignedValues, Tuple.Create)
                .Where(tuple => tuple.Item1 != tuple.Item2)
                .ToList();

            foreach (var combination in allNumbersCombinations)
            {
                var first = combination.Item1;
                var firstValueCells = cellGroup.Where(x => x.CanPutCandidate(first)).ToList();

                var second = combination.Item2;
                var secondValueCells = cellGroup.Where(x => x.CanPutCandidate(second)).ToList();

                if (firstValueCells.Count != 2 || secondValueCells.Count != 2
                                               || !firstValueCells.SequenceEqual(secondValueCells))
                {
                    continue;
                }

                foreach (var cell in firstValueCells)
                {
                    context.CellsAffectedWithCrossOut += cell.RemoveCandidatesExcept(combination.ToArray());
                }

                RemoveCandidates(context, cellGroup, Tuple.Create(firstValueCells.First(), firstValueCells.Last()));
            }
        }

        private static IEnumerable<short> GetAllSudokuNumbers()
        {
            return Enumerable.Range(1, Consts.SudokuGridSize)
                .Select(x => (short) x)
                .ToList();
        }

        public static void SearchForPairsInSquares(SolvingContext context)
        {
            foreach (var rowIndex in Enumerable.Range(0, Consts.SudokuSquareSideSize))
            {
                foreach (var columnIndex in Enumerable.Range(0, Consts.SudokuSquareSideSize))
                {
                    var square = context.Grid.GetSquare(columnIndex, rowIndex);
                    var combinations = GetValidPairsCombinations(square);
                    var pairs = HandleCellPairs(context, combinations, square).ToList();

                    ApplyPairsToRowsAndColumns(context, pairs);

                    FindCandidatesPairsInCells(context, square);
                }
            }

            var allSquareIndices = new short[] {0, 1, 2};
            SearchForPairsInSquareRows(context, allSquareIndices);

            SearchForPairsInSquareColumns(context, allSquareIndices);
        }

        private static void SearchForPairsInSquareColumns(SolvingContext context, short[] allSquareIndices)
        {
            foreach (var columnIndex in Enumerable.Range(0, Consts.SudokuSquareSideSize))
            {
                var squaresInColumn = context.Grid.GetSquaresInColumn(columnIndex).ToList();
                var sudokuNumbers = GetAllSudokuNumbers();

                foreach (var number in sudokuNumbers)
                {
                    var availableColumns = squaresInColumn
                        .Select((x, i) => new NumberInColumn
                        {
                            Value = number,
                            Columns =
                                x.Where(c => c.CanPutCandidate(number))
                                    .Select(c => c.X).Distinct()
                                    .ToList(),
                            RowIndex = (short) i
                        })
                        .Where(x => x.Columns.Count == 2)
                        .ToList();

                    if (availableColumns.Count < 2)
                    {
                        continue;
                    }

                    var pairs = availableColumns.SelectMany(x => availableColumns, Tuple.Create)
                        .Where(x => x.Item1.Value == x.Item2.Value)
                        .Where(x => x.Item1.RowIndex != x.Item2.RowIndex)
                        .ToList();

                    foreach (var pair in pairs)
                    {
                        if (!pair.Item1.Columns.SequenceEqual(pair.Item2.Columns)) continue;

                        var remainingSquareIndex = allSquareIndices
                            .Except(pair.ToList().Select(x => x.RowIndex))
                            .Single();

                        var remainingSquare = squaresInColumn[remainingSquareIndex];
                        var cellsToRemoveCandidates = remainingSquare.Where(x => pair.Item1.Columns.Contains(x.X))
                            .ToList();

                        foreach (var cell in cellsToRemoveCandidates)
                        {
                            context.CellsAffectedWithCrossOut += cell.RemoveCandidates(pair.Item1.Value);
                        }
                    }
                }
            }
        }

        private static void SearchForPairsInSquareRows(SolvingContext context, short[] allSquareIndices)
        {
            foreach (var rowIndex in Enumerable.Range(0, Consts.SudokuSquareSideSize))
            {
                var squaresInRow = context.Grid.GetSquaresInRow(rowIndex).ToList();
                var sudokuNumbers = GetAllSudokuNumbers();

                foreach (var number in sudokuNumbers)
                {
                    var availableColumns = squaresInRow
                        .Select((x, i) => new NumberInRow
                        {
                            Value = number,
                            Rows =
                                x.Where(c => c.CanPutCandidate(number))
                                    .Select(c => c.X).Distinct()
                                    .ToList(),
                            ColumnIndex = (short) i
                        })
                        .Where(x => x.Rows.Count == 2)
                        .ToList();

                    if (availableColumns.Count < 2)
                    {
                        continue;
                    }

                    var pairs = availableColumns.SelectMany(x => availableColumns, Tuple.Create)
                        .Where(x => x.Item1.Value == x.Item2.Value)
                        .Where(x => x.Item1.ColumnIndex != x.Item2.ColumnIndex)
                        .ToList();

                    foreach (var pair in pairs)
                    {
                        if (!pair.Item1.Rows.SequenceEqual(pair.Item2.Rows)) continue;

                        var remainingSquareIndex = allSquareIndices
                            .Except(pair.ToList().Select(x => x.ColumnIndex))
                            .Single();

                        var remainingSquare = squaresInRow[remainingSquareIndex];
                        var cellsToRemoveCandidates = remainingSquare.Where(x => pair.Item1.Rows.Contains(x.Y))
                            .ToList();

                        foreach (var cell in cellsToRemoveCandidates)
                        {
                            context.CellsAffectedWithCrossOut += cell.RemoveCandidates(pair.Item1.Value);
                        }
                    }
                }
            }
        }

        private static void ApplyPairsToRowsAndColumns(SolvingContext context, IEnumerable<Tuple<ICell, ICell>> pairs)
        {
            foreach (var pair in pairs)
            {
                if (pair.Item1.X == pair.Item2.X)
                {
                    var colIndex = pair.Item1.X;
                    var column = context.Grid.GetColumn(colIndex);

                    RemoveCandidates(context, column, pair);
                }

                if (pair.Item1.Y == pair.Item2.Y)
                {
                    var rowIdx = pair.Item2.Y;
                    var row = context.Grid.GetRow(rowIdx);

                    RemoveCandidates(context, row, pair);
                }
            }
        }

        private static void RemoveCandidates(SolvingContext context, ICellGroup cellGroup,
            Tuple<ICell, ICell> pair)
        {
            var otherCells = cellGroup.Except(pair.ToList()).ToList();
            var firstFields = pair.Item1.GetCandidates().ToList();
            context.Pairs.Add(Tuple.Create(firstFields.First(), firstFields.Last()));
            context.PairFound = true;
            foreach (var otherCell in otherCells)
            {
                context.CellsAffectedWithCrossOut += otherCell.RemoveCandidates(firstFields.ToArray());
            }
        }

        private static IEnumerable<Tuple<ICell, ICell>> GetValidPairsCombinations(IEnumerable<ICell> cellGroup)
        {
            var validGroups = cellGroup
                .Where(x => x.GetCandidates().Count == 2)
                .ToList();

            var allTuples = validGroups.SelectMany(x => validGroups, Tuple.Create).ToList();
            var combinations = allTuples
                .Where(tuple => tuple.Item1.X != tuple.Item2.X || tuple.Item1.Y != tuple.Item2.Y)
                .Where(tuple => tuple.Item1.GetCandidates().SequenceEqual(tuple.Item2.GetCandidates()))
                .ToList();

            return combinations;
        }

        private static IEnumerable<Tuple<ICell, ICell>> HandleCellPairs(SolvingContext context,
            IEnumerable<Tuple<ICell, ICell>> combinations, ICellGroup cellGroup)
        {
            foreach (var combination in combinations)
            {
                if (combination.Item1 == null || combination.Item2 == null)
                {
                    break;
                }

                var firstFields = combination.Item1.GetCandidates().ToList();
                var secondFields = combination.Item2.GetCandidates().ToList();
                if (firstFields.Count != 2 || secondFields.Count != 2 ||
                    !firstFields.SequenceEqual(secondFields)) continue;

                RemoveCandidates(context, cellGroup, combination);

                yield return combination;
            }
        }
    }
}