using SudokuSolver.Lib.Models.Abstract;
using SudokuSolver.Lib.Models.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver.Lib.Models
{
    public class Column : CellGroupBase<ColumnConstructorContext>
    {
        public Column(ColumnConstructorContext context) : base(context) { }

        public Column(ICollection<short> values, short columnIndex) : base(
          new ColumnConstructorContext
          {
              Values = values,
              ColumnIndex = columnIndex
          })
        { }

        public ICell GetCell(int index)
        {
            return Cells[index];
        }

        protected override IList<ICell> InitializeCells(ColumnConstructorContext context)
        {
            var cells = context.Values
                .Select((x, i) => CreateCell(context.Values, x, context.ColumnIndex, (short)i))
                .ToList();

            return cells;
        }
    }
}
