let a =
    [ 1..10 ] |> List.filter (fun i -> i % 2 = 0) |> List.map ((*) 2) |> List.sum

printfn "%i" a

// sin 2. + 1. // will calculate sin 2. then add 1
let sinus = sin <| 2. + 1. // will perform 2 + 1 => sin of 3
printfn "%f" sinus

let minimum = 23 |> min <| 4
printfn "%d" minimum

let tupleMin = (32, 3) ||> min
printfn "%i" tupleMin

let mult3 a b c = a * b * c
let tripleMult = (2, 4, 5) |||> mult3
printfn "%d" tripleMult
