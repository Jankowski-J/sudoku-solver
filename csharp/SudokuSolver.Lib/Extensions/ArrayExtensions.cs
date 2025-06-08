using System;
using System.Linq;

namespace SudokuSolver.Lib.Extensions;

public static class ArrayExtensions
{
    // Found here: https://stackoverflow.com/a/26291720
    public static T[,] To2D<T>(this T[][] source)
    {
        try
        {
            var firstDimension = source.Length;
            var secondDimension = source.GroupBy(row => row.Length).Single().Key;

            var result = new T[firstDimension, secondDimension];
            for (var i = 0; i < firstDimension; ++i)
            for (var j = 0; j < secondDimension; ++j)
                result[i, j] = source[i][j];

            return result;
        }
        catch (InvalidOperationException)
        {
            throw new InvalidOperationException("The given jagged array is not rectangular.");
        } 
    }
}
