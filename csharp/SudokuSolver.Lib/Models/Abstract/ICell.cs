using System;
using System.Collections.Generic;

namespace SudokuSolver.Lib.Models.Abstract
{
    public interface ICell : IComparable<ICell>
    {
        short Value { get; }
        short X { get; }
        short Y { get; }
        ICollection<short> GetAvailableValues();
        bool CrossOutValue(short value);
        int CrossOutValues(params short[] values);
        bool CanPutValue(short value);
    }
}
