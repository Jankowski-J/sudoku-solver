using System.Diagnostics;
using SudokuSolver.Lib.Models.Abstract;

namespace SudokuSolver.Lib.Models
{
    [DebuggerDisplay("{" + nameof(IsSuccess) + ("}, {" + nameof(FilledInCellsCount) + "}"))]
    public class SolvingResult
    {
        public bool IsSuccess { get; set; }
        public IGrid SudokuGrid { get; set; }
        public int FilledInCellsCount { get; set; }
    }
}
