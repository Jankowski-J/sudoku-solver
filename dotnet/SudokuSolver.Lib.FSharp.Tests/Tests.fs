module Tests

open SudokuSolver.Lib.FSharp
open System
open Xunit

[<Fact>]
let ``Test grid printing`` () = 
    let rawCells = Array2D.zeroCreate 9 9
    let grid: Models.SudokuGrid = Factory.createGrid rawCells 
    let printed = Utils.gridToString grid
    
    Assert.Equal('0', printed.[0])
