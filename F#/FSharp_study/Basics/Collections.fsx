open System.Collections.Generic

let dict = new Dictionary<string, int>()

dict["a"] <- 1
dict["a"] <- 2
dict["b"] <- 3

let comparison = dict["a"] = 1
printfn $"a value = 1 ? %b{comparison}"

printfn $"%A{dict}"