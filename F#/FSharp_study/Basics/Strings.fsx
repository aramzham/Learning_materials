let aString = "Hello World!"

printfn $"%b{aString[1] = 'e'}"

// aString[1] <- 'a' // can't do that

let substring = aString[1..6]
printfn $"substring from 1 to 6 = %s{substring}"

let str1 = "abc
def"
let str2 = "ghi\
jkl"
printfn $"this string is without backslash: %s{str1}"
printfn $"this string is WITH backslash: %s{str2}"

// adding double quotes aka " into string
let str3 = @"<element id=""button alert""/>"
printfn $"verbatim string: %s{str3}"

let str4 = """<element id="button alert"/>"""
printfn $"triple double quotes: %s{str4}"

let a = 1
let b = 2
printfn $"use $ and handlebars: a = {a}, b = {b}"

let s = sprintf "String: \"%s\", int: %d, float: %f" "foo" 3 5.3
printfn $"%A{s}"