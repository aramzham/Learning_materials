open System

let promptUser() =
    Console.Write("(d)eposit, (w)ithdraw or e(x)it: ")
    Console.ReadLine() // as return
    
[<EntryPoint>]
let main argv =
    Console.WriteLine("Hello from the transaction processor!")
    
    let mutable isRunning = true
    while isRunning do
        let action = promptUser()
        printfn $"You told me to do this: %s{action}"
        
        isRunning <- action <> "x"
    
    Console.WriteLine("Bye!!")
    0 // must return something