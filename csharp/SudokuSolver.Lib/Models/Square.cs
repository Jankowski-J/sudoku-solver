using System;
using System.Collections.Generic;
using System.Linq;
using SudokuSolver.Lib.Common;
using SudokuSolver.Lib.Models.Abstract;
using SudokuSolver.Lib.Models.Contexts;

namespace SudokuSolver.Lib.Models
{
    public sealed class Square : CellGroupBase<SquareConstructorContext>, IEquatable<Square>
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
            return Cells[row * Consts.SudokuSquareSideSize + column];
        }

        protected override IList<ICell> InitializeCells(SquareConstructorContext context)
        {
            IList<ICell> cells;
            var rowIndex = (short)(context.RowIndex * Consts.SudokuSquareSideSize);
            var columnIndex = (short)(context.ColumnIndex * Consts.SudokuSquareSideSize);
            if (context.Values.Any())
            {
                cells = context.Values
                    .Select((value, index) =>
                        CreateCell(context.Values, value, (short) (index % Consts.SudokuSquareSideSize + columnIndex),
                            (short) (index / Consts.SudokuSquareSideSize + rowIndex)))
                    .ToList();
            }
            else
            {
                cells = context.Cells.ToList();
                MakeCellsUnavailable(context, cells);
            }

            return cells;
        }

        public bool Equals(Square other)
        {
            return this.SequenceEqual(other);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Square && Equals((Square) obj);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
