printfn "%i" ((+) ((+) 1 2) 3)

let add1 = (+) 1 // partial application
// here add1 is a function int -> int

printfn "%i" (add1 5)

let (|<>|) x y = if x>y then x-y else y-x

printfn "%d" (9 |<>| 3 |<>| 0)