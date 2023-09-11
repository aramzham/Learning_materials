type Request = {
    Id: int;
    Name: string;
    Email: string;
}

type RequestPipelineError = 
    | ValidationError of field:string
    | DatabaseError of message:string

let validateEmail request =
    match request.Email with
    | "" -> Error (ValidationError "Email")
    | _ -> Ok request

let validateName request =
    match request.Name with
    | "" -> Error (ValidationError "Name")
    | _ -> Ok request

let validateRequest request =
    (Ok request)
        |> Result.bind validateName
        |> Result.bind validateEmail

//let validateRequest request =
//    (Ok request)
//        >>= validateName
//        >>= validateName

let storeInDatabase request =
    try
        // store somewhere...
        Ok request
    with
        | _ -> Error (DatabaseError "Database issue")

let returnStatus result =
    match result with
    | Ok data -> $"return 200 / Ok and data: {data}"
    | Error e -> 
        match e with
        | ValidationError field -> $"Validation error: {field}"
        | DatabaseError message -> $"Database error: {message}"


let requestProcessor request =
    (Ok request)
        |> Result.bind validateName
        |> Result.bind validateEmail
        |> returnStatus


let validRequest = {Id=1; Name="John"; Email="john@iaea.org"}
let invalidRequest1 = {Id=2; Name=""; Email=""}
let invalidRequest2 = {Id=3; Name="Predrag"; Email=""}


printfn "%A" (requestProcessor validRequest)
printfn "%A" (requestProcessor invalidRequest1)
printfn "%A" (requestProcessor invalidRequest2)