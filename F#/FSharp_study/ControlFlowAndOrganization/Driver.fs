module Transactions.Driver

open System

module UserConsole =
    let private promptUser() =
        Console.Write("(d)eposit, (w)ithdraw or e(x)it: ")
        Console.ReadLine() // as return
    
    let private getAmount () =
        Console.Write("Enter the amount of the transaction: ")
        Console.ReadLine() |> Decimal.Parse
        
    let run () =
        let rec loop balance =
            printfn $"balance: %f{balance}"
            
            let action = promptUser()
            printfn $"You told me to do this: %s{action}"
            
            match action with
                | "d" -> loop (balance + getAmount()) 
                | "w" -> loop (balance - getAmount())
                | "x" -> ()
                | _ ->
                    printfn $"Invalid action: {action}"
                    loop balance
                    
        loop 0m
        ()