using System;
using SudokuSolver.Lib.Models.Abstract;
using SudokuSolver.Lib.Models.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver.Lib.Models
{
    public sealed class Column : CellGroupBase<ColumnConstructorContext>, IEquatable<Column>
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
                    .Select((value, index) => CreateCell(context.Values, value, context.ColumnIndex, (short) index))
                    .ToList();
            }
            else
            {
                cells = context.Cells.ToList();
                MakeCellsUnavailable(context, cells);
            }

            return cells;
        }

        public bool Equals(Column other)
        {
            return this.SequenceEqual(other);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Column && Equals((Column) obj);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
