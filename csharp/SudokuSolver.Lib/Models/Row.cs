using SudokuSolver.Lib.Models.Abstract;
using System.Collections.Generic;

namespace SudokuSolver.Lib.Models
{
    public class Row : CellGroupBase
    {
        public Row(ICollection<short> values) : base(values) { }

        public Row(ICollection<ICell> cells) : base(cells) { }

        public ICell GetCell(int index)
        {
            return Cells[index];
        }
    }
}
