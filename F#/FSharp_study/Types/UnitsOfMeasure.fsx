[<Measure>] type cm
[<Measure>] type inch
[<Measure>] type m
[<Measure>] type sec
[<Measure>] type kg
[<Measure>] type lbs

let x = 1<cm>
let m = 1<m>

printfn "%A" (x + 3<cm>)
printfn "%b" (x = 4<cm>)
// printfn "%A" (m = 1<cm>) // getting a compiler error that units of measure don't match


let distance = 1.0<m>
let time = 2.0<sec>
let speed = 2.0<m/sec>
let acceleration = 2.0<m/sec^2> // you can combine measures however you want
let force1 = 2.0<kg m/sec^2>

[<Measure>] type N = kg*m/sec^2 // for newtons
let force2 = 2.0<N>
printfn "%b" (force1 = force2)


[<Measure>] type degC
[<Measure>] type degF

let convertDegCToF c = c * 1.8<degF/degC> + 32.0<degF>
printfn "%f" (convertDegCToF 0.0<degC>)

// conversion factor
let cmPerM = 100.0<cm/m>
let distanceInCm = 2.0<m> * cmPerM
printfn "%f" distanceInCm