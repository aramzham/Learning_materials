// this technique allows you to create types with modified behavior without the need to create a separate file or a type definition
let oe = { new System.Object() with member x.ToString() = "F#" }
System.Console.WriteLine(oe)


type IPrintable = abstract member Print: unit -> unit

// factory method
let makePrintable (x: int) (y: float) =
    {new IPrintable with member this.Print() = printfn "created %d %f" x y}

let x = makePrintable 1 2.0
x.Print() // in this case you don't need to cast the object to an interface