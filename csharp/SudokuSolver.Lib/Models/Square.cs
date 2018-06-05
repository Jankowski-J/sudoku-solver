using System.Collections.Generic;
using System.Linq;
using SudokuSolver.Lib.Models.Abstract;
using SudokuSolver.Lib.Models.Contexts;

namespace SudokuSolver.Lib.Models
{
    public sealed class Square : CellGroupBase<SquareConstructorContext>
    {
        public Square(SquareConstructorContext context) : base(context)
        {
            Cells = InitializeCells(context);
        }

        public Square(ICollection<short> values, short rowIndex, short columnIndex)
            : this(new SquareConstructorContext
            {
                Values = values,
                ColumnIndex = columnIndex,
                RowIndex = rowIndex
            })
        {
        }

        public ICell GetCell(int column, int row)
        {
            return Cells[row * 3 + column];
        }

        protected override IList<ICell> InitializeCells(SquareConstructorContext context)
        {
            IList<ICell> cells;
            var rowIndex = (short)(context.RowIndex * 3);
            var columnIndex = (short)(context.ColumnIndex * 3);
            if (context.Values.Any())
            {
                cells = context.Values
                    .Select((x, i) =>
                        CreateCell(context.Values, x, (short) (i / 3 + rowIndex), (short) (i % 3 + columnIndex)))
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
