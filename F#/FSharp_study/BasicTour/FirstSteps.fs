namespace FirstSteps

module BasicFunctions = 
    let param = 200
    
    let sampleFunction1 x = x*x + 3

    let result = sampleFunction1 param

    printfn $"result of {param}*{param}+3 equals {result}"

    let sampleFunction2 (x:int) = 2 * x + 10

    let result2 = sampleFunction2(2 + 8)

    let sampleFunction3 x = 
        if x > 0 then
            x * x
        else 
            x + 100

    let result3 = sampleFunction3(20)
    let result3_1 = sampleFunction3(-6)

    printfn $"sampleFunction(20) = {result3}"
    printfn $"sampleFunction(-6) = {result3_1}"

module Immutability =
    let number = 2
    //let number = 3
    let mutable other = 23
    other <- 100
    printfn $"other number is {other}"

module UnitsOfMeasure = 
    open Microsoft.FSharp.Data.UnitSystems.SI.UnitNames

    let value0 = 20<kilogram>

    /// Next, define a new unit type
    [<Measure>]
    type mile =
        static member asMeter = 1609.34<meter/mile>

    let value1 = 180000.0<mile>
    printfn $"{value1}mile = {mile.asMeter * value1 / 1000.0}km"

module TypeInference = 
    let doSomething f x = 
        let y = f(x+1)
        "hello " + y

    let func x = 
        $"{x*10}"
    printfn $"{doSomething func 3}"
    // if you add an int to something => it must be an int => x is an int
    // f is a function that accepts an int parameter
    // if you add something to string => it must be a string => doSomething's return type is string