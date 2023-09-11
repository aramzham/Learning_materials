type ISprintable =
    abstract member Print : format:string -> unit

type IRepository<'T> = {
    Get: int -> Result<'T, string>
    Put: 'T -> Result<'T, string>
}

type IPrintable =
    abstract member Print : unit -> unit

type MyClass1(x:int, y:float) =
    interface IPrintable with
        member this.Print(): unit = printfn "%d %f" x y


let x1 = new MyClass1(1, 2.0)
// x1.Print() // compiler error

(x1 :> IPrintable).Print()


// F# interface declaration style
type INumericFSharp = 
    abstract Add: x:int -> y: int -> int // currying
// use it like this
// instance1.Add 1 2

// .net style
type INumericDotNet = 
    abstract Add: x: int * y: int -> int // tuple as the argument
// instance2.Add(1,2) // looks like we are calling a method in .net