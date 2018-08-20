using System.Collections.Generic;

namespace SudokuSolver.Lib.Models
{
    public class NumberInColumn
    {
        public short Value { get; set; }
        public List<short> Columns { get; set; }
        public short RowIndex { get; set; }
    }
}