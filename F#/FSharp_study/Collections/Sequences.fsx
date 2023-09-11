printfn "%A" (seq {1..10}) // when printing sequences F# will show only 4 items

printfn "%A" (seq {for i in 1..10 -> i * i}) // squares

let s = seq {
    for i in 1..3 ->
        printfn "i is: %i" i // a sequence with side effect
        i * i
}

for i in s do
    printfn "%d" i


// this takes a while before output starts
//for i in [1..1000000000] do
//    if i < 10 then printfn "%d" i
// takes about 2 minutes to finish!!!!!!!!!!!!!!!!!!! BE CAREFUL WITH THIS CODE!!!!!!!!!!!!!!!!!

for i in seq {1..1000000000} do
    if i < 10 then printfn "%d" i
// instant finish!!