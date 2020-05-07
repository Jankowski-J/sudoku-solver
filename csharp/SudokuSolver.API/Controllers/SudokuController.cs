using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SudokuSolver.API.Models;
using SudokuSolver.Lib.Models;
using SudokuSolver.Lib.Services.Abstract;

namespace SudokuSolver.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SudokuController : ControllerBase
    {
        private readonly ISolverService _solverService;

        public SudokuController(ISolverService solverService)
        {
            _solverService = solverService;
        }

        [Route("solve")]
        [HttpPost]
        public IActionResult Solve([FromBody] SolveSudokuModel model)
        {
            if (model.Cells.Length != 9)
            {
                return new BadRequestObjectResult(new {error = "The sudoku grid should have 9 rows."});
            }

            if (model.Cells.Any(x => x.Length != 9))
            {
                return new BadRequestObjectResult(new {error = "Each sudoku row should have 9 columns."});
            }

            var matrix = new short[9, 9];

            for (var row = 0; row < 9; row++)
            {
                for (var col = 0; col < 9; col++)
                {
                    matrix[row, col] = (short) model.Cells[row][col];
                }
            }

            var grid = Grid.FromSudokuMatrix(matrix);
            var result = _solverService.Solve(grid);
            return new JsonResult(result.IsSuccess);
        }
    }
}