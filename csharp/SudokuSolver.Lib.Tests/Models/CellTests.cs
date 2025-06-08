using SudokuSolver.Lib.Models;
using System.Collections.Generic;
using Xunit;

namespace SudokuSolver.Lib.Tests.Models;

public class CellTests
{
    [Fact]
    public void Construct_ForZeroValue_ShouldHaveAllValuesAvailable()
    {
        var cell = new Cell(0);

        var expected = new List<short> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        var actual = cell.GetCandidates();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Construct_ForNonZeroValue_ShouldHaveNoValuesAvailable()
    {
        var cell = new Cell(5);

        var expected = new List<short>();
        var actual = cell.GetCandidates();

        Assert.Equal(expected, actual);
    }
}
