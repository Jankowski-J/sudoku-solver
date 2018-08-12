module FactoryTests

open SudokuSolver.Lib.FSharp
open System
open Xunit

let compareSequences = Seq.compareWith Operators.compare

[<Fact>]
let ``Cell initialized with non-zero value should have correct value`` () = 
    let cell = Factory.createCell 6
        
    Assert.Equal(6, cell.value)
    
[<Fact>]
let ``Cell with value should have no candidates`` () = 
    let cell = Factory.createCell 6
    
    Assert.Empty(cell.candidates)
   
[<Fact>]
let ``Cell with value equal to zero should have 9 candidates`` () =
    let cell = Factory.createCell 0
    
    Assert.Equal(9, cell.candidates.Length)
    
[<Fact>]
let ``Cell initialized with zero should have value equal to zero`` () =
    let cell = Factory.createCell 0
    
    Assert.Equal(0, cell.value)

[<Fact>]
let ``Cell initialized with zero should have correct candidates`` () =
    let cell = Factory.createCell 0
    
    let expected: int8 array = [| (int8)1; (int8)2; (int8)3; (int8)4; (int8)5; (int8)6; (int8)7;(int8) 8; (int8)9 |]
        
    let result = cell.candidates
    
    let expectedSameAsResult = (compareSequences expected result = 0)
    
    Assert.True(expectedSameAsResult)