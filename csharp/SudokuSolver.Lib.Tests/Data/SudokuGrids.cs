using Newtonsoft.Json;
using SudokuSolver.Lib.Models;
using SudokuSolver.Lib.Models.Abstract;

namespace SudokuSolver.Lib.Tests.Data
{
    public static class SudokuGrids
    {
        public static IGrid EmptyGrid
        {
            get
            {
                const string rawGrid = @"[
                                [0,0,0, 0,0,0, 0,0,0],
                                [0,0,0, 0,0,0, 0,0,0],
                                [0,0,0, 0,0,0, 0,0,0],

                                [0,0,0, 0,0,0, 0,0,0],
                                [0,0,0, 0,0,0, 0,0,0],
                                [0,0,0, 0,0,0, 0,0,0],

                                [0,0,0, 0,0,0, 0,0,0],
                                [0,0,0, 0,0,0, 0,0,0],
                                [0,0,0, 0,0,0, 0,0,0]
                             ]";

                var matrix = JsonConvert.DeserializeObject<short[,]>(rawGrid);
                var grid = Grid.FromSudokuMatrix(matrix);
                return grid;
            }
        }

        public static IGrid EasyGridOne
        {
            get
            {
                const string rawGrid = @"[
                                [0,2,0, 6,0,0, 0,7,0],
                                [0,0,0, 0,5,0, 9,0,1],
                                [0,7,9, 0,0,0, 5,0,0],

                                [0,0,0, 5,6,0, 4,0,0],
                                [9,0,0, 4,0,3, 0,0,7],
                                [0,0,8, 0,2,1, 0,0,0],

                                [0,0,7, 0,0,0, 1,3,0],
                                [6,0,3, 0,9,0, 0,0,0],
                                [0,1,0, 0,0,2, 0,8,0]
                             ]";

                var matrix = JsonConvert.DeserializeObject<short[,]>(rawGrid);
                var grid = Grid.FromSudokuMatrix(matrix);
                return grid;
            }
        }

        public static IGrid EasyGridOneSolution
        {
            get
            {
                const string rawGrid = @"[
                                [5,2,1, 6,3,9, 8,7,4],
                                [3,6,4, 7,5,8, 9,2,1],
                                [8,7,9, 2,1,4, 5,6,3],

                                [1,3,2, 5,6,7, 4,9,8],
                                [9,5,6, 4,8,3, 2,1,7],
                                [7,4,8, 9,2,1, 3,5,6],

                                [2,9,7, 8,4,6, 1,3,5],
                                [6,8,3, 1,9,5, 7,4,2],
                                [4,1,5, 3,7,2, 6,8,9]
                             ]";

                var matrix = JsonConvert.DeserializeObject<short[,]>(rawGrid);
                var grid = Grid.FromSudokuMatrix(matrix);
                return grid;
            }
        }
    }
}

