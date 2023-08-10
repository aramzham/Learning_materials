let describeMatch x =
    match x with
    | 0 -> "zero"
    | 1 -> "one"
    | _ -> "neither zero or one"

let describe = function // same as match but without the single parameter
    | 0 -> "zero"
    | 1 -> "one"
    | _ -> "neither zero or one"
    
printfn $"value is %s{describe 1}"
printfn $"value is %s{describe 0}"
printfn $"value is %s{describe -1}"


let describeWithVariable = function
    | 0 | 1 | 2 -> "found 0, 1 or 2!"
    | a -> $"Something else found: {a}?" // with _ the value would not be captured
    
printfn $"with variable describe is %s{describeWithVariable 0}"
printfn $"with variable describe is %s{describeWithVariable 1}"
printfn $"with variable describe is %s{describeWithVariable 2}"
printfn $"with variable describe is %s{describeWithVariable 3}"


let detectZero = function
    | (0, 0) -> "both values are zero"
    | (var1, var2) & (0, _) -> $"first value is 0 in ({var1}, {var2})"
    | (var1, var2) & (_, 0) -> $"second value is 0 in ({var1}, {var2})"
    | _ -> "both are nonzero"
    
printfn $"val it: string = %s{detectZero (0, 0)}"
printfn $"val it: string = %s{detectZero (1, 0)}"
printfn $"val it: string = %s{detectZero (0, 2)}"
printfn $"val it: string = %s{detectZero (3, 4)}"
