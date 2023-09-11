module Transactions.Repository

open Transactions.Domain
open Transactions.Utils.Railway
open System.IO

open Transactions.Utils.Json.Serialization

let private getAccountfileName accountId = // 'a -> string
    $"account_{accountId}.json"

let private getAccountData accountId = // 'a -> string
    try
        getAccountfileName accountId |> File.ReadAllText |> Ok
    with e -> Error e.Message

let private buildAccount json: Result<Account, string> = // string -> Account
    try
        json |> deserialize |> Ok
    with e -> Error e.Message

let private save account = // Account -> unit
    try
        (getAccountfileName account.Id, serialize account) // string * string
        |> File.WriteAllText // unit
        Ok account
    with e -> Error e.Message

let get accountId =
    accountId |> getAccountData >>= buildAccount
    
let put account = // Account -> Account
    account |> save