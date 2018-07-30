namespace SudokuSolver.Lib.FSharp

type SudokuGrid = { cells: int8[,] }

type SudokuCell = { value: int; candidates: int8[] }

type SudokuRow = { cells: SudokuCell[] }


module Utils =
    open System.Text
    open System.Linq

    let mutable s = new StringBuilder()
    let getGrid (grid : SudokuGrid) = 
        for row in Enumerable.Range(0, grid.cells.GetLength(0)) do
            for column in Enumerable.Range(0, grid.cells.GetLength(1)) do
                s <- s.Append(grid.cells.[row, column])
            s <- s.AppendLine()
        s.ToString()
            