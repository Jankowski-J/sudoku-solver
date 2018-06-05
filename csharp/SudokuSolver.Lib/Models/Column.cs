using SudokuSolver.Lib.Models.Abstract;
using SudokuSolver.Lib.Models.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver.Lib.Models
{
    public sealed class Column : CellGroupBase<ColumnConstructorContext>
    {
        public Column(ColumnConstructorContext context) : base(context)
        {
            Cells = InitializeCells(context);
        }

        public Column(ICollection<short> values, short columnIndex)
            : this(new ColumnConstructorContext
            {
                Values = values,
                ColumnIndex = columnIndex
            })
        {
        }

        public ICell GetCell(int index)
        {
            return Cells[index];
        }

        protected override IList<ICell> InitializeCells(ColumnConstructorContext context)
        {
            IList<ICell> cells;
            if(context.Values.Any())
            {
                cells = context.Values
                    .Select((x, i) => CreateCell(context.Values, x, context.ColumnIndex, (short) i))
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
