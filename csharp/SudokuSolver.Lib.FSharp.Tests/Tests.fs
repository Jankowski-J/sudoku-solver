module Tests

open SudokuSolver.Lib.FSharp
open System
open Xunit

[<Fact>]
let ``My test`` () =
    Assert.True(true)
    
[<Fact>]
let ``Test grid printing`` () = 
    let grid: SudokuGrid = { cells = Array2D.zeroCreate 9 9 }
    let printed = Utils.getGrid grid
    
    Assert.Equal('0', printed.[0])
