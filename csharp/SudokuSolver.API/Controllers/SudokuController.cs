using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenCvSharp;
using SudokuSolver.API.Models;
using SudokuSolver.Lib.Models;
using SudokuSolver.Lib.Services.Abstract;
using System;
using System.IO;
using System.Linq;
using Size = OpenCvSharp.Size;

namespace SudokuSolver.API.Controllers;

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
            return new BadRequestObjectResult(new { error = "The sudoku grid should have 9 rows." });
        }

        if (model.Cells.Any(x => x.Length != 9))
        {
            return new BadRequestObjectResult(new { error = "Each sudoku row should have 9 columns." });
        }

        var matrix = new short[9, 9];

        for (var row = 0; row < 9; row++)
        {
            for (var col = 0; col < 9; col++)
            {
                matrix[row, col] = (short)model.Cells[row][col];
            }
        }

        var grid = Grid.FromSudokuMatrix(matrix);
        var result = _solverService.Solve(grid);
        return new JsonResult(result.IsSuccess);
    }

    [HttpPost("extract-grid")]
    public IActionResult ExtractGrid(IFormFile image)
    {
        if (image == null || image.Length == 0)
            return BadRequest(new { error = "Brak pliku." });

        try
        {
            using var ms = new MemoryStream();
            image.CopyTo(ms);
            byte[] bytes = ms.ToArray();

            using var src = Cv2.ImDecode(bytes, ImreadModes.Color);
            using var grid = ExtractSudokuGrid(src);

            var resultBytes = grid.ToBytes(".png");
            return File(resultBytes, "image/png");
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    private Mat ExtractSudokuGrid(Mat img)
    {
        var gray = new Mat();
        Cv2.CvtColor(img, gray, ColorConversionCodes.BGR2GRAY);

        var blur = new Mat();
        Cv2.GaussianBlur(gray, blur, new Size(9, 9), 0);

        var thresh = new Mat();
        Cv2.AdaptiveThreshold(blur, thresh, 255, AdaptiveThresholdTypes.MeanC, ThresholdTypes.BinaryInv, 11, 2);

        var contours = Cv2.FindContoursAsArray(thresh, RetrievalModes.External, ContourApproximationModes.ApproxSimple);
        if (contours.Length == 0)
            throw new Exception("Nie znaleziono konturów.");

        // ZnajdŸ najwiêkszy kontur      
        var maxContour = contours.OrderByDescending(x => Cv2.ContourArea(x)).First();
        var peri = Cv2.ArcLength(maxContour, true);
        var approx = Cv2.ApproxPolyDP(maxContour, 0.02 * peri, true);

        if (approx.Length != 4)
            throw new Exception("Nie wykryto prostok¹tnej siatki.");

        // Przekszta³cenie perspektywy
        var sorted = SortCorners(approx.Select(p => (Point2f)p).ToArray());
        var size = 450;
        var dstPts = new[]
        {
            new Point2f(0,0),
            new Point2f(size-1,0),
            new Point2f(size-1,size-1),
            new Point2f(0,size-1)
        };

        var matrix = Cv2.GetPerspectiveTransform(sorted, dstPts);
        var warped = new Mat();
        Cv2.WarpPerspective(img, warped, matrix, new Size(size, size));

        return warped;
    }

    private Point2f[] SortCorners(Point2f[] pts)
    {
        var sorted = new Point2f[4];

        var sum = pts.Select(p => p.X + p.Y).ToArray();
        var diff = pts.Select(p => p.Y - p.X).ToArray();

        sorted[0] = pts[Array.IndexOf(sum, sum.Min())]; // top-left
        sorted[2] = pts[Array.IndexOf(sum, sum.Max())]; // bottom-right
        sorted[1] = pts[Array.IndexOf(diff, diff.Min())]; // top-right
        sorted[3] = pts[Array.IndexOf(diff, diff.Max())]; // bottom-left

        return sorted;
    }
}
