﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver.Lib.Models
{
    public class Column : CellGroupBase
    {
        public Column(ICollection<short> values) : base(values)
        {
        }

        public Cell GetCell(int index)
        {
            return _cells[index];
        }
    }
}