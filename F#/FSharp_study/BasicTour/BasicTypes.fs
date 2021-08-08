namespace BasicTypes

module Numbers =
    let numbers = [0 .. 99]
    let tableOfSquares = [for i in numbers -> (i, i*i)]
    let tableOfSquares1 = [for i in 0..99 -> (i, i*i)]
    printfn $"fist table = {tableOfSquares}"
    printfn $"seconds table = {tableOfSquares}"

module Booleans =
    let True = true
    printfn $"True is {not True}"

module Strings = 
    let string1 = "Hello"
    let string2 = "World"

    let helloWorld = string1 + ", " + string2 + "!"
    printfn "%s" helloWorld

    let substring = helloWorld.[1..3]
    printfn $"{substring}"

module Tuples = 
    let t = (1,2)
    let swap (a,b) = (b,a)

    printfn $"{swap(t)}"

    let sampleStructTuple = struct (1, 2)
    //let thisWillNotCompile: (int*int) = struct (1, 2)
    
    // Although you can
    let convertFromStructTuple (struct(a, b)) = (a, b)
    let convertToStructTuple (a, b) = struct(a, b)