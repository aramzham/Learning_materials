module Mathematics
let add x y = x + y
let subtract x y = x - y
let addResult = add 1 2

module FloatingPoint =
    let add x y :float = x + y
    let subtract x y :float = x - y
    
module AnotherModule = // Mathematics.AnotherModule
    let value1 = add 1 3
    let value2 = FloatingPoint.subtract 2. 4.