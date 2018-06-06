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
                var availableValues = targetCell.GetAvailableValues();
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
                    var elligibleCells = square.Where(x => x.CanPutValue(number)).ToList();

                    if (elligibleCells.Count != 2) continue;

                    var first = elligibleCells.First();
                    var second = elligibleCells.Last();

                    if (first.X == second.X)
                    {
                        var column = context.Grid.GetColumn(first.X);
                        var pairGroup = new List<ICell> {first, second};
                        var otherCells = column.Except(pairGroup).ToList();
                        var updatedCells = otherCells.Aggregate(0, (a, b) => a + (b.CrossOutValue(number) ? 1 : 0));
                        context.CellsAffectedWithCrossOut += updatedCells;
                    }

                    if (first.Y == second.Y)
                    {
                        var row = context.Grid.GetRow(first.Y);
                        var pairGroup = new List<ICell> {first, second};
                        var otherCells = row.Except(pairGroup).ToList();
                        var updatedCells = otherCells.Aggregate(0, (a, b) => a + (b.CrossOutValue(number) ? 1 : 0));
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
                    ElligibleCellsCount = group.Count(y => y.CanPutValue(x))
                })
                .Where(x => x.ElligibleCellsCount == 1)
                .Select(x => x.Value)
                .ToList();

            var single = singles.FirstOrDefault();

            if (single == 0) return;

            var targetCell = group.FirstOrDefault(x => x.CanPutValue(single));

            if (targetCell == null) return;
            var newCell = new Cell(single, targetCell.X, targetCell.Y);
            context.Grid = context.Grid.UpdateCell(newCell);
            context.FilledInCellsCount++;
            context.GoToLoopStart = true;
        }

        private static bool SearchForPairs(SolvingContext context)
        {
            context.TargetCell = context.Grid.GetCellWithLeastAvailableValues();

            if (context.TargetCell == null || context.TargetCell.GetAvailableValues().Count <= 1) return false;
            
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
            }
        }

        public static void SearchForPairsInRows(SolvingContext context)
        {
            foreach (var rowIndex in Enumerable.Range(0, Consts.SudokuGridSize))
            {
                var row = context.Grid.GetRow(rowIndex);
                var combinations = GetValidPairsCombinations(row);
                var pairs = HandleCellPairs(context, combinations, row).ToList();
            }
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

                    CrossOutValues(context, column, pair);
                }

                if (pair.Item1.Y == pair.Item2.Y)
                {
                    var rowIdx = pair.Item2.Y;
                    var row = context.Grid.GetRow(rowIdx);

                    CrossOutValues(context, row, pair);
                }
            }
        }

        private static void CrossOutValues(SolvingContext context, ICellGroup cellGroup,
            Tuple<ICell, ICell> pair)
        {
            var otherCells = cellGroup.Except(pair.ToList()).ToList();
            var firstFields = pair.Item1.GetAvailableValues().ToList();
            context.Pairs.Add(Tuple.Create(firstFields.First(), firstFields.Last()));
            context.PairFound = true;
            foreach (var otherCell in otherCells)
            {
                context.CellsAffectedWithCrossOut += otherCell.CrossOutValues(firstFields.ToArray());
            }
        }

        private static IEnumerable<Tuple<ICell, ICell>> GetValidPairsCombinations(IEnumerable<ICell> cellGroup)
        {
            var validGroups = cellGroup
                .Where(x => x.GetAvailableValues().Count == 2)
                .ToList();

            var allTuples = validGroups.SelectMany(x => validGroups, Tuple.Create).ToList();
            var combinations = allTuples
                .Where(tuple => tuple.Item1.X != tuple.Item2.X || tuple.Item1.Y != tuple.Item2.Y)
                .Where(tuple => tuple.Item1.GetAvailableValues().SequenceEqual(tuple.Item2.GetAvailableValues()))
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

                var firstFields = combination.Item1.GetAvailableValues().ToList();
                var secondFields = combination.Item2.GetAvailableValues().ToList();
                if (firstFields.Count != 2 || secondFields.Count != 2 ||
                    !firstFields.SequenceEqual(secondFields)) continue;

                CrossOutValues(context, cellGroup, combination);

                yield return combination;
            }
        }
    }
}
