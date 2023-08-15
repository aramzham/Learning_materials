module Transactions.Driver

open System

module UserConsole =
    let private promptUser() =
        Console.Write("(d)eposit, (w)ithdraw or e(x)it: ")
        Console.ReadLine() // as return
    
    let private getAmount () =
        Console.Write("Enter the amount of the transaction: ")
        Console.ReadLine() |> Decimal.Parse
        
    let userLoop () =
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