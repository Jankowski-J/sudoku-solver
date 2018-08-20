module FactoryTests

open Newtonsoft
open Newtonsoft.Json
open SudokuSolver.Lib.FSharp
open System
open Xunit

let compareSequences = CommonTests.compareSequences

[<Fact>]
let ``Cell initialized with non-zero value should have correct value``() =
    let cell = Factory.createCell 6y
    Assert.Equal(6y, cell.value)

[<Fact>]
let ``Cell with value should have no candidates``() =
    let cell = Factory.createCell 6y
    Assert.Empty(cell.candidates)

[<Fact>]
let ``Cell with value equal to zero should have 9 candidates``() =
    let cell = Factory.createCell 0y
    Assert.Equal(9, cell.candidates.Length)

[<Fact>]
let ``Cell initialized with zero should have value equal to zero``() =
    let cell = Factory.createCell 0y
    Assert.Equal(0y, cell.value)

[<Fact>]
let ``Cell initialized with zero should have correct candidates``() =
    let cell = Factory.createCell 0y
    
    let expected : int8 array =
        [| (int8) 1
           (int8) 2
           (int8) 3
           (int8) 4
           (int8) 5
           (int8) 6
           (int8) 7
           (int8) 8
           (int8) 9 |]
    
    let result = cell.candidates
    let expectedSameAsResult = (compareSequences expected result = 0)
    Assert.True(expectedSameAsResult)

[<Fact>]
let ``Creating empty cell should properly assing candidates``() =
    let row =
        [| 0
           2
           0
           6
           0
           0
           0
           7
           0 |]
        |> Utils.toInt8Array
        |> Array.toSeq
    
    let cell = Factory.createEmptyCell (row)
    let expectedCandidates = [|1;3;4;5;8;9|] |> Utils.toInt8Array
    let candidatesAreTheSame = (compareSequences expectedCandidates cell.candidates = 0)
    Assert.Equal(0y, cell.value)
    Assert.True(candidatesAreTheSame)

[<Fact>]
let ``createGrid should correctly assign candidates``() =
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
    let grid = Factory.createGrid deserialized
    let actual = grid.cells.[0, 0]
    
    let expectedCandidates = [|1;3;4;5;8|] |> Utils.toInt8Array
    let removedCandidates = [|2;6;7;9|] |> Utils.toInt8Array 
    
    let expected = Factory.createEmptyCell (removedCandidates)
    let areCandidatesTheSame = (compareSequences expectedCandidates actual.candidates = 0)
    
    Assert.Equal(expected.value, actual.value)
    Assert.True(areCandidatesTheSame)
