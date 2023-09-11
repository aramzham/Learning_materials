type Line = class
    val X1 : float
    val X2 : float
    val Y1 : float
    val Y2 : float

    new (x1, x2, y1, y2) as this = {X1=x1; X2=x2; Y1=y1; Y2=y2} then
        printfn " Creating Line: {(%g, %g), (%g, %g)}\nLength: %g" this.X1 this.Y1 this.X2 this.Y2 this.Length

    member this.Length =
        let sqr i = i * i
        sqrt(sqr(this.X1 - this.X2) + sqr(this.Y1 - this.Y2))
end

let aLine = new Line(1.0, 1.0, 4.0, 5.0)
printfn "%f" aLine.Length


// class definition with implicit constructor
type vec2(x:float, y: float) =
    member this.X = x
    member this.Y = y
    member this.Length = sqrt(this.X * this.X + this.Y * this.Y)

let v2 = vec2(3.0, 4.0)
printfn "%f" v2.Length

type vec3(x:float, y: float) =
    member val X = x with get, set // defining X prop for x field with a getter and setter
    member val Y = y with get, set
    member this.Length = sqrt(this.X * this.X + this.Y * this.Y)

let v3 = vec3(3.0, 4.0)
printfn "%f" v3.Length

v3.X <- 5. // using the setter
v3.Y <- 12.
printfn "%f" v3.Length