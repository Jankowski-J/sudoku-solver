namespace SudokuSolver.Lib.Models
{
    public class Cell
    {
        public short Value { get; private set; }

        public Cell(short value = 0)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        protected bool Equals(Cell other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Cell) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
