module CommonTests

open Newtonsoft.Json
open SudokuSolver.Lib.FSharp
open System
open Xunit

let compareSequences = Seq.compareWith Operators.compare

let raw = @"[
                  [0,2,0, 6,0,0, 0,7,0],
                  [0,0,0, 0,5,0, 9,0,1],
                  [0,7,9, 0,0,0, 5,0,0],
                  
                  [0,0,0, 5,6,0, 4,0,0],
                  [9,0,0, 4,0,3, 0,0,7],
                  [0,0,8, 0,2,1, 0,0,0],
              
                  [0,0,7, 0,0,0, 1,3,0],
                  [6,0,3, 0,9,0, 0,0,0],
                  [0,1,0, 0,0,2, 0,8,0]
                ]"
let deserialized = JsonConvert.DeserializeObject<int8 [,]>(raw)

[<Fact>]
let ``row should correct row``() =  
    
    let row = SudokuSolver.Lib.FSharp.Commons.row 1 deserialized |> Seq.toArray
    
    let expected = 
        [|
            0y
            0y
            0y
            0y
            5y
            0y
            9y
            0y
            1y
        |]
        
    let areCollectionsTheSame = (compareSequences expected row = 0)
    Assert.True(areCollectionsTheSame)
    
 
[<Fact>]
let ``col should correct column``() =
    let column = SudokuSolver.Lib.FSharp.Commons.col 1 deserialized |> Seq.toArray
    
    let expected = 
        [|
            2y
            0y
            7y
            0y
            0y
            0y
            0y
            0y
            1y
        |]
        
    let areCollectionsTheSame = (compareSequences expected column = 0)
    Assert.True(areCollectionsTheSame)
    

[<Fact>]
let ``Test grid printing`` () = 
    let rawCells = Array2D.zeroCreate 9 9
    let grid: Models.SudokuGrid = Factory.createGrid rawCells 
    let printed = Utils.gridToString grid
    
    Assert.Equal('0', printed.[0])

    