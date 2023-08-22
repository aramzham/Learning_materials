// map
let map mapper items =
    [for value in items -> mapper value]
    
let result = map (fun x -> x*x) [1..5]
printfn "%A" result

// projection
type Person = {Name: string; Age:int}
let people = [{Name="John"; Age=60}; {Name="Predrag"; Age=40}]
let names = List.map (fun x -> x.Name) people
printfn "%A" names

// filter
let filter f list =
    let rec loop f accumulator list =
        match list with
        | [] -> accumulator
        | head::tail ->
            if f head then loop f (head::accumulator) tail
                      else loop f accumulator tail
    loop f [] list
    
let filtered = filter (fun x -> x.Age > 40) people
printfn "%A" filtered

let filteredByList = List.filter (fun x -> x.Age > 40) people
printfn "%A" filteredByList

// aggregation
let aggregate f accumulator items =
    let rec loop f accumulator items =
        match items with
        | [] -> accumulator
        | head::tail -> loop f (f accumulator head) tail
    loop f accumulator items
    
let customAggregated = aggregate (+) 0 (List.map (fun x-> x.Age) people)
printfn "%A" customAggregated

let aggregatedByFold = List.fold (+) 0 (List.map (fun x-> x.Age) people)
printfn "%A" aggregatedByFold

let aggregatedBySum = List.sum (List.map (fun x->x.Age) people)
printfn "%A" aggregatedBySum