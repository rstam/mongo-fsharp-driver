namespace MongoDB.FSharp.Driver

type internal MongoDatabase =
    val _csharpDatabase : MongoDB.Driver.IMongoDatabase
    val _settings : MongoDatabaseSettings option

    new(csharpDatabase : MongoDB.Driver.IMongoDatabase, settings : MongoDatabaseSettings option) =
        { _csharpDatabase = csharpDatabase; _settings = settings }

    interface IMongoDatabase with
        // properties
        member this.Name = this._csharpDatabase.DatabaseNamespace.DatabaseName
        member this.Settings = this._settings

        // methods
        member this.DropCollection(name : string) =
            this._csharpDatabase.DropCollection(name)

        member this.GetCollection<'TDocument>(name :string, ?settings : MongoCollectionSettings) =
            let csharpSettings = settings |> Option.map (fun s -> s.ToCSharp()) |> OptionToNull
            let csharpCollection = this._csharpDatabase.GetCollection<'TDocument>(name, csharpSettings)
            new MongoCollection<'TDocument>(csharpCollection, settings) :> IMongoCollection<'TDocument>
    end


