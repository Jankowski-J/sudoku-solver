module Tests

open SudokuSolver.Lib.FSharp
open System
open Xunit

[<Fact>]
let ``Test grid printing`` () = 
    let rawCells = Array2D.zeroCreate 9 9
    let grid: SudokuGrid = Factory.createGrid rawCells 
    let printed = Utils.getGrid grid
    
    Assert.Equal('0', printed.[0])
