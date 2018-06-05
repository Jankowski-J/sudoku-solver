﻿using System;
using System.Collections.Generic;

namespace SudokuSolver.Lib.Models.Abstract
{
    public interface ICell : IComparable<ICell>
    {
        short Value { get; }
        short X { get; }
        short Y { get; }
        ICollection<short> GetAvailableValues();
        bool MakeValueUnavailable(short value);
        int MakeValuesUnavailable(params short[] values);
        bool CanValueBePut(short value);
    }
}
