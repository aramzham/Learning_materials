let t1 = 1, "q"
let t2 = (1, "q")
printfn "%b" (t1=t2)

let t3 = (1,"b")
let t4 = (1,2.0, true)
printfn "%b" (t1=t3)
//printfn "%b" (t1=t4) // produces error

printfn "%i" (fst t1)
printfn "%s" (snd t1)

let third (_,_,c) = c
let t5 = (1, "b", 2.0)
printfn "%f" (third t5)

let print tuple =
    match tuple with
    | (a, b) -> printfn "(%A, %A)" a b

print (1, "a")

let divRem a b =
    let x = a / b
    let y = a % b
    (x, y)

let result = divRem 5 2
print result

let (roundedResult, remainder) = divRem 11 2
printfn "(%i, %d)" roundedResult remainder
