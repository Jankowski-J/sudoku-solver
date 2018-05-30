using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SudokuSolver.Lib.Common;

namespace SudokuSolver.Lib.Models
{
    public abstract class CellGroupBase : IEnumerable<Cell>
    {
        protected IList<Cell> Cells;

        protected CellGroupBase(ICollection<short> values)
        {
            if (values == null || !values.Any())
                throw new ArgumentException("Non-empty collection is required", nameof(values));

            if (values.Count != Consts.SudokuGridSize || values.Any(x => x < 0) || values.Any(x => x > Consts.SudokuGridSize))
                throw new ArgumentException("9 numbers are required in range of (0, 9)", nameof(values));

            Cells = values.Select(x => new Cell(x)).ToList();
        }

        public IEnumerator<Cell> GetEnumerator()
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
