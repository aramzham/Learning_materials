open System

let promptUser() =
    Console.Write("(d)eposit, (w)ithdraw or e(x)it: ")
    Console.ReadLine() // as return
    
let getAmount () =
    Console.Write("Enter the amount of the transaction: ")
    Console.ReadLine() |> Decimal.Parse
    
[<EntryPoint>]
let main argv =
    Console.WriteLine("Hello from the transaction processor!")
    
    let mutable balance = 0m
    
    let mutable isRunning = true
    while isRunning do
        printfn $"balance: %f{balance}"
        
        let action = promptUser()
        printfn $"You told me to do this: %s{action}"
        
        balance <-
            match action with
            | "d" -> balance + getAmount() 
            | "w" -> balance - getAmount() 
            | _ ->
                isRunning <- action <> "x"
                balance
    
    Console.WriteLine("Bye!!")
    0 // must return something