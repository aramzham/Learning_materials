for i in [1;2;3] do
    printfn $"%A{i}"
    
for i in [1..3] do // this will allocate an array in memory
    printfn $"%A{i}"
    
for i = 1 to 3 do // this will only allocate i
    printfn $"%A{i}"
    
let mutable i = 0
while i < 3 do
    printfn $"%A{i}"
    i <- i + 1
    

// Calculate sum
// imperative manner
let data = [1;2;3]
let mutable mutableSum = 0
for value in data do mutableSum <- mutableSum + value
printfn $"imperative value %i{mutableSum}"

// expression way
printfn $"functional/expression way %i{List.sum(data)}"

// fold
let foldSum = List.fold (+) 0 data
printfn $"fold sum = %i{foldSum}"

// iter iteration
List.iter (fun x -> printfn "%i" x) data


printfn $"%A{[for x in 1..5 do yield x*x]}"
// shortcut
printfn $"%A{[for x in 1..5 -> x*x]}"
// nested loops
let nestedLoopResult = [
    for r in 1..8 do
        for c in 1..8 do
            if r <> c then yield (r,c) // <> and not != is used in F#
]

printfn $"%A{nestedLoopResult}"