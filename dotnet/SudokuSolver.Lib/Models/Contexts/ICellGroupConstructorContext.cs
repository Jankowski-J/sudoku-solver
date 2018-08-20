using SudokuSolver.Lib.Models.Abstract;
using System.Collections.Generic;

namespace SudokuSolver.Lib.Models.Contexts
{
    public interface ICellGroupConstructorContext
    {
        ICollection<short> Values { get; set; }
        ICollection<ICell> Cells { get; set; }
    }
}
