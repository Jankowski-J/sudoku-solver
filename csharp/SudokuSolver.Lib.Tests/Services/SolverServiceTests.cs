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

        // Second test case is probably flawed
        [Fact]
        public void Solve_ForEasyGridTwo_ShouldReturnSuccessStatus()
        {
            var solver = new SolverService();
            var easyGrid = SudokuGrids.EasyGridTwo;

            var result = solver.Solve(easyGrid);
            Assert.True(result.IsSuccess);
        }
        
        [Fact]
        public void Solve_ForEasyGridTwo_ShouldSolveWhole()
        {
            var solver = new SolverService();
            var easyGrid = SudokuGrids.EasyGridTwo;

            var result = solver.Solve(easyGrid);
            Assert.Equal(SudokuGrids.EasyGridTwoSolution, result.SudokuGrid);
        }
        
        [Fact]
        public void Solve_ForEasyGridThree_ShouldReturnSuccessStatus()
        {
            var solver = new SolverService();
            var easyGrid = SudokuGrids.EasyGridThree;

            var result = solver.Solve(easyGrid);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void Solve_ForEasyGridThree_ShouldSolveWhole()
        {
            var solver = new SolverService();
            var easyGrid = SudokuGrids.EasyGridThree;

            var result = solver.Solve(easyGrid);
            Assert.Equal(SudokuGrids.EasyGridThreeSolution, result.SudokuGrid);
        }
        
        [Fact]
        public void Solve_ForEasyGridFour_ShouldReturnSuccessStatus()
        {
            var solver = new SolverService();
            var easyGrid = SudokuGrids.EasyGridFour;

            var result = solver.Solve(easyGrid);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void Solve_ForEasyGridFour_ShouldSolveWhole()
        {
            var solver = new SolverService();
            var easyGrid = SudokuGrids.EasyGridFour;

            var result = solver.Solve(easyGrid);
            Assert.Equal(SudokuGrids.EasyGridFourSolution, result.SudokuGrid);
        }
        
        //[Fact]
        public void Solve_ForMediumGridOne_ShouldReturnSuccessStatus()
        {
            var solver = new SolverService();
            var mediumGrid = SudokuGrids.MediumGridOne;

            var result = solver.Solve(mediumGrid);
            Assert.True(result.IsSuccess);
        }

        //[Fact]
        public void Solve_ForMediumGridOne_ShouldSolveWhole()
        {
            var solver = new SolverService();
            var mediumGrid = SudokuGrids.MediumGridOne;

            var result = solver.Solve(mediumGrid);
            Assert.Equal(SudokuGrids.MediumGridOneSolution, result.SudokuGrid);
        }
    }
}
