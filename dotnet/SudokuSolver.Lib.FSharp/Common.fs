namespace SudokuSolver.Lib.FSharp

module Commons =
    let row i (arr: 'T[,]) = arr.[i..i, *] |> Seq.cast<'T>
    
    let col i (arr: 'T[,]) = arr.[*, i..i] |> Seq.cast<'T>