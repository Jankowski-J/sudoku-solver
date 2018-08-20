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
        let cellFactory (i: int)(j: int)(c: int8[,]) =
            let value = c.[i, j]           
            match value with 
            | 0y -> createEmptyCell(Commons.row j c |> Seq.append (Commons.col i c))
            | x -> createCell(x)
            
        let gridCells = Array2D.init<SudokuCell> 9 9 (fun i j -> (cellFactory i j cells))
            
        let grid: Models.SudokuGrid = { cells = gridCells }
        
        grid
        