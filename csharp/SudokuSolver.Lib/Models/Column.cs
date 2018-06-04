﻿using SudokuSolver.Lib.Models.Abstract;
using System.Collections.Generic;

namespace SudokuSolver.Lib.Models
{
    public class Column : CellGroupBase
    {
        public Column(ICollection<short> values) : base(values)
        {
        }

        public ICell GetCell(int index)
        {
            return Cells[index];
        }
    }
}
