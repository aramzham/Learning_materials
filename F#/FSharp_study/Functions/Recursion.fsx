let rec sum items =
    match items with
    | [] -> 0 // base case
    | head::tail -> head + (sum tail)
    
printfn "%d" (sum [1..10])

let sum2 items =
    let rec loop accumulator items =
        match items with
        | [] -> accumulator
        | head::tail -> loop (accumulator + head) tail
    loop 0 items
    
printfn "%i" (sum2 [1..10])

let fold f accumulator items =
    let rec loop f accumulator items =
        match items with
        | [] -> accumulator
        | head::tail -> loop f (f  accumulator head) tail
    loop f accumulator items
    
printfn "%d" (fold (*) 1 [1..5])

let sum3 = fold (+) 0
printfn "%i" (sum3 [1..10])

let prod = fold (*) 1
printfn "%d" (prod [1..4])


// tail-recursion
let rec sum4 running_total items =
    match items with
    | [] -> running_total
    | head::tail ->
        sum4 (running_total + head) tail

let result4 = sum4 0 [1..100000] // if you try with non-tail-recursive version, the terminal will explode with stackoverflow exception
printfn "%i" result4


// let mutable state = 0
// let mutable running = true
// while running do
//     printfn $"State: {state}"
//     let i = System.Console.ReadLine()
//     let (s, v) = System.Int32.TryParse i
//     match s with
//     | true -> state <- state + v
//     | false -> if i = "x" then
//                    running <- false

let rec appLoop state =
    printfn $"State: {state}"
    let i = System.Console.ReadLine()
    let (s, v) = System.Int32.TryParse i
    match s with
    | true -> appLoop (state + v)
    | _ -> if i <> "x" then appLoop state

appLoop 0