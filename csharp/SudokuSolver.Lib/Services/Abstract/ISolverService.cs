using SudokuSolver.Lib.Models;
using SudokuSolver.Lib.Models.Abstract;

namespace SudokuSolver.Lib.Services.Abstract;

public interface ISolverService
{
    SolvingResult Solve(IGrid baseGrid);
}
