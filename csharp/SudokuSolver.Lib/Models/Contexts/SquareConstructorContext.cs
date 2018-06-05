using System.Collections.Generic;
using SudokuSolver.Lib.Models.Abstract;

namespace SudokuSolver.Lib.Models.Contexts
{
    public class SquareConstructorContext : ICellGroupConstructorContext
    {
        public ICollection<short> Values { get; set; }
        public ICollection<ICell> Cells { get; set; }
        public short RowIndex { get; set; }
        public short ColumnIndex { get; set; }

        public SquareConstructorContext()
        {
            Values = new List<short>();
            Cells = new List<ICell>();
        }
    }
}
