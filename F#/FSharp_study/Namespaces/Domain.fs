namespace Finance

type Transaction = {Id:int; Amount:float}

module Operations =
    let createTransaction id amount = {Id = id; Amount = amount}