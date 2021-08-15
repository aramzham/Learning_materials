// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System

open System
open System.Linq

let findCommon list =
    if list.[0] <> list.[1] && list.[0] <> list.[2] then list.[0]
    elif list.[0] = list.[1] then list.[2]
    else list.[1]

let findUniq list =
    let common = findCommon list
    for i in list
        if i <> common then i

let a = bmi 50.0 1.80
printfn $"{a}"
// Define a function to construct a message to print
let from whom =
    sprintf "from %s" whom

[<EntryPoint>]
let main argv =
    let message = from "F#" // Call the function
    printfn "Hello world %s" message
    0 // return an integer exit code