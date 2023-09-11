[||] // empty array
[|1;2;3|] // for inline declaration you need ;
[|
    1
    2
    3
|] // for vertical one not

printfn "%A" [|1..5|]
printfn "%A" [|for x in 1..5 -> x * 2|]

// if you want to explicitly specify type use :
let array : float array = [|1.9; 2.4; 4.|]
printfn "%A" array

printfn "%A" Array.empty

printfn "%A" (Array.create 5 1.) // repeating 1.0 5 times

printfn "%A" (Array.zeroCreate 5)

printfn "%A" (Array.init 5 (fun index -> index * 2))

printfn "%f" array[2]

array[1] <- -2.
printfn "%A" array

let array2d = [| [|1..3|]; [|4..6|] |]
printfn "%A" array2d
printfn "%d" (array2d[0][0])

let a1 = [|1..3|]
let a2 = [|4..6|]
let a3 = [|7|]

printfn "%A" (Array.append a1 a2)
printfn "%A" (Array.concat [a1; a2; a3])