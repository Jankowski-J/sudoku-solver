using SudokuSolver.Lib.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace SudokuSolver.Lib.Tests.Models;

public class RowTests
{
    [Fact]
    public void Constructor_ForEmptyCollection_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Row(new List<short>(), 0));
    }

    [Fact]
    public void Constructor_ForNotBigEnoughCollection_ShouldThrowArgumentException()
    {
        var nums = new List<short>
        {
            5, 4, 9, 6
        };
        Assert.Throws<ArgumentException>(() => new Row(nums, 0));
    }

    [Fact]
    public void Constructor_ForTooBigEnoughCollection_ShouldThrowArgumentException()
    {
        var nums = new List<short>
        {
            5, 4, 9, 6, 7, 0, 1, 2, 8, 3, 4, 5
        };
        Assert.Throws<ArgumentException>(() => new Row(nums, 0));
    }

    [Fact]
    public void Constructor_For9NumbersButSomeOutOfRange_ShouldThrowArgumentException()
    {
        var nums = new List<short>
        {
            -1, 2, 3, 4, 5, 6, 7, 8, 11
        };
        Assert.Throws<ArgumentException>(() => new Row(nums, 0));
    }

    [Fact]
    public void Constructor_For9ValidNumbers_ShouldCreateObject()
    {
        var nums = new List<short>
        {
            0, 2, 3, 4, 5, 6, 7, 8, 9
        };
        var row = new Row(nums, 0);
        Assert.NotNull(row);
    }

    [Fact]
    public void Constructor_For9ValidNumbers_ShouldCreateObjectWithCorrectValues()
    {
        var nums = new List<short>
        {
            0, 2, 3, 4, 5, 6, 7, 8, 9
        };
        var row = new Row(nums, 0);           
        
        foreach(var num in nums)
        {
            var index = nums.IndexOf(num);

            var cell = row.GetCell(index);

            Assert.Equal(num, cell.Value);
        }
    }
}
