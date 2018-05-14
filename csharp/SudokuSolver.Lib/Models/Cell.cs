namespace SudokuSolver.Lib.Models
{
    public class Cell
    {
        public short Value { get; private set; }

        public Cell(short value = 0)
        {
            Value = value;
        }        
    }
}
