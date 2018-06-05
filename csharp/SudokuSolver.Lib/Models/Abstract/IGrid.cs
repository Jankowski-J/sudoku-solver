using System.Collections.Generic;

namespace SudokuSolver.Lib.Models.Abstract
{
    public interface IGrid : IEnumerable<ICell>
    {
        Row GetRow(int index);
        Column GetColumn(int index);
        Square GetSquare(int col, int row);
        IGrid UpdateCell(ICell cell);
        bool HasEmptyCells();
        ICell GetCellWithLeastAvailableValues();
    }
}
