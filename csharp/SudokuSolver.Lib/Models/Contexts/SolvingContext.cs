using SudokuSolver.Lib.Models.Abstract;

namespace SudokuSolver.Lib.Models.Contexts
{
    public class SolvingContext
    {
        public bool IsSuccess { get; set; } = true;
        public bool ContinueSolving { get; set; } = true;
        public bool HasEmptyCells { get; set; } = true;
        public bool PairFound { get; set; } = false;
        public int UpdatedCellsCount { get; set; } = 0;
        public IGrid Grid { get; set; }
        public ICell TargetCell { get; set; }
    }
}
