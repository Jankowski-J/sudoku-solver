using System.Collections.Generic;
using SudokuSolver.Lib.Models;
using Xunit;

namespace SudokuSolver.Lib.Tests.Models
{
    public class SquareTests
    {
        [Fact]
        public void Constructor_For9ValidNumbers_ShouldCreateObject()
        {
            var nums = new List<short>
            {
                0, 2, 3, 4, 5, 6, 7, 8, 9
            };
            var square = new Square(nums, 1, 1);
            Assert.NotNull(square);
        }

        [Fact]
        public void Constructor_For9ValidNumbers_ShouldSetCorrectCellsCoordinates()
        {
            var nums = new List<short>
            {
                0, 2, 3, 4, 5, 6, 7, 8, 9
            };
            var square = new Square(nums, 1, 1);

            foreach (var num in nums)
            {
                var indexOf = nums.IndexOf(num);
                var matchingNumber = square.GetCell(indexOf % 3, indexOf / 3).Value;

                Assert.Equal(num, matchingNumber);
            }
        }
    }
}
