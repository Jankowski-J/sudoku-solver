namespace SudokuSolver.Lib.Models
{
    public interface IGrid
    {
        Row GetRow(int index);
        Column GetColumn(int index);
    }

    public class Grid : IGrid
    {
        public Column GetColumn(int index)
        {
            throw new System.NotImplementedException();
        }

        public Row GetRow(int index)
        {
            throw new System.NotImplementedException();
        }
    }
}
