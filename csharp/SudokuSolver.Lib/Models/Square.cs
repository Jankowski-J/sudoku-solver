using System.Collections.Generic;
using SudokuSolver.Lib.Models.Abstract;
using SudokuSolver.Lib.Models.Contexts;

namespace SudokuSolver.Lib.Models
{
    public class Square : CellGroupBase<SquareConstructorContext>
    {
        public Square(SquareConstructorContext context) : base(context)
        {
        }

        public Square(ICollection<short> values, short rowIndex, short columnIndex)
            : base(new SquareConstructorContext
            {
                Values = values,
                ColumnIndex =columnIndex,
                RowIndex = rowIndex
            })       {
        }

        public ICell GetCell(int col, int row)
        {
            return Cells[row * 3 + col];
        }

        protected override IList<ICell> InitializeCells(SquareConstructorContext context)
        {
            // TODO: implement initialization
            throw new System.NotImplementedException();
        }
    }
}
