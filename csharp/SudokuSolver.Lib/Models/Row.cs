using SudokuSolver.Lib.Models.Abstract;
using System.Collections.Generic;

namespace SudokuSolver.Lib.Models
{
    public class Row : CellGroupBase
    {
        public Row(ICollection<short> values) : base(values)
        {
        }

        public Cell GetCell(int index)
        {
            return Cells[index];
        }
    }
}
