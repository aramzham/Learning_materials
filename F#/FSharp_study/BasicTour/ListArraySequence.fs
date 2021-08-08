namespace ListArraySequence

open System

// Lists are ordered, immutable collections of elements of the same type. They are singly linked lists, which means they are meant for enumeration, but a poor choice for random access and concatenation if they're large.
module Lists = 
    let list1 = [1;2;3] // for same line
    let list2 = [
        1
        2
        3
    ] // for multiline you don't need ;

    // This is a list of integers from 1 to 1000
    let numberList = [ 1 .. 1000 ] 

    let daysList = 
        [ for month in 1 .. 12 do
              for day in 1 .. DateTime.DaysInMonth(2021, month) do 
                  yield DateTime(2021, month, day) ]

    let dateToString (x : DateTime) = x.ToString "dd/MM/yyyy"
    printfn $"The first 10 days of 2021 are: {daysList |> List.map dateToString |> List.take 10}"


// Arrays are fixed-size, mutable collections of elements of the same type. They support fast random access of elements, and are faster than F# lists because they are just contiguous blocks of memory.
module Arrays = 
    let array1 = [||] // empty array
    let array2 = [|"aram"; "atom"; "sirun"; "mna"; "hush"; "misht"; "gorgeouz"; "beats"|]
    let array3 = [|1..1000|] // from 1 to 1000

    let evenNumbers = Array.init 1001 (fun n-> n*2) // last number will be 1000 * 2
    printfn "%d" evenNumbers.[evenNumbers.Length - 1]
    printfn "%A" evenNumbers

    array2.[1] <- "WORLD!"

    let sumOfLengthsOfWords = 
        array2
        |> Array.filter (fun x -> x.StartsWith "a")
        |> Array.sumBy (fun x -> x.Length)

    printfn $"The sum of the lengths of the words in Array 2 is: %d{sumOfLengthsOfWords}"


// Sequences are a logical series of elements, all of the same type. These are a more general type than Lists and Arrays, capable of being your "view" into any logical series of elements. They also stand out because they can be lazy, which means that elements can be computed only when they are needed.
module Sequences = 
    let seq1 = Seq.empty
    let seq2 = seq { yield "hello"; yield "world"; yield "and"; yield "hello"; yield "world"; yield "again" }
    // on demand 1 to 1000
    let seq3 = seq {1..1000}
    
    let rnd = System.Random()
        
    // This is an infinite sequence which is a random walk.
    // This example uses yield! to return each element of a subsequence.
    let rec randomWalk x =
        seq { yield x
              yield! randomWalk (x + rnd.NextDouble() - 0.5) }
    
    let first100ValuesOfRandomWalk = 
        randomWalk 5.0 
        |> Seq.truncate 100
        |> Seq.toList
    
    printfn $"First 100 elements of a random walk: {first100ValuesOfRandomWalk}"