namespace MongoDB.FSharp.Driver

type Tag = {
        Name : string
        Value : string
    }

    with
        static member internal FromCSharp(csharp : MongoDB.Driver.Tag) =
            { Name = csharp.Name; Value = csharp.Value }

        member internal this.ToCSharp() =
            new MongoDB.Driver.Tag(this.Name, this.Value)
