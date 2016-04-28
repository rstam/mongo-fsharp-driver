namespace MongoDB.FSharp.Driver

open System

type WValue =
    | Count of int
    | Mode of string

    with
        static member internal FromCSharp(w : MongoDB.Driver.WriteConcern.WValue) =
            match w with
                | :? MongoDB.Driver.WriteConcern.WCount as wcount -> Count wcount.Value
                | :? MongoDB.Driver.WriteConcern.WMode as wmode -> Mode wmode.Value
                | _ -> raise (new InvalidCastException())

        member internal this.ToCSharp() =
            match this with
                | Count count -> new MongoDB.Driver.WriteConcern.WCount(count) :> MongoDB.Driver.WriteConcern.WValue
                | Mode mode -> new MongoDB.Driver.WriteConcern.WMode(mode) :> MongoDB.Driver.WriteConcern.WValue