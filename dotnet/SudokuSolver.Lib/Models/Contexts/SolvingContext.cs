using System;
using System.Collections.Generic;
using SudokuSolver.Lib.Models.Abstract;

namespace SudokuSolver.Lib.Models.Contexts
{
    public class SolvingContext
    {
        public bool IsSuccess { get; set; } = true;
        public bool ContinueSolving { get; set; } = true;
        public bool HasEmptyCells { get; set; } = true;
        public bool PairFound { get; set; } = false;
        public int CellsAffectedWithCrossOut { get; set; } = 0;
        public IGrid Grid { get; set; }
        public ICell TargetCell { get; set; }
        public bool GoToLoopStart { get; set; } = false;
        public ICollection<Tuple<short, short>> Pairs = new List<Tuple<short, short>>();
        public int FilledInCellsCount { get; set; } = 0;
    }
}
