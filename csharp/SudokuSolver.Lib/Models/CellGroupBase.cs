using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver.Lib.Models
{
    public abstract class CellGroupBase
    {
        protected IList<Cell> _cells;

        protected CellGroupBase(ICollection<short> values)
        {
            if (values == null || !values.Any())
                throw new ArgumentException("Non-empty collection is required", nameof(values));

            if (values.Count != 9 || values.Any(x => x < 0) || values.Any(x => x > 9))
                throw new ArgumentException("9 numbers are required in range of (0, 9)", nameof(values));

            _cells = values.Select(x => new Cell(x)).ToList();
        }
    }
}
