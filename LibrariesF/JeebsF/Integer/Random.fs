namespace JeebsF.Internal.Integer

[<AutoOpen>]
module internal Random =

    open System
    open System.Security.Cryptography



    let get max =
        let mutable b = Array.zeroCreate<byte> 4

        use csp = new RNGCryptoServiceProvider()
        csp.GetBytes b

        let rnd = BitConverter.ToUInt32(b, 0) |> float
        let upp = UInt32.MaxValue |> float

        rnd / upp * (max |> float) |> Math.Round |> int



    let getForZeroBased max =
        match get max with
        | rnd when rnd = max -> rnd - 1
        | rnd -> rnd