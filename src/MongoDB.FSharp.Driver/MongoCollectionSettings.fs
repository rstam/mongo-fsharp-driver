namespace MongoDB.FSharp.Driver

type MongoCollectionSettings = {
        AssignIdOnInsert : bool option
        GuidRepresentation : GuidRepresentation option
        ReadConcern : ReadConcern option
        ReadPreference : ReadPreference option
        WriteConcern : WriteConcern option
    }

    with
        static member Defaults = {
                AssignIdOnInsert = None
                GuidRepresentation = None
                ReadConcern = None
                ReadPreference = None
                WriteConcern = None
            }

        member internal this.ToCSharp() =
             let csharp = new MongoDB.Driver.MongoCollectionSettings()
             match this.AssignIdOnInsert with Some assignIdOnInsert -> csharp.AssignIdOnInsert <- assignIdOnInsert | None -> ()
             match this.GuidRepresentation with Some guidRepresentation -> csharp.GuidRepresentation <- guidRepresentation.ToCSharp() | None -> ()
             match this.ReadConcern with Some readConcern -> csharp.ReadConcern <- readConcern.ToCSharp() | None -> ()
             match this.ReadPreference with Some readPreference -> csharp.ReadPreference <- readPreference.ToCSharp() | None -> ()
             match this.WriteConcern with Some writeConcern -> csharp.WriteConcern <- writeConcern.ToCSharp() | None -> ()
             csharp.Freeze()
