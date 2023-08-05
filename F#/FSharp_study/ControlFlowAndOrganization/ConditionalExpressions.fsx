let compare x y =
    if x = y then $"{x} equals {y}"
    elif x < y then $"{x} is less than {y}"
    else $"{x} is greater than {y}" // this is required!!
    
printfn $"%s{compare 1 2}" 
printfn $"%s{compare 2 1}"
printfn $"%s{compare 1 1}"