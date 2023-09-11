let list = List.init 5 (fun x -> x * 2) // here the index will be taken as item
printfn "%A" list

let list1 = let r = System.Random() in List.init 10 (fun x -> r.Next(0, 10))
printfn "%A" list1

printfn "%A" (List.sort [6..-2..0])
printfn "%A" (List.sortDescending [1..5])
printfn "%A" (List.rev ["a"; "b"; "c"])

// any
printfn "%A" (List.exists (fun x -> x > 2) [1..40])

// all items matching the predicate
printfn "%A" (List.where (fun x -> x < 3) [1..40])

// if these 2 doesn't find a matching element => will throw an exception
// find index
printfn "%i" (List.findIndex (fun x -> x = 30) [1..40])
// find element
printfn "%d" (List.find (fun x -> x / 2 = 4) [1..340])


// try variants of those
let isEven x = x % 2 = 0

match List.tryFind isEven list1 with
| Some value -> printfn "The first even value is %d" value
| None -> printfn "There's no even value in the list"

match List.tryFindIndex isEven list1 with
| Some value -> printfn "The first even value is at position %i" value
| None -> printfn "There's no even value in the list"


printfn "%A" (List.concat [[1..4]; [1..5]; [10..12]])
printfn "%A" (List.append [1..5] [6..8]) // same as @ operator
printfn "%A" (List.removeAt 1 [1..5]) // returns a new list


printfn "%A" (List.take 2 [1..5])
printfn "%A" (List.takeWhile (fun x -> x < 0) [-1; -2; 3; 5; -23]) // take while the predicate returns true
printfn "%A" (List.skip 3 [1..5])
printfn "%A" (List.skipWhile (fun x -> x < 0) [-1; -2; 3; 5; -23]) // skip while the predicate is true then take the rest

printfn "%A" ([1..10] |> List.skip 3 |> List.take 2)