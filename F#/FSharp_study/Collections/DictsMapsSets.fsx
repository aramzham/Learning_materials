let parts = 
    ["Nuts", 23; "Bolts", 53; "Washers", 77]
    |> dict

printfn "%i" parts["Nuts"]

// ERRORS!!
// parts["Nuts"] <- 11
// parts.Add("Screws", 3)
// parts.Remove("Nuts")

open System.Collections.Generic

let mutableDictionary = new Dictionary<_, _>()
mutableDictionary["a"] <- 1
mutableDictionary["b"] <- 2
printfn "%A" mutableDictionary


// maps are native to F# and are immutable dictionaries
let capitals = ["USA", "Washington D.C"; "Canada", "Ottawa"; "France", "Paris"] |> Map.ofList
    
printfn "%A" capitals

let usa = capitals["USA"]
let canada = capitals["Canada"]
let newCapitals = 
    capitals
    |> Map.add "Japan" "Tokyo"
    |> Map.remove "France"

printfn "%A" newCapitals


let set1 = ["A"; "B"; "A"; "A"; "C"] |> Set.ofList
let set2 = [| "C"; "D"; "A"; "E" ; "C" |] |> Set.ofArray
let set3 = [|"B"; "C"|] |> Set.ofArray

printfn "set1 = %A" set1
printfn "set2 = %A" set2
printfn "set3 = %A" set3

let set4 = set1.Add("Z")
let set5 = set2.Remove("F") // no exception thrown!!

let union = set1 + set2 + set3
let distinctToSet1 = set1 - set2 // only those elements that exist in set1
let intersect = set1 |> Set.intersect set2 // exist in both
let isSubset = set1 |> Set.isSubset set3

printfn "union = %A" union
printfn "distinctToSet1 = %A" distinctToSet1
printfn "intersect = %A" intersect
printfn "isSubset = %b" isSubset