﻿using Newtonsoft.Json;
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

                var grid = DeserializeGrid(rawGrid);
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

                var grid = DeserializeGrid(rawGrid);
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

                var grid = DeserializeGrid(rawGrid);
                return grid;
            }
        }

        public static IGrid EasyGridTwo
        {
            get
            {
                const string rawGrid = @"[
                                [0,5,0, 3,0,0, 2,0,0],
                                [0,0,0, 0,0,6, 0,3,8],
                                [3,9,8, 0,0,5, 6,0,0],

                                [0,0,0, 0,7,0, 0,6,0],
                                [9,0,7, 0,0,0, 1,0,4],
                                [0,6,0, 0,3,0, 0,0,0],

                                [0,0,9, 4,0,0, 8,5,7],
                                [4,7,0, 6,0,0, 0,0,0],
                                [0,0,3, 0,0,1, 0,9,0]
                             ]";

                var grid = DeserializeGrid(rawGrid);
                return grid;
            }
        }

        public static IGrid EasyGridTwoSolution
        {
            get
            {
                const string rawGrid = @"[
                                [1,5,6, 3,8,7, 2,4,9],
                                [7,4,2, 9,1,6, 5,3,8],
                                [3,9,8, 2,4,5, 6,7,1],

                                [5,2,1, 8,7,4, 9,6,3],
                                [9,3,7, 5,6,2, 1,8,4],
                                [8,6,4, 1,3,9, 7,2,5],

                                [6,1,9, 4,2,3, 8,5,7],
                                [4,7,5, 6,9,8, 3,1,2],
                                [2,8,3, 7,5,1, 4,9,6]
                             ]";

                var grid = DeserializeGrid(rawGrid);
                return grid;
            }
        }
        
        public static IGrid EasyGridThree
        {
            get
            {
                const string rawGrid = @"[
                                [0,0,0, 3,0,1, 0,0,0],
                                [6,1,0, 0,0,0, 0,8,4],
                                [7,0,8, 0,5,0, 3,0,9],
                                
                                [4,0,0, 5,0,9, 0,0,2],
                                [9,0,0, 0,0,0, 0,0,5],
                                [3,0,0, 8,0,2, 0,0,7],
                                
                                [8,0,6, 0,3,0, 2,0,1],
                                [1,9,0, 0,0,0, 0,5,3],
                                [0,0,0, 9,0,6, 0,0,0]
                             ]";

                var grid = DeserializeGrid(rawGrid);
                return grid;
            }
        }

        public static IGrid EasyGridThreeSolution
        {
            get
            {
                const string rawGrid = @"[
                            [5,4,9, 3,8,1, 7,2,6],
                            [6,1,3, 2,9,7, 5,8,4],
                            [7,2,8, 6,5,4, 3,1,9],
                            
                            [4,8,7, 5,6,9, 1,3,2],
                            [9,6,2, 1,7,3, 8,4,5],
                            [3,5,1, 8,4,2, 9,6,7],
                            
                            [8,7,6, 4,3,5, 2,9,1],
                            [1,9,4, 7,2,8, 6,5,3],
                            [2,3,5, 9,1,6, 4,7,8]
                             ]";

                var grid = DeserializeGrid(rawGrid);
                return grid;
            }
        }
        
        public static IGrid EasyGridFour
        {
            get
            {
                const string rawGrid = @"[
                                [0,0,0, 5,0,0, 0,0,0],
                                [0,5,0, 0,1,2, 0,0,4],
                                [0,0,0, 4,6,0, 7,0,3],
                                
                                [2,0,7, 6,0,0, 0,8,0],
                                [1,0,0, 0,0,0, 0,0,9],
                                [0,8,0, 0,0,9, 5,0,6],
                                
                                [6,0,9, 0,7,1, 0,0,0],
                                [7,0,0, 9,8,0, 0,4,0],
                                [0,0,0, 0,0,6, 0,0,0]
                             ]";

                var grid = DeserializeGrid(rawGrid);
                return grid;
            }
        }

        public static IGrid EasyGridFourSolution
        {
            get
            {
                const string rawGrid = @"[
                            [4,7,6, 5,9,3, 2,1,8],
                            [8,5,3, 7,1,2, 9,6,4],
                            [9,1,2, 4,6,8, 7,5,3],
                            
                            [2,9,7, 6,5,4, 3,8,1],
                            [1,6,5, 8,3,7, 4,2,9],
                            [3,8,4, 1,2,9, 5,7,6],
                            
                            [6,4,9, 2,7,1, 8,3,5],
                            [7,3,1, 9,8,5, 6,4,2],
                            [5,2,8, 3,4,6, 1,9,7]
                             ]";

                var grid = DeserializeGrid(rawGrid);
                return grid;
            }
        }
        
        public static IGrid MediumGridOne
        {
            get
            {
                const string rawGrid = @"[
                                [2,0,4, 0,0,0, 0,7,9],
                                [0,0,0, 0,0,6, 0,8,5],
                                [0,3,0, 0,0,0, 4,0,0],
                                
                                [9,0,0, 5,8,0, 0,4,0],
                                [0,0,0, 0,0,0, 0,0,0],
                                [0,6,0, 0,9,2, 0,0,7],
                                
                                [0,0,1, 0,0,0, 0,3,0],
                                [6,2,0, 3,0,0, 0,0,0],
                                [5,4,0, 0,0,0, 7,0,8]
                             ]";

                var grid = DeserializeGrid(rawGrid);
                return grid;
            }
        }
        
        public static IGrid MediumGridOneSolution
        {
            get
            {
                const string rawGrid = @"[
                                [2,5,4, 1,3,8, 6,7,9],
                                [1,7,9, 2,4,6, 3,8,5],
                                [8,3,6, 7,5,9, 4,2,1],
                                
                                [9,1,7, 5,8,3, 2,4,6],
                                [4,8,2, 6,1,7, 9,5,3],
                                [3,6,5, 4,9,2, 8,1,7],
                                
                                [7,9,1, 8,6,4, 5,3,2],
                                [6,2,8, 3,7,5, 1,9,4],
                                [5,4,3, 9,2,1, 7,6,8]
                             ]";

                var grid = DeserializeGrid(rawGrid);
                return grid;
            }
        }
        
        public static IGrid MediumGridTwo
        {
            get
            {
                const string rawGrid = @"[
                                [1,0,9, 0,0,0, 0,3,8],
                                [0,5,0, 0,0,0, 0,7,0],
                                [0,0,0, 5,6,3, 0,0,2],
                                
                                [9,0,0, 3,0,0, 0,0,0],
                                [0,8,0, 7,0,9, 0,6,0],
                                [0,0,0, 0,0,6, 0,0,9],
                                
                                [8,0,0, 4,3,1, 0,0,0],
                                [0,2,0, 0,0,0, 0,8,0],
                                [3,1,0, 0,0,0, 9,0,4]
                             ]";

                var grid = DeserializeGrid(rawGrid);
                return grid;
            }
        }
        
        public static IGrid MediumGridTwoSolution
        {
            get
            {
                const string rawGrid = @"[
                                [1,6,9, 2,4,7, 5,3,8],
                                [2,5,3, 1,9,8, 4,7,6],
                                [7,4,8, 5,6,3, 1,9,2],
                                
                                [9,7,6, 3,2,4, 8,1,5],
                                [4,8,1, 7,5,9, 2,6,3],
                                [5,3,2, 8,1,6, 7,4,9],
                                
                                [8,9,5, 4,3,1, 6,2,7],
                                [6,2,4, 9,7,5, 3,8,1],
                                [3,1,7, 6,8,2, 9,5,4]
                             ]";

                var grid = DeserializeGrid(rawGrid);
                return grid;
            }
        }
        
        public static IGrid MediumGridThree
        {
            get
            {
                const string rawGrid = @"[
                                [1,0,9, 0,0,0, 0,3,8],
                                [0,5,0, 0,0,0, 0,7,0],
                                [0,0,0, 5,6,3, 0,0,2],
                                
                                [9,0,0, 3,0,0, 0,0,0],
                                [0,8,0, 7,0,9, 0,6,0],
                                [0,0,0, 0,0,6, 0,0,9],
                                
                                [8,0,0, 4,3,1, 0,0,0],
                                [0,2,0, 0,0,0, 0,8,0],
                                [3,1,0, 0,0,0, 9,0,4]
                             ]";

                var grid = DeserializeGrid(rawGrid);
                return grid;
            }
        }
        
        public static IGrid MediumGridThreeSolution
        {
            get
            {
                const string rawGrid = @"[
                                [1,6,9, 2,4,7, 5,3,8],
                                [2,5,3, 1,9,8, 4,7,6],
                                [7,4,8, 5,6,3, 1,9,2],
                                
                                [9,7,6, 3,2,4, 8,1,5],
                                [4,8,1, 7,5,9, 2,6,3],
                                [5,3,2, 8,1,6, 7,4,9],
                                
                                [8,9,5, 4,3,1, 6,2,7],
                                [6,2,4, 9,7,5, 3,8,1],
                                [3,1,7, 6,8,2, 9,5,4]
                             ]";

                var grid = DeserializeGrid(rawGrid);
                return grid;
            }
        }
        
        public static IGrid MediumGridFour
        {
            get
            {
                const string rawGrid = @"[
                                [0,0,0, 5,0,7, 0,0,0],
                                [0,0,8, 9,4,3, 2,0,0],
                                [9,0,0, 0,2,0, 0,0,4],
                                
                                [0,6,3, 0,0,0, 1,4,0],
                                [5,0,0, 0,0,0, 0,0,8],
                                [0,8,7, 0,0,0, 9,5,0],
                                
                                [8,0,0, 0,3,0, 0,0,9],
                                [0,0,4, 1,9,5, 6,0,0],
                                [0,0,0, 7,0,2, 0,0,0]
                             ]";

                var grid = DeserializeGrid(rawGrid);
                return grid;
            }
        }
        
        public static IGrid MediumGridFourSolution
        {
            get
            {
                const string rawGrid = @"[
                                [4,3,2, 5,1,7, 8,9,6],
                                [6,5,8, 9,4,3, 2,7,1],
                                [9,7,1, 6,2,8, 5,3,4],
                                
                                [2,6,3, 8,5,9, 1,4,7],
                                [5,4,9, 2,7,1, 3,6,8],
                                [1,8,7, 3,6,4, 9,5,2],
                                
                                [8,1,5, 4,3,6, 7,2,9],
                                [7,2,4, 1,9,5, 6,8,3],
                                [3,9,6, 7,8,2, 4,1,5]
                             ]";

                var grid = DeserializeGrid(rawGrid);
                return grid;
            }
        }
        
        public static IGrid HardGridOne
        {
            get
            {
                const string rawGrid = @"[
                                [0,6,2, 0,0,3, 0,0,0],
                                [0,3,0, 0,5,0, 0,0,1],
                                [0,4,0, 7,1,0, 0,0,0],
                                
                                [0,0,0, 0,4,0, 0,2,3],
                                [2,0,0, 0,0,0, 0,0,5],
                                [4,9,0, 0,6,0, 0,0,0],
                                
                                [0,0,0, 0,7,4, 0,5,0],
                                [8,0,0, 0,2,0, 0,1,0],
                                [0,0,0, 5,0,0, 2,8,0]
                             ]";

                var grid = DeserializeGrid(rawGrid);
                return grid;
            }
        }
        
        public static IGrid HardGridOneSolution
        {
            get
            {
                const string rawGrid = @"[
                                [1,6,2, 4,8,3, 5,9,7],
                                [7,3,9, 6,5,2, 8,4,1],
                                [5,4,8, 7,1,9, 6,3,2],
                                
                                [6,8,5, 1,4,7, 9,2,3],
                                [2,1,7, 9,3,8, 4,6,5],
                                [4,9,3, 2,6,5, 1,7,8],
                                
                                [9,2,1, 8,7,4, 3,5,6],
                                [8,5,4, 3,2,6, 7,1,9],
                                [3,7,6, 5,9,1, 2,8,4]
                             ]";

                var grid = DeserializeGrid(rawGrid);
                return grid;
            }
        }
        
        public static IGrid HardGridTwo
        {
            get
            {
                const string rawGrid = @"[
                                [0,0,0, 6,0,0, 8,0,0],
                                [0,9,0, 0,5,4, 0,0,1],
                                [0,0,0, 0,2,0, 7,5,0],
                                
                                [0,0,7, 0,0,9, 3,0,2],
                                [0,0,0, 0,0,0, 0,0,0],
                                [3,0,9, 8,0,0, 5,0,0],
                                
                                [0,7,3, 0,9,0, 0,0,0],
                                [9,0,0, 1,6,0, 0,2,0],
                                [0,0,4, 0,0,7, 0,0,0]
                             ]";

                var grid = DeserializeGrid(rawGrid);
                return grid;
            }
        }
        
        public static IGrid HardGridTwoSolution
        {
            get
            {
                const string rawGrid = @"[
                                [5,3,2, 6,7,1, 8,4,9],
                                [7,9,8, 3,5,4, 2,6,1],
                                [1,4,6, 9,2,8, 7,5,3],
                                
                                [8,6,7, 5,4,9, 3,1,2],
                                [4,5,1, 7,3,2, 6,9,8],
                                [3,2,9, 8,1,6, 5,7,4],
                                
                                [2,7,3, 4,9,5, 1,8,6],
                                [9,8,5, 1,6,3, 4,2,7],
                                [6,1,4, 2,8,7, 9,3,5]
                             ]";

                var grid = DeserializeGrid(rawGrid);
                return grid;
            }
        }
        
        public static IGrid HardGridThree
        {
            get
            {
                const string rawGrid = @"[
                                [0,0,0, 3,0,0, 0,9,0],
                                [5,0,3, 0,0,9, 0,0,1],
                                [0,0,0, 0,1,0, 0,6,4],
                                
                                [0,7,0, 0,8,4, 0,0,0],
                                [0,0,5, 0,0,0, 8,0,0],
                                [0,0,0, 2,9,0, 0,4,0],
                                
                                [9,5,0, 0,7,0, 0,0,0],
                                [8,0,0, 6,0,0, 2,0,9],
                                [0,3,0, 0,0,1, 0,0,0]
                             ]";

                var grid = DeserializeGrid(rawGrid);
                return grid;
            }
        }
        
        public static IGrid HardGridThreeSolution
        {
            get
            {
                const string rawGrid = @"[
                                [1,2,4, 3,6,7, 5,9,8],
                                [5,6,3, 8,4,9, 7,2,1],
                                [7,8,9, 5,1,2, 3,6,4],
                                
                                [2,7,6, 1,8,4, 9,5,3],
                                [4,9,5, 7,3,6, 8,1,2],
                                [3,1,8, 2,9,5, 6,4,7],
                                
                                [9,5,2, 4,7,8, 1,3,6],
                                [8,4,1, 6,5,3, 2,8,9],
                                [6,3,7, 9,2,1, 4,8,5]
                             ]";

                var grid = DeserializeGrid(rawGrid);
                return grid;
            }
        }
        
        public static IGrid HardGridFour
        {
            get
            {
                const string rawGrid = @"[
                                [0,0,0, 0,2,3, 0,7,1],
                                [0,0,0, 0,1,0, 0,4,0],
                                [5,0,0, 8,0,0, 0,3,0],
                                
                                [8,0,0, 0,0,0, 0,2,5],
                                [0,0,0, 0,6,0, 0,0,0],
                                [1,2,0, 0,0,0, 0,0,4],
                                
                                [0,5,0, 0,0,2, 0,0,6],
                                [0,7,0, 0,4,0, 0,0,0],
                                [9,3,0, 7,8,0, 0,0,0]
                             ]";

                var grid = DeserializeGrid(rawGrid);
                return grid;
            }
        }
        
        public static IGrid HardGridFourSolution
        {
            get
            {
                const string rawGrid = @"[
                                [6,8,4, 9,2,3, 5,7,1],
                                [7,9,3, 6,1,5, 2,4,8],
                                [5,1,2, 8,7,4, 6,3,9],
                                
                                [8,6,7, 4,9,1, 3,2,5],
                                [3,4,5, 2,6,8, 9,1,7],
                                [1,2,9, 3,5,7, 8,6,4],
                                
                                [4,5,8, 1,3,2, 7,9,6],
                                [2,7,6, 5,4,9, 1,8,3],
                                [9,3,1, 7,8,6, 4,5,2]
                             ]";

                var grid = DeserializeGrid(rawGrid);
                return grid;
            }
        }
        
        public static IGrid SpecificCaseForPairInRow
        {
            get
            {
                const string rawGrid = @"[
                                [0,0,2, 6,7,1, 8,0,0],
                                [7,9,8, 3,5,4, 2,6,1],
                                [0,0,0, 9,2,8, 7,5,0],

                                [0,0,7, 5,0,9, 3,0,2],
                                [0,0,0, 7,3,0, 0,0,0],
                                [3,0,9, 8,0,0, 5,7,0],

                                [2,7,3, 4,9,5, 0,0,0],
                                [9,8,5, 1,6,3, 4,2,7],
                                [0,0,4, 2,8,7, 9,3,5]
                             ]";

                var grid = DeserializeGrid(rawGrid);
                return grid;
            }
        }
        
        public static IGrid SpecificCaseForPairInSquare
        {
            get
            {
                const string rawGrid = @"[
                                [0,0,0, 3,0,0, 5,9,0],
                                [5,0,3, 0,0,9, 7,0,1],
                                [0,0,9, 0,1,0, 3,6,4],
                                
                                [0,7,0, 0,8,4, 9,0,0],
                                [4,9,5, 0,0,0, 8,0,0],
                                [0,0,0, 2,9,0, 0,4,0],
                                
                                [9,5,0, 0,7,0, 0,0,0],
                                [8,0,0, 6,0,0, 2,0,9],
                                [0,3,0, 9,0,1, 0,0,0],
                             ]";

                var grid = DeserializeGrid(rawGrid);
                return grid;
            }
        }

        private static Grid DeserializeGrid(string rawGrid)
        {
            var matrix = JsonConvert.DeserializeObject<short[,]>(rawGrid);
            var grid = Grid.FromSudokuMatrix(matrix);
            return grid;
        }
    }
}

