// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System

module BasicFunctions = 
    let param = 200
    
    let sampleFunction1 x = x*x + 3

    let result = sampleFunction1 param

    printfn $"result of {param}*{param}+3 equals {result}"

    let sampleFunction2 (x:int) = 2 * x + 10

    let result2 = sampleFunction2(2 + 8)

    let sampleFunction3 x = 
        if x > 0 then
            x * x
        else 
            x + 100

    let result3 = sampleFunction3(20)
    let result3_1 = sampleFunction3(-6)

    printfn $"sampleFunction(20) = {result3}"
    printfn $"sampleFunction(-6) = {result3_1}"

module Immutability =
    let number = 2
    //let number = 3
    let mutable other = 23
    other <- 100
    printfn $"other number is {other}"

module Pipelines = 
    let square x = x * x
    let addOne x = x+1
    let isOdd x = x % 2 <> 0

    let numbers = [1;2;3;4;5]

    let squareOddValuesAndAddOne values = 
        let odds = List.filter isOdd values
        let squares = List.map square odds
        let result = List.map addOne squares
        result

    printfn $"processing {numbers} through 'squareOddValuesAndAddOne' produces: {squareOddValuesAndAddOne numbers}"

    let squareOddValuesAndAddOneNested values = 
        List.map addOne (List.map square (List.filter isOdd values))

    printfn $"processing {numbers} through 'squareOddValuesAndAddOneNested' produces: {squareOddValuesAndAddOneNested numbers}"

    let squareOddValuesAndAddOnePipeline values =
        values
        |> List.filter isOdd
        |> List.map square
        |> List.map addOne

    printfn $"processing {numbers} through 'squareOddValuesAndAddOnePipeline' produces: {squareOddValuesAndAddOnePipeline numbers}"

    let squareOddValuesAndAddOneShorterPipeline values =
        values
        |> List.filter isOdd
        |> List.map(fun x -> x 
                             |> square 
                             |> addOne)

    printfn $"processing {numbers} through 'squareOddValuesAndAddOneShorterPipeline' produces: {squareOddValuesAndAddOneShorterPipeline numbers}"

    let squareOddValuesAndAddOneComposition =
        List.filter isOdd >> List.map (square >> addOne)

    printfn $"processing {numbers} through 'squareOddValuesAndAddOneComposition' produces: {squareOddValuesAndAddOneComposition numbers}"

// Define a function to construct a message to print
let from whom =
    sprintf "from %s" whom

[<EntryPoint>]
let main argv =
    let message = from "F#" // Call the function
    printfn "Hello world %s" message
    0 // return an integer exit code