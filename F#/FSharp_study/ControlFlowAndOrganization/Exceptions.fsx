exception Error1 of string * int
exception Error2 of string * int * int
exception Error3 of string

let function1 x y =
    try
        try
            if x = 6 then raise (Error3("Value 6 is not permitted!!"))
            elif x = y then raise (Error1("Values are equal", x))
            else raise (Error2("Values are not equal", x, y))
        with
            | Error1(str, x) -> printfn $"{str}: {x}"
            | Error2(str, x, y) -> printfn $"{str}: {x}, {y}"
            // | _ -> ()
    finally
        printfn "doing finally block"
        
function1 10 10
function1 1 2
function1 6 7 // unhandled exception will be raised
function1 8 9 // this will not be called