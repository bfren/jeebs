namespace JeebsF

[<AutoOpen>]
module Core =

    let (|->) x f = printfn "%A" x ; f x
    let (|=>) a b f = printfn "%A" a ; printfn "%A" b ; f a b
