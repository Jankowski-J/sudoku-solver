namespace SudokuSolver.Lib.FSharp

module Factory = 
    open Models 
    
    
    let createCell (value: int8) =
        match value with 
        | 0y -> { value = value; candidates = Array.init 9 (fun index -> (int8) (index + 1)) }  
        | _ -> { value = value; candidates = Array.init 0 (fun _ -> (int8) 0) }
        
    let createCellFromInt (value: int) = 
         createCell((int8)value)
        
    let createEmptyCell (excludedCandidates: seq<int8>) =
        let candidates = 
            Seq.init 9 (fun index -> (int8) (index + 1)) 
            |> Seq.except excludedCandidates 
            |> Seq.toArray
            
        {value = 0y; candidates = candidates }
        
    let createGrid (cells: int8[, ]) =
        let durr = Array2D.init<SudokuCell> 9 9 (fun i j -> createCell(cells.[i, j]))
        //let empty: SudokuGrid[, ] = Array2D.init (cells.GetLength(0) ,cells.GetLength(1), (fun v -> None))
        let gridCells = 
            cells
            |> Array2D.map (fun v -> createCell(v)) 
            
        let grid: Models.SudokuGrid = { cells = gridCells }
        
        grid
        