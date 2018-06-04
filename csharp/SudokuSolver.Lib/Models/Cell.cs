using System.Collections.Generic;
using System.Linq;
using SudokuSolver.Lib.Models.Abstract;

namespace SudokuSolver.Lib.Models
{
    public class Cell : ICell
    {
        public short Value { get; }

        public short X { get; }

        public short Y { get; }

        private readonly List<short> _availableValues;

        public Cell(short value = 0, short x = -1, short y = -1)
        {
            Value = value;
            if(Value == 0)
            {
                _availableValues = Enumerable.Range(1, 9).Select(r => (short)r).ToList();
            }
            else
            {
                _availableValues = new List<short>();
            }

            X = x;
            Y = y;
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
            if (obj.GetType() != GetType()) return false;
            return Equals((Cell) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public ICollection<short> GetAvailableValues()
        {
            return _availableValues.ToList();
        }

        public bool MakeValueUnavailable(short value)
        {
            return _availableValues.Remove(value);
        }

        public void MakeValuesUnavailable(params short[] values)
        {
            _availableValues.RemoveAll(values.Contains);
        }
    }
}
