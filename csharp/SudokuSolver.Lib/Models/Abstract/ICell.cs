using System;
using System.Collections.Generic;

namespace SudokuSolver.Lib.Models.Abstract
{
    public interface ICell : IComparable<ICell>
    {
        short Value { get; }
        short X { get; }
        short Y { get; }
        ICollection<short> GetCandidates();
        bool RemoveCandidate(short candidate);
        int RemoveCandidates(params short[] candidates);
        int RemoveCandidatesExcept(params short[] candidatesToBeUntouched);
        bool CanPutCandidate(short candidate);
    }
}
