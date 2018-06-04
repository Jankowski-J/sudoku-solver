using SudokuSolver.Lib.Models.Abstract;
using SudokuSolver.Lib.Models.Contexts;
using System.Collections.Generic;

namespace SudokuSolver.Lib.Models
{
    public class Row : CellGroupBase<RowConstructorContext>
    {
        public Row(RowConstructorContext context) : base(context) { }

        public Row(ICollection<short> values, short rowIndex) : base(
            new RowConstructorContext
            {
                Values = values,
                RowIndex = rowIndex
            }) { }

        public ICell GetCell(int index)
        {
            return Cells[index];
        }

        protected override IList<ICell> InitializeCells(RowConstructorContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
