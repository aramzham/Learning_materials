namespace RecordsAndDiscriminatedUnionTypes

module Records = 
    type ContactCard = {
        Name: string
        Phone: string
        Verified: bool
    }

    let contact1 = {
        Name = "Alf"
        Phone = "2337482374"
        Verified = false
    }

    let contactOnSameLine = { Name = "Alf"; Phone = "(206) 555-0157"; Verified = false }

    // this means contact2 equals contact1 with phone and verified changed
    let contact2 = {
        contact1 with
            Phone = "(206) 555-0112"
            Verified = true
    }

    type ContactCardAlternate =
        { Name     : string
          Phone    : string
          Address  : string
          Verified : bool }

        member this.PrintedContactCard = this.Name + " Phone: " + this.Phone + (if not this.Verified then "(unverified) " else "") + this.Address
    
    let contactAlternate = { 
        Name = "Alf" 
        Phone = "(206) 555-0157" 
        Verified = false 
        Address = "111 Alf Street" 
    }

    printfn $"Alf's alternate contact card is {contactAlternate.PrintedContactCard}"


module DiscriminatedUnions =
    type Suit = 
        | Hearts 
        | Clubs 
        | Diamonds 
        | Spades

    type Rank = 
        | Value of int
        | Ace
        | King
        | Queen
        | Jack

        static member GetAllRanks() = 
            [ yield Ace
              for i in 2 .. 10 do yield Value i
              yield Jack
              yield Queen
              yield King ]

    // Single-case DUs are often used for domain modeling.  This can buy you extra type safety
    // over primitive types such as strings and ints.
    type Address = Address of string
    let address = Address "Yerevan, World"
    let unwrapAddress (Address a) = a

    printfn $"unwrapped address = {address |> unwrapAddress}"