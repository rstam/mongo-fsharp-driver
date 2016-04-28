namespace MongoDB.FSharp.Driver

type InsertOneOptions = {
        BypassDocumentValidation : bool option
    }

    with
        static member Defaults = { BypassDocumentValidation = None }

        member internal this.ToCSharp() =
            let bypassDocumentValidation = this.BypassDocumentValidation |> OptionToNullable
            new MongoDB.Driver.InsertOneOptions(BypassDocumentValidation = bypassDocumentValidation)
