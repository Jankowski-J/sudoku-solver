using System.Collections.Generic;

namespace SudokuSolver.Lib.Models.Abstract
{
    public interface ICell
    {
        short Value { get; }
        ICollection<short> GetAvailableValues();
        bool MakeValueUnavailable(short value);
    }
}
