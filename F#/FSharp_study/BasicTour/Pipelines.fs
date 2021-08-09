module Pipelines 
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