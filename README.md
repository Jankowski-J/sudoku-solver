# sudoku-solver
Repository for various Sudoku solvers implementations

# What is this about?
I'm going to implement a solver for Sudoku puzzles. 
I've been into Sudoku as my past-time activity back-and-forth in my life and the idea of implementing a solver by myself has been popping up once in a while. I want to use this as an opportunity to get started with functional programming, however the first implementation will be far from it. 
First implementation will be in C#, as I'm most profficient in this language.
When I wrap my head around that, I will go for F# and JavaScript.

## Details
I will prepare search for sample Sudoku puzzles and their solutions. Each implementation will be considered *good enough* when it successfully solves each of them. 
Efficiency issues are side ones as of now.

### Input
Each puzzle will be described as a JSON array of arrays (basically 9x9 matrix) and the output should be the same as well.
Each subarray contains 9 numbers, 1 for each cell of a row. '0' means empty field.

## Implementation status
- [x] provide test cases
- [ ] C#
- [ ] F#
- [ ] JavaScript
