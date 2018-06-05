using System;
using System.Collections.Generic;
using System.Linq;
using SudokuSolver.Lib.Models;
using SudokuSolver.Lib.Models.Abstract;
using SudokuSolver.Lib.Services.Abstract;

namespace SudokuSolver.Lib.Services
{
    public class SolverService : ISolverService
    {
        public SolvingResult Solve(IGrid baseGrid)
        {
            var continueSolving = true;
            var grid = baseGrid;
            var isSuccess = true;
            while (continueSolving)
            {
                var hasEmptyCells = grid.HasEmptyCells();

                if (!hasEmptyCells)
                {
                    continueSolving = false;
                    continue;
                }

                var targetCell = grid.GetCellWithLeastAvailableValues();
                var pairFound = false;
                int changedCellsCount = 0;
                if (targetCell.GetAvailableValues().Count > 1)
                {
                    foreach (var rowIndex in Enumerable.Range(0, 9))
                    {
                        var row = grid.GetRow(rowIndex);

                        var validGroups = row
                            .Where(x => x.GetAvailableValues().Count == 2)
                            .ToList();

                        var allTuples = validGroups.SelectMany(x => validGroups, Tuple.Create).ToList();
                        var combinations = allTuples
                            .Where(tuple => tuple.Item1.X != tuple.Item2.X || tuple.Item1.Y != tuple.Item2.Y)
                            .Where(tuple => tuple.Item1.GetAvailableValues().SequenceEqual(tuple.Item2.GetAvailableValues()))
                            .ToList();

                        foreach (var combination in combinations)
                        {
                            var firstFields = combination.Item1.GetAvailableValues().ToList();
                            var secondFields = combination.Item2.GetAvailableValues().ToList();
                            if (firstFields.Count == 2 && secondFields.Count == 2 &&
                                firstFields.SequenceEqual(secondFields))
                            {
                                var group = new List<ICell>
                                {
                                    combination.Item1,
                                    combination.Item2
                                };
                                var otherCells = row.Except(group).ToList();
                                pairFound = true;
                                foreach (var otherCell in otherCells)
                                {
                                    changedCellsCount += otherCell.MakeValuesUnavailable(firstFields.ToArray());
                                }
                            }
                        }
                    }

                    foreach (var columnIndex in Enumerable.Range(0, 9))
                    {
                        var column = grid.GetColumn(columnIndex);

                        var validGroups = column
                            .Where(x => x.GetAvailableValues().Count == 2)
                            .ToList();

                        var allTuples = validGroups.SelectMany(x => validGroups, Tuple.Create).ToList();
                        var combinations = allTuples
                            .Where(tuple => tuple.Item1.X != tuple.Item2.X || tuple.Item1.Y != tuple.Item2.Y)
                            .Where(tuple => tuple.Item1.GetAvailableValues().SequenceEqual(tuple.Item2.GetAvailableValues()))
                            .ToList();

                        foreach (var combination in combinations)
                        {
                            var firstFields = combination.Item1.GetAvailableValues().ToList();
                            var secondFields = combination.Item2.GetAvailableValues().ToList();
                            if (firstFields.Count == 2 && secondFields.Count == 2 &&
                                firstFields.SequenceEqual(secondFields))
                            {
                                var group = new List<ICell>
                                {
                                    combination.Item1,
                                    combination.Item2
                                };
                                var otherCells = column.Except(group).ToList();
                                pairFound = true;
                                foreach (var otherCell in otherCells)
                                {
                                    changedCellsCount += otherCell.MakeValuesUnavailable(firstFields.ToArray());
                                }
                            }
                        }
                    }

                    if (pairFound && changedCellsCount > 0)
                    {
                        continue;
                    }

                    isSuccess = false;
                    continueSolving = false;
                    continue;
                }

                var updatedCell = new Cell(targetCell.GetAvailableValues().First(), targetCell.X, targetCell.Y);
                grid = grid.UpdateCell(updatedCell);
            }

            return new SolvingResult
            {
                IsSuccess = isSuccess,
                SudokuGrid = grid
            };
        }
    }
}
