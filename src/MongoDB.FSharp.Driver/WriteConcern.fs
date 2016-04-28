namespace MongoDB.FSharp.Driver

open System

type WriteConcern = {
        FSync : bool option
        Journal : bool option
        W : WValue option
        WTimeout : TimeSpan option
    }

    with
        static member Acknowledged = { FSync = None; Journal = None; W = None; WTimeout = None }
        static member Unacknowledged = { FSync = None; Journal = None; W = Some (Count 0); WTimeout = None }
        static member W1 = { FSync = None; Journal = None; W = Some (Count 1); WTimeout = None }
        static member W2 = { FSync = None; Journal = None; W = Some (Count 2); WTimeout = None }
        static member W3 = { FSync = None; Journal = None; W = Some (Count 3); WTimeout = None }
        static member WMajority = { FSync = None; Journal = None; W = Some (Mode "majority"); WTimeout = None }

        static member internal FromCSharp(csharp: MongoDB.Driver.WriteConcern) =
            let fsync = csharp.FSync |> NullableToOption
            let journal = csharp.Journal |> NullableToOption
            let w = csharp.W |> NullToOption |> Option.map (fun w -> WValue.FromCSharp(w))
            let wTimeout = csharp.WTimeout |> NullableToOption
            { FSync = fsync; Journal = journal; W = w; WTimeout = wTimeout }

        member internal this.ToCSharp() =
            let fsync = this.FSync |> OptionToNullable |> ToOptional
            let journal = this.Journal |> OptionToNullable |> ToOptional
            let w = this.W |> Option.map (fun w -> w.ToCSharp()) |> OptionToNull |> ToOptional
            let wTimeout = this.WTimeout |> OptionToNullable |> ToOptional
            new MongoDB.Driver.WriteConcern(w, wTimeout, fsync, journal)
