namespace ClassesAndInterfaces

module Classes = 
    type Vector2D(dx : double, dy : double) =    
        /// This internal field stores the length of the vector, computed when the 
        /// object is constructed, so there's no constructor
        let length = sqrt (dx*dx + dy*dy)
    
        member this.DX = dx
    
        member this.DY = dy
    
        member this.Length = length
    
        /// This member is a method.  The previous members were properties.
        member this.Scale(k) = Vector2D(k * this.DX, k * this.DY)
        
    // class instantiation
    let vector1 = Vector2D(3.0, 4.0)
    let vector2 = vector1.Scale(10.0)
    
    printfn $"vector2 = {{dx:{vector2.DX}, dy:{vector2.DY}}}"

module Interfaces =
    /// This is a type that implements IDisposable.
    type ReadFile() =

        let file = new System.IO.StreamReader("readme.txt")

        member this.ReadLine() = file.ReadLine()

        // This is the implementation of IDisposable members.
        interface System.IDisposable with
            member this.Dispose() = file.Close()

    let reader = new ReadFile()
    printfn $"{reader.ReadLine()}"