using System.Collections.Generic;
using SudokuSolver.Lib.Models.Abstract;

namespace SudokuSolver.Lib.Models
{
    public class Square : CellGroupBase
    {
        public Square(ICollection<short> values) : base(values)
        {
        }

        public Square(ICollection<ICell> cells) : base(cells)
        {
        }

        public ICell GetCell(int col, int row)
        {
            return Cells[row * 3 + col];
        }
    }
}
