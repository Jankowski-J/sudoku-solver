using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SudokuSolver.Lib.Common;

namespace SudokuSolver.Lib.Models.Abstract
{
    public abstract class CellGroupBase : IEnumerable<ICell>
    {
        protected IList<ICell> Cells;

        protected CellGroupBase(ICollection<short> values)
        {
            if (values == null || !values.Any())
                throw new ArgumentException("Non-empty collection is required", nameof(values));

            if (HasInvalidSizeOrValues(values))
                throw new ArgumentException("9 numbers are required in range of (0, 9)", nameof(values));

            Cells = values.Select(x => CreateCell(values, x)).ToList();
        }

        protected CellGroupBase(ICollection<ICell> cells)
        {
            if (cells == null || !cells.Any())
                throw new ArgumentException("Non-empty collection is required", nameof(cells));

            if (HasInvalidSizeOrValues(cells))
                throw new ArgumentException("9 numbers are required in range of (0, 9)", nameof(cells));

            Cells = cells.Select(x =>
            {
                x.MakeValuesUnavailable(cells.Select(c => c.Value).ToArray());
                return x;
            }).ToList();
        }

        private static ICell CreateCell(IEnumerable<short> allValues, short cellValue)
        {
            var cell = new Cell(cellValue);
            if (cellValue == 0)
            {
                cell.MakeValuesUnavailable(allValues.ToArray());
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
