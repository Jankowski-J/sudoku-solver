using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SudokuSolver.Lib.Common;
using SudokuSolver.Lib.Models.Contexts;

namespace SudokuSolver.Lib.Models.Abstract
{
    public abstract class CellGroupBase<T> : IEnumerable<ICell> where T : ICellGroupConstructorContext
    {
        protected IList<ICell> Cells;

        protected CellGroupBase(T context)
        {
            ValidateContext(context);
        }

        protected static void ValidateContext(T context)
        {
            var hasValues = context.Values != null && context.Values.Any();
            var hasValuesOrCells = hasValues || (context.Cells != null && context.Cells.Any());

            if (!hasValuesOrCells)
            {
                throw new ArgumentException("Either Values or Cells have to be non-empty", nameof(context));
            }

            if (hasValues)
            {
                ValidateValues(context.Values);
            }
            else
            {
                ValidateCells(context.Cells);
            }
        }

        protected static void ValidateValues(ICollection<short> values)
        {
            if (values == null || !values.Any())
                throw new ArgumentException("Non-empty collection is required", nameof(values));

            if (HasInvalidSizeOrValues(values))
                throw new ArgumentException("9 numbers are required in range of (0, 9)", nameof(values));
        }

        protected abstract IList<ICell> InitializeCells(T context);

        protected CellGroupBase(ICollection<ICell> cells)
        {
            ValidateCells(cells);

            Cells = cells.Select(x =>
            {
                x.MakeValuesUnavailable(cells.Where(c => c.Value > 0).Select(c => c.Value).ToArray());
                return x;
            }).ToList();
        }

        private static void ValidateCells(ICollection<ICell> cells)
        {
            if (cells == null || !cells.Any())
                throw new ArgumentException("Non-empty collection is required", nameof(cells));

            if (HasInvalidSizeOrValues(cells))
                throw new ArgumentException("9 numbers are required in range of (0, 9)", nameof(cells));
        }

        protected ICell CreateCell(IEnumerable<short> allValues, short cellValue, short x = -1, short y = -1)
        {
            var cell = new Cell(cellValue, x, y);
            if (cellValue == 0)
            {
                cell.MakeValuesUnavailable(allValues.Where(c => c > 0).ToArray());
            }

            return cell;
        }

        private static bool HasInvalidSizeOrValues(ICollection<short> values)
        {
            return values.Count != Consts.SudokuGridSize || values.Any(x => x < 0) ||
                   values.Any(x => x > Consts.SudokuGridSize);
        }

        private static bool HasInvalidSizeOrValues(ICollection<ICell> values)
        {
            return values.Count != Consts.SudokuGridSize || values.Any(x => x.Value < 0) ||
                   values.Any(x => x.Value > Consts.SudokuGridSize);
        }

        public IEnumerator<ICell> GetEnumerator()
        {
            return Cells.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return string.Join(" | ", Cells);
        }
    }
}
