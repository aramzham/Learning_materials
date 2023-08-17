let f x = x * x
let g x = x + 1
printfn "%i" (g (f 4))

let g_f x = g(f(x))
printfn "%d" (g_f 3)