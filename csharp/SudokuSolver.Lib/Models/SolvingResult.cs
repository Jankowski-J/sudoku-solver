using SudokuSolver.Lib.Models.Abstract;

namespace SudokuSolver.Lib.Models
{
    public class SolvingResult
    {
        public bool IsSuccess { get; set; }
        public IGrid SudokuGrid { get; set; }
        public int FilledInCellsCount { get; set; }
    }
}
