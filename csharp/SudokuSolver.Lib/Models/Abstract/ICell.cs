using System.Collections.Generic;

namespace SudokuSolver.Lib.Models.Abstract
{
    public interface ICell
    {
        short Value { get; }
        short X { get; }
        short Y { get; }
        ICollection<short> GetAvailableValues();
        bool MakeValueUnavailable(short value);
        void MakeValuesUnavailable(params short[] values);
    }
}
