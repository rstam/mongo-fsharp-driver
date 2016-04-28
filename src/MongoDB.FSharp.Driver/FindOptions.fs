namespace MongoDB.FSharp.Driver

type FindOptions<'TDocument> = {
        Limit : int option
        Skip : int option
    }

    with
        static member Defaults = {
            Limit = None
            Skip = None
        }

        member internal this.ToCSharp() =
            let limit = this.Limit |> OptionToNullable
            let skip = this.Skip |> OptionToNullable
            new MongoDB.Driver.FindOptions<'TDocument>(Limit = limit, Skip = skip)
