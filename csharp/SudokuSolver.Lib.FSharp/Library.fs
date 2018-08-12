namespace SudokuSolver.Lib.FSharp

type SudokuCell = { value: int; candidates: int8[] }

type SudokuGrid = { cells: SudokuCell[,] }

type SudokuRow = { cells: SudokuCell[] }

module Utils =
    open System.Text
    open System.Linq

    let mutable s = new StringBuilder()
    let getGrid (grid : SudokuGrid) = 
        for row in Enumerable.Range(0, grid.cells.GetLength(0)) do
            for column in Enumerable.Range(0, grid.cells.GetLength(1)) do
                s <- s.Append(grid.cells.[row, column].value)
            s <- s.AppendLine()
        s.ToString()
        
   


module Factory = 
    let createCell (value: int) =
        match value with 
        | 0 -> { value = value; candidates = Array.init 9 (fun index -> (int8) (index + 1)) }  
        | _ -> { value = value; candidates = Array.init 0 (fun _ -> (int8) 0) }
        
    let createGrid (cells: int8[, ]) =
        let gridCells = 
            cells
            |>  Array2D.map (fun v -> createCell((int) v)) 
            
        let grid: SudokuGrid = { cells = gridCells }
        
        grid
        