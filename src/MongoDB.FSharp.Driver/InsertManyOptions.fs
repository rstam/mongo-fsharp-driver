namespace MongoDB.FSharp.Driver

type InsertManyOptions = {
        BypassDocumentValidation : bool option
        IsOrdered : bool
    }

    with
        static member Defaults = {
            BypassDocumentValidation = None;
            IsOrdered = true
        }

        member internal this.ToCSharp() =
            let bypassDocumentValidation = this.BypassDocumentValidation |> OptionToNullable
            new MongoDB.Driver.InsertManyOptions(BypassDocumentValidation = bypassDocumentValidation, IsOrdered = this.IsOrdered)
