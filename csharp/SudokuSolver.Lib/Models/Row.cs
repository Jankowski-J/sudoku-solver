using System;
using SudokuSolver.Lib.Models.Abstract;
using SudokuSolver.Lib.Models.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver.Lib.Models
{
    public sealed class Row : CellGroupBase<RowConstructorContext>, IEquatable<Row>
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
                    .Select((value, index) => CreateCell(context.Values, value, (short) index, context.RowIndex))
                    .ToList();
            }
            else
            {
                cells = context.Cells.ToList();
                MakeCellsUnavailable(context, cells);
            }

            return cells;
        }

        public bool Equals(Row other)
        {
            return this.SequenceEqual(other);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Row && Equals((Row) obj);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
