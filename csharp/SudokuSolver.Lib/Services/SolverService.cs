using System;
using System.Collections.Generic;
using System.Linq;
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

                if (!context.ContinueSolving)
                {
                    break;
                }
            
                
                SearchForSinglesInContainers(context);

                if (context.GoToLoopStart)
                {
                    continue;
                }
                
                if (SearchForPairs(context));

                var availableValues = context.TargetCell.GetAvailableValues();
                if (availableValues.Count == 1)
                {
                    var updatedCell = new Cell(context.TargetCell.GetAvailableValues().First(), context.TargetCell.X,
                        context.TargetCell.Y);
                    context.Grid = context.Grid.UpdateCell(updatedCell);
                }
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
            if (!context.HasEmptyCells)
            {
                context.ContinueSolving = false;
            }
        }

        private static void SearchForSinglesInContainers(SolvingContext context)
        {
            foreach (var square in context.Grid.GetSquares())
            {
                var derp = square.Any(x => x.X == 2 && x.Y == 2);
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
            var allNumbers = Enumerable.Range(1, 9).Select(x => (short) x)
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

            if (context.TargetCell == null)
            {
                return false;
            }

            if (context.TargetCell.GetAvailableValues().Count <= 1) return false;
            
            SearchForPairsInRows(context);
            SearchForPairsInColumns(context);
            SearchForPairsInSquares(context);
            if (context.PairFound && context.UpdatedCellsCount > 0)
            {
                return true;
            }

            context.IsSuccess = false;
            context.ContinueSolving = false;
            return true;
        }

        private static void SearchForPairsInColumns(SolvingContext context)
        {
            foreach (var columnIndex in Enumerable.Range(0, 9))
            {
                var column = context.Grid.GetColumn(columnIndex);
                var combinations = GetValidPairsCombinations(column, context);
                HandleCellPairs(context, combinations, column);
            }
        }

        private static void SearchForPairsInRows(SolvingContext context)
        {
            foreach (var rowIndex in Enumerable.Range(0, 9))
            {
                var row = context.Grid.GetRow(rowIndex);
                var combinations = GetValidPairsCombinations(row, context);
                HandleCellPairs(context, combinations, row);
            }
        }

        private static void SearchForPairsInSquares(SolvingContext context)
        {
            foreach (var rowIndex in Enumerable.Range(0, 3))
            {
                foreach (var columnIndex in Enumerable.Range(0, 3))
                {
                    var square = context.Grid.GetSquare(columnIndex, rowIndex);
                    var combinations = GetValidPairsCombinations(square, context);
                    HandleCellPairs(context, combinations, square);
                }
            }
        }

        private static IEnumerable<Tuple<ICell, ICell>> GetValidPairsCombinations(IEnumerable<ICell> cellGroup, SolvingContext context)
        {
            var tuple = cellGroup.FirstOrDefault(x => !x.Equals(context.TargetCell)
                                                     && x.GetAvailableValues()
                                                         .SequenceEqual(context.TargetCell.GetAvailableValues()));
            
            yield return new Tuple<ICell, ICell>(context.TargetCell, tuple);
        }

        private static void HandleCellPairs<T>(SolvingContext context, IEnumerable<Tuple<ICell, ICell>> combinations,
            CellGroupBase<T> cellGroup) where T : ICellGroupConstructorContext
        {
            foreach (var combination in combinations)
            {
                if (combination.Item1 == null || combination.Item2 == null)
                    return;
                
                var firstFields = combination.Item1.GetAvailableValues().ToList();
                var secondFields = combination.Item2.GetAvailableValues().ToList();
                if (firstFields.Count != 2 || secondFields.Count != 2 ||
                    !firstFields.SequenceEqual(secondFields)) continue;

                var group = new List<ICell>
                {
                    combination.Item1,
                    combination.Item2
                };
                var otherCells = cellGroup.Except(@group).ToList();
                context.PairFound = true;
                foreach (var otherCell in otherCells)
                {
                    context.UpdatedCellsCount += otherCell.MakeValuesUnavailable(firstFields.ToArray());
                }
            }
        }
    }
}
