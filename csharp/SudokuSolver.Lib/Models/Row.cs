using SudokuSolver.Lib.Models.Abstract;
using SudokuSolver.Lib.Models.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver.Lib.Models
{
    public sealed class Row : CellGroupBase<RowConstructorContext>
    {
        public Row(RowConstructorContext context) : base(context)
        {
            Cells = InitializeCells(context);
        }

        public Row(ICollection<short> values, short rowIndex)
            : this(new RowConstructorContext
            {
                Values = values,
                RowIndex = rowIndex
            })
        {
        }

        public ICell GetCell(int index)
        {
            return Cells[index];
        }

        protected override IList<ICell> InitializeCells(RowConstructorContext context)
        {
            IList<ICell> cells;
            if (context.Values.Any())
            {
                cells = context.Values
                    .Select((x, i) => CreateCell(context.Values, x, (short) i, context.RowIndex))
                    .ToList();
            }
            else
            {
                cells = context.Cells.ToList();
            }

            return cells;
        }
    }
}
