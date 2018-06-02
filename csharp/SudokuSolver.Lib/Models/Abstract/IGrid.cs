namespace SudokuSolver.Lib.Models.Abstract
{
    public interface IGrid
    {
        Row GetRow(int index);
        Column GetColumn(int index);
    }
}
