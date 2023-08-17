let infixFoldSum = List.fold (+) 0 [1..100]
printfn "%d" infixFoldSum

// partially apply fold
let sum = List.fold (+) 0
printfn "%i" (sum [1..100])