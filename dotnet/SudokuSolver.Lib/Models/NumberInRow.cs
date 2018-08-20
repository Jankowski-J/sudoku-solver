using System.Collections.Generic;

namespace SudokuSolver.Lib.Models
{
    public class NumberInRow
    {
        public short Value { get; set; }
        public List<short> Rows { get; set; }
        public short ColumnIndex { get; set; }
    }
}