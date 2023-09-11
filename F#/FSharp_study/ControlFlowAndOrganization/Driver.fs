module Transactions.Driver

open System
open System.IO
open Transactions.Domain
open Transactions.Repository
open Transactions.Rules.Accounts
open Transactions.Utils.Railway

module UserConsole =
    let private promptUser() =
        Console.Write("(d)eposit, (w)ithdraw or e(x)it: ")
        Console.ReadLine() // as return
    
    let private getAmount () =
        Console.Write("Enter the amount of the transaction: ")
        let input = Console.ReadLine()
        let (success, value) = input |> Decimal.TryParse
        match success with
        | true -> Ok value
        | false -> Error $"Something other than a number was entered: {input}"
        
    let run () =
        let rec loop account =
            printfn $"balance: %f{account.Balance}"
            
            let action = promptUser()
            printfn $"You told me to do this: %s{action}"
            
            match action with
            | "d" | "w" ->
                match getAmount() with
                | Ok value ->
                    match action with
                    | "d" -> loop (deposit value account) 
                    | "w" -> loop (withdraw value account)
                    | _ -> loop account
                | Error e -> 
                    printfn "%A" e
                    loop account
            | "x" -> ()
            | _ ->
                printfn $"Invalid action: {action}"
                loop account
                    
        loop Account.Default
        ()

module Utils =
    let deleteAccountRepoFiles () =
        Directory.GetFiles(".", "account_*.json") |> Array.iter File.Delete

module AccountRepoDriver =
    let run () =
        Utils.deleteAccountRepoFiles()

        Account.Default
        |> deposit 100m
        |> withdraw 25m
        |> put
        |> ignore

        get 0 |> printfn "%A"
        get 1 |> printfn "%A"

        get 0
        >>> deposit 40m
        >>= put
        |> ignore

        get 0 |> printfn "%A"

        get 1
        >>> deposit 40m
        >>= put
        |> printfn "%A"