using System.Collections.Generic;
using SudokuSolver.Lib.Models.Abstract;

namespace SudokuSolver.Lib.Models.Contexts
{
    public class RowConstructorContext : ICellGroupConstructorContext
    {
        public ICollection<short> Values { get; set; }
        public ICollection<ICell> Cells { get; set; }
        public short RowIndex { get; set; }

        public RowConstructorContext()
        {
            Values = new List<short>();
            Cells = new List<ICell>();
        }
    }
}
