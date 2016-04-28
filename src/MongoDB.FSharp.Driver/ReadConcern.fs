namespace MongoDB.FSharp.Driver

type ReadConcern = {
        Level : ReadConcernLevel option
    }

    with
        static member Default = { Level = None }
        static member Local = { Level = Some ReadConcernLevel.Local }
        static member Majority = { Level = Some ReadConcernLevel.Majority }

        static member internal FromCSharp(csharp : MongoDB.Driver.ReadConcern) =
            let level = csharp.Level |> NullableToOption |> Option.map (fun value -> ReadConcernLevel.FromCSharp(value))
            { Level = level }

        member internal this.ToCSharp() =
            let level = this.Level |> Option.map (fun value -> value.ToCSharp()) |> OptionToNullable |> ToOptional
            new MongoDB.Driver.ReadConcern(level)
