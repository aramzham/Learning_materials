let isItSomething = function
    | Some i -> $"Yes, it's something: {i}"
    | None -> "Nope, there's nothing"

printfn "%s" (isItSomething(Some 4))
printfn "%s" (isItSomething(None))

let addIntsAsOptions xoption yoption =
    match xoption with
    | Some xvalue -> 
        match yoption with
        | Some yvalue -> Some (xvalue + yvalue)
        | None -> None
    | None -> None

printfn "%A" (addIntsAsOptions (Some 1) (Some 2))
printfn "%A" (addIntsAsOptions None (Some 2))
printfn "%A" (addIntsAsOptions (Some 1) None)
printfn "%A" (addIntsAsOptions None None)

let parse s =
    let (succeeded, value) = System.Double.TryParse(string s)
    if succeeded then Some value else None

printfn "%A" (parse "4.3")
printfn "%A" (parse "my name is Slim Shady")

let describe = function
    | Some x -> $"The value is {x}"
    | None -> "There's no value"

printfn "%s" ("4343.32940" |> parse |> describe)
printfn "%s" ("your door is ringing" |> parse |> describe)

printfn "%A" ("1.1" |> parse |> Option.map (fun x -> x * 2.) |> describe)
printfn "%A" ("your door is ringing" |> parse |> Option.map (fun x -> x * 2.) |> describe) // if there's no value the fun will not be called by option.map

let happyPath = "45.13"
                |> parse
                |> Option.bind (fun x-> x* 2. |> Some)
                |> describe
let unhappyPath = "NaN"
                  |> parse
                  |> Option.bind (fun x-> x* 2. |> Some)
                  |> describe

printfn "%A" happyPath
printfn "%A" unhappyPath