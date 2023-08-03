open System;

let a = [|1;2;3;|]
printfn $"%A{a}"
Console.WriteLine(a)

printf "Enter a command, human!!: "
let input = Console.ReadLine()
printfn $"Your command is: %s{input}"


let myInteger = 1
let myFloat = 3.
let myString = "some string here"
let myExplicitFloat: float = 2
myInteger = 2 // this is not an assignment, variables are immutable => this is a comparison => will return bool false 
printfn $"%A{myInteger}"

let myArray = [|1.;4; 20.|] // this is a mutable float array
printfn $"%f{myArray[0]}"
myArray[1] = 5. // this is not an assignment
myArray[1] <- 20. // now this IS an assignment
printfn $"%A{myArray}"

let myList = [1;2;3] // without pipes the array is IMMUTABLE
myList[2] = 5
printfn $"is myList[2] equal to 5 ? %b{myList[2] = 5}"
// myList[1] <- 30 // compile error
printfn $"%A{myList}"

// ways to create a list/array
[1..5]
[|0..5|]
printfn "%A" [|10..-2..0|] // [start..step..end] => [|10;8;6;4;2;0|]

let aList = [1;2;3]
let withNewHead = -5 :: aList
printfn "after adding a new head %A" withNewHead

let head :: tail = aList
printfn "tail is %A" tail
printfn "head is %d" head
// concat
let conc = aList @ [40; 32]
printfn "concatenated = %A" conc


// create a function
let square a = a * a
printfn $"square of 2 is %d{square 2}"

let floatSquare (x: float) = x * x // parameter will be float
let returnTypeFloatSquare x : float = 4. + 5.
let argument = 2
let returnValue = returnTypeFloatSquare argument
printfn "%f" returnValue

let add a b =
    let c = a + b
    c
printfn $"1 + 2 = %d{add 1 2}"

// pipe forward
printfn $"%d{10 |> add 1}" // 11
printfn $"%d{20 |> (10 |> add)}" // 30


[<EntryPoint>] // explicitly specifying the entry point
let main argv = // main is a conventional name, it must have one parameter, argv is again a convention
    printfn "%A" argv
    
    Console.WriteLine("hello from transaction processor")
    
    Console.Write("(d)eposit, (w)ithdraw or e(x)it")
    let action = Console.ReadLine()
    printfn $"You told me to do this: %A{action}"
    
    Console.WriteLine("Bye!")
    
    0 // must return a value