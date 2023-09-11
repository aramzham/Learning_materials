In F# functions are treated as first class citizens.

Everything in F# is an expression.
Every piece of code returns a value.


Only one function in a project can be annotated with [<EntryPoint>], and it must be at the end of its file.


You can use .Net's Console class for input/output.

printfn adds a carriage return symbol at the end of the line, while printf doesn't.


|Format specifier| Meaning                 |
|----------------|-------------------------|
|%A				 | any value               |
|%s			     | String                  |
|%c			     | Char                    |
|%d, %i			 | decimal integer         |
|%f, %F			 | floating point          |
|%b			     | boolean => true or false|
|%%			     | just a '%'              |


Variable is a user created label which is used to access a value somewhere in the application.


With pipe forward operator the value to the left of the symbol |> is made the rightmost part of the expression on the right.
10 |> add 1  is the same as add 1 10
20 |> (10 |> add)  is the same as 20 |> (add 10) then add 10 20


char is a 2 byte Unicode character.


List - ordered, immutable, series of elements of the same type. (implemented as single linked list)
Array - fixed-sized, zero-based, mutable collections of consecutive elements that are all of the same type. (contiguous block of memory)


Record is a simple aggregate of named values.


Strings are immutable in F# and are just alias for .net's System.String


print/string formatting variations
| Function | Use                                  |
| ---------|--------------------------------------|
| printf   | output goes to stdout                |
| sprintf  | returns a formatted string           |
| eprintf  | output to stderr                     |
| fprintf  | output to a text writer              |
| bprintf  | output to a StringBuilder            |
| kprintf  | calls a function to generate a result|


Unit is a type that indicates absence of a specific value.


F# is not a dynamically typed language but type inference makes you feel so.


In F# everything evaluates to value => everything is expression.


Rules for bindings (aka variables)
1. no types - compiler infers them
2. no new keyword for F# types
3. no semicolons
4. no parantheses for function parameters
