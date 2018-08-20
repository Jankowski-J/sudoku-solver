using System;
using System.Collections.Generic;

namespace SudokuSolver.Lib.Extensions
{
    public static class TupleExtensions
    {
        public static ICollection<T> ToList<T>(this Tuple<T, T> tuple)
        {
            return new List<T> { tuple.Item1, tuple.Item2 };
        }
        
        public static T[] ToArray<T>(this Tuple<T, T> tuple)
        {
            return new T[]{ tuple.Item1, tuple.Item2 };
        }
    }
}