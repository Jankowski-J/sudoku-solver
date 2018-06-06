using System;
using System.Collections.Generic;
using System.Linq;
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
            var context = new SolvingContext
            {
                Grid = baseGrid
            };

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
            }

            return new SolvingResult
            {
                IsSuccess = context.IsSuccess,
                SudokuGrid = context.Grid
            };
        }

        private static void ResetContext(SolvingContext context)
        {
            context.UpdatedCellsCount = 0;
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
            foreach (var square in context.Grid.GetSquares())
            {
                SearchForSingleInGroup(context, square);
            }
            
            foreach (var column in context.Grid.GetColumns())
            {
                SearchForSingleInGroup(context, column);
            }
            
            foreach (var row in context.Grid.GetRows())
            {
                SearchForSingleInGroup(context, row);
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
                    ElligibleCellsCount = group.Count(y => y.CanValueBePut(x))
                })
                .Where(x => x.ElligibleCellsCount == 1)
                .Select(x => x.Value)
                .ToList();

            var single = singles.FirstOrDefault();

            if (single == 0) return;

            var targetCell = group.FirstOrDefault(x => x.CanValueBePut(single));

            if (targetCell == null) return;
            var newCell = new Cell(single, targetCell.X, targetCell.Y);
            context.Grid = context.Grid.UpdateCell(newCell);
            context.GoToLoopStart = true;
        }

        private static bool SearchForPairs(SolvingContext context)
        {
            context.TargetCell = context.Grid.GetCellWithLeastAvailableValues();

            if (context.TargetCell == null || context.TargetCell.GetAvailableValues().Count <= 1) return false;
            
            SearchForPairsInRows(context);
            SearchForPairsInColumns(context);
            SearchForPairsInSquares(context);
            
            if (context.PairFound && context.UpdatedCellsCount > 0) return true;

            context.IsSuccess = false;
            context.ContinueSolving = false;
            return true;
        }

        private static void SearchForPairsInColumns(SolvingContext context)
        {
            foreach (var columnIndex in Enumerable.Range(0, 9))
            {
                var column = context.Grid.GetColumn(columnIndex);
                var combinations = GetValidPairsCombinations(column);
                var pairs = HandleCellPairs(context, combinations, column).ToList();
            }
        }

        public static void SearchForPairsInRows(SolvingContext context)
        {
            foreach (var rowIndex in Enumerable.Range(0, 9))
            {
                var row = context.Grid.GetRow(rowIndex);
                var combinations = GetValidPairsCombinations(row);
                var pairs = HandleCellPairs(context, combinations, row).ToList();
            }
        }

        public static void SearchForPairsInSquares(SolvingContext context)
        {
            foreach (var rowIndex in Enumerable.Range(0, 3))
            {
                foreach (var columnIndex in Enumerable.Range(0, 3))
                {
                    var square = context.Grid.GetSquare(columnIndex, rowIndex);
                    var combinations = GetValidPairsCombinations(square);
                    var pairs = HandleCellPairs(context, combinations, square).ToList();

                    ApplyPairsToRowsAndColumns(context, pairs);
                }
            }
        }

        private static void ApplyPairsToRowsAndColumns(SolvingContext context, List<Tuple<ICell, ICell>> pairs)
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
            context.PairFound = true;
            foreach (var otherCell in otherCells)
            {
                context.UpdatedCellsCount += otherCell.MakeValuesUnavailable(pair.Item1.GetAvailableValues().ToArray());
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

                context.Pairs.Add(Tuple.Create(firstFields.First(), firstFields.Last()));
                CrossOutValues(context, cellGroup, combination);

                yield return combination;
            }
        }
    }
}
