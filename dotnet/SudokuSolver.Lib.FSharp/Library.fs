namespace SudokuSolver.Lib.FSharp

module Models = 
    type SudokuCell = { value: int8; candidates: int8[] }

    type SudokuGrid = { cells: SudokuCell[,] }

    type SudokuRow = { cells: SudokuCell[] }

module Utils =
    open System.Text
    open System.Linq

    let toInt8Array(arr: int[]) =
        arr |> Array.map (fun v -> (int8)v)

    let gridToString (grid : Models.SudokuGrid) = 
        let mutable s = new StringBuilder()
        for row in Enumerable.Range(0, grid.cells.GetLength(0)) do
            for column in Enumerable.Range(0, grid.cells.GetLength(1)) do
                s <- s.Append(grid.cells.[row, column].value)
            s <- s.AppendLine()
        s.ToString()
        
   


