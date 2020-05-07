namespace JeebsF.Internal.Array

[<AutoOpen>]
module internal Shuffle =



    let private getRnd = JeebsF.Internal.Integer.Random.getForZeroBased



    let shuffle (input : array<'T>) =
        let swap (arr : array<'T>) =
            let idxOne = getRnd arr.Length
            let idxTwo = getRnd arr.Length

            let valOne = arr.[idxOne]
            let valTwo = arr.[idxTwo]

            arr.[idxOne] <- valTwo
            arr.[idxTwo] <- valOne

            arr

        [| 1 .. input.Length |]
        |> Array.fold (fun a _ -> swap a) input
