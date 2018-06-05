using SudokuSolver.Lib.Services;
using SudokuSolver.Lib.Tests.Data;
using Xunit;

namespace SudokuSolver.Lib.Tests.Services
{
    public class SolverServiceTests
    {
        [Fact]
        public void Solve_ForEmptyGrid_ShouldReturnFailedStatus()
        {
            var solver = new SolverService();
            var emptyGrid = SudokuGrids.EmptyGrid;

            var result = solver.Solve(emptyGrid);
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void Solve_ForEmptyGrid_ShouldNotFillAnyCell()
        {
            var solver = new SolverService();
            var emptyGrid = SudokuGrids.EmptyGrid;

            var result = solver.Solve(emptyGrid);
            Assert.Equal(SudokuGrids.EmptyGrid, result.SudokuGrid);
        }

        [Fact]
        public void Solve_ForEasyGridOne_ShouldReturnSuccessStatus()
        {
            var solver = new SolverService();
            var easyGrid = SudokuGrids.EasyGridOne;

            var result = solver.Solve(easyGrid);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void Solve_ForEasyGridOne_ShouldSolveWhole()
        {
            var solver = new SolverService();
            var easyGrid = SudokuGrids.EasyGridOne;

            var result = solver.Solve(easyGrid);
            Assert.Equal(SudokuGrids.EasyGridOneSolution, result.SudokuGrid);
        }

       // [Fact]
        public void Solve_ForEasyGridTwo_ShouldReturnSuccessStatus()
        {
            var solver = new SolverService();
            var easyGrid = SudokuGrids.EasyGridTwo;

            var result = solver.Solve(easyGrid);
            Assert.True(result.IsSuccess);
        }

        //[Fact]
        public void Solve_ForEasyGridTwo_ShouldSolveWhole()
        {
            var solver = new SolverService();
            var easyGrid = SudokuGrids.EasyGridTwo;

            var result = solver.Solve(easyGrid);
            Assert.Equal(SudokuGrids.EasyGridTwoSolution, result.SudokuGrid);
        }
    }
}
