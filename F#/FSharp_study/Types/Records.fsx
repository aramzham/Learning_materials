type Person = {
    FirstName: string;
    LastName: string;
    Age: int
}

let p1 = {FirstName = "John"; LastName = "Murray"; Age = 50}
printfn "%A" p1
printfn "John is %d years old" p1.Age

let p2 = {p1 with Age = 32; LastName = "Stones"}
printfn "%A" p2


type ExtendedPerson = {FirstName:string; LastName: string; Age:int}
    with member me.FullName = $"{me.FirstName} {me.LastName}"
         member this.IsSenior = this.Age >= 65
         member self.IsOlderThan age = self.Age > age
         static member New f l a = {FirstName=f; LastName=l; Age=a}

let ep = ExtendedPerson.New "Titus" "Brumble" 44
printfn "%s" ep.FullName
printfn "%b" ep.IsSenior
printfn "%b" (ep.IsOlderThan 30)
printfn "%A" ep // members added by "with member" will not be reflected in printfn
