let l0 = []
let l1 = [1;2;3] // if you declare inline => put ;
let l2 = [
    1
    2
    3
] // declaring vertically => no need of ;
let l3: float list = [1; 2; 3.]
printfn "%A" l3

printfn "%i" l1[1]
printfn "%i" l1.[1] // old way

printfn "%b" (l1[2] = 5)
// l1[2] <- 0 // error, you can do it with array but cannot do it with lists

printfn "%A" (10 :: l1) // puts 10 as head
printfn "%A" (l1 @ [10]) // append 2 lists together


let list = [1..100] // 1 to 100
printfn "%A" (list[10..14]) // bounded on both ends
printfn "%A" (list[..4]) // first five
printfn "%A" (list[95..]) // last five


printfn "%b" list.IsEmpty
printfn "%d" list.Length
printfn "%d" list.Head
printfn "%A" list.Tail // all the elements except Head
printfn "%d" list.Tail.Tail.Head
printfn "%d" (list.Item(1)) // if you don't want to use indexer
