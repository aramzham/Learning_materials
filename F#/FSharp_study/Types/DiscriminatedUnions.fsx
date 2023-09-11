//type DaysOfWeek = Sunday | Monday | Tuesday | Wednesday | Thursday | Friday | Saturday
type DaysOfWeek = 
    | Sunday 
    | Monday 
    | Tuesday 
    | Wednesday 
    | Thursday 
    | Friday 
    | Saturday


let sunday = Sunday
printfn "%b" (sunday = Sunday)
printfn "%b" (sunday = Saturday)


type Shape = 
    | Rectangle of width: float * height: float
    | Circle of radius: float
    | Triangle of s1: float * s2: float * s3: float

let rect = Rectangle(width=1, height=2)
printfn "%A" rect

let circle = Circle(2.)
printfn "%A" circle

let tri = Triangle(1.2, 4.33, 43.1)
printfn "%A" tri

let perimeter shape = 
    match shape with
    | Rectangle (width, height) -> 2. * (width + height)
    | Circle radius -> 2. * System.Math.PI * radius
    | Triangle (s1, s2, s3) -> s1 + s2 + s3

let perimeters = perimeter rect, perimeter circle, perimeter tri
printfn "%A" perimeters

let isSquare shape =
    match shape with
    | Rectangle (width, height) -> width = height
    | _ -> false

printfn "%b" (isSquare rect)
printfn "%b" (isSquare circle)
printfn "%b" (isSquare (Rectangle(2.4, 2.4)))


type BinaryTree =
    | Leaf of int
    | Node of BinaryTree * BinaryTree

let root = Node(Leaf(1), Node(Leaf(2), Leaf(3)))
let sum tree = 
    let rec walk accum current =
        match current with
        | Leaf value -> accum + value
        | Node (left, right) -> (walk accum left) + (walk accum right)
    walk 0 tree

printfn "%i" (sum root)