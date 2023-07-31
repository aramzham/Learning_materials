open System

let aTuple = (1, 2., "hello")
let (integer, floatValue, stringValue) = aTuple // deconstruction

printfn
    $"tuple = %A{aTuple}{Environment.NewLine}first value = %d{integer}{Environment.NewLine}second value = %f{floatValue}{Environment.NewLine}third value = %s{stringValue}"


type Person =
    { FirstName: string
      LastName: string
      Age: int }

let p1 =
    { FirstName = "John"
      LastName = "Murray"
      Age = 54 }
// let p2 = {FirstName = "Aram" } // compiler error
// let p3 = {FirstName = "Predrag";LastName = "Maksimovic";Age = 40; MoreProperties="hello" }

printfn $"%A{p1}"