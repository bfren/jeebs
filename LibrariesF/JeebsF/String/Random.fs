namespace JeebsF.Internal.String

[<AutoOpen>]
module internal Random =

    open System
    open JeebsF.Internal.Array.Shuffle
    open JeebsF.Internal.Integer.Random



    let private numberChars = [ for i in 48 .. 57 -> Convert.ToChar i ]
    let private uppercaseChars = [ for i in 65 .. 90 -> Convert.ToChar i ]
    let private lowercaseChars = [ for i in 97 .. 122 -> Convert.ToChar i ]
    let private specialChars = [ '!'; '#'; '@'; '+'; '-'; '*'; '^'; '='; ':'; ';'; '£'; '$'; '~'; '`'; '¬' ]



    let addChars add toAdd original =
        match add with
        | false -> original
        | true -> original @ toAdd



    let private getRnd = getForZeroBased



    let private getChar (chars : char list) =
        chars.[getRnd chars.Length]




    let get lower upper numbers special length =
        let build add toAdd (chars, starter) =
            addChars add chars toAdd, getChar toAdd :: starter

        let chars, starter =
            ([], [])
            |> build lower lowercaseChars
            |> build upper uppercaseChars
            |> build numbers numberChars
            |> build special specialChars

        if chars.Length = 0 then "You must choose at least one character group." |> failwith

        let random = match length - starter.Length with
                     | diff when diff = 0 -> starter
                     | diff when diff > 0 -> starter @ ([ 1 .. diff ] |> List.map (fun _ -> getChar chars))
                     | _ -> sprintf "You requested a string of length %i which is shorter than the required number of character groups (%i)." length starter.Length |> failwith

        random
        |> Array.ofList
        |> shuffle
        |> String



    let getLower = get true false false false
    let getAll = get true true true true
