using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SudokuSolver.Lib.Models.Abstract;

namespace SudokuSolver.Lib.Models
{
    [DebuggerDisplay("{Value} ({X}, {Y}) ({GetCandidatesFormat()})")]
    public class Cell : ICell, IComparable<Cell>
    {
        public short Value { get; }

        public short X { get; }

        public short Y { get; }

        private readonly List<short> _candidates;

        public Cell(short value = 0, short x = -1, short y = -1)
        {
            Value = value;
            _candidates = Value == 0 
                ? Enumerable.Range(1, 9).Select(r => (short) r).ToList() 
                : new List<short>();

            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"{Value.ToString()}";
        }

        protected bool Equals(Cell other)
        {
            return Value == other.Value && X == other.X && Y == other.Y;
        }

        public int CompareTo(ICell other)
        {
            return Value.CompareTo(other.Value);
        }

        public int CompareTo(Cell other)
        {
            return Value.CompareTo(other.Value);
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
            unchecked
            {
                var hashCode = Value.GetHashCode();
                hashCode = (hashCode * 397) ^ X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                return hashCode;
            }
        }

        public ICollection<short> GetCandidates()
        {
            return _candidates.ToList();
        }

        public bool RemoveCandidate(short candidate)
        {
            return _candidates.Remove(candidate);
        }

        public int RemoveCandidates(params short[] candidates)
        {
            return _candidates.RemoveAll(candidates.Contains);
        }

        public int RemoveCandidatesExcept(params short[] candidatesToBeUntouched)
        {
            var toBeRemoved = GetCandidates()
                .Where(x => !candidatesToBeUntouched.Contains(x))
                .ToArray();
            return RemoveCandidates(toBeRemoved);
        }

        public bool CanPutCandidate(short candidate)
        {
            return _candidates.Contains(candidate);
        }

        private string GetCandidatesFormat()
        {
            return string.Join(",", _candidates);
        }
    }
}
