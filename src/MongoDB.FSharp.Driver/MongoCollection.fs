namespace MongoDB.FSharp.Driver

open System

type internal MongoCollection<'TDocument> =
    val _csharpCollection : MongoDB.Driver.IMongoCollection<'TDocument>
    val _settings : MongoCollectionSettings option

    new(csharpCollection : MongoDB.Driver.IMongoCollection<'TDocument>, settings : MongoCollectionSettings option) =
        { _csharpCollection = csharpCollection; _settings = settings }

    interface IMongoCollection<'TDocument> with
        // properties
        member this.DatabaseName = this._csharpCollection.Database.DatabaseNamespace.DatabaseName
        member this.Name = this._csharpCollection.CollectionNamespace.CollectionName
        member this.Settings = this._settings

        // methods
        member this.Count(filter : MongoDB.Bson.BsonDocument) =
            raise (new NotImplementedException())

        member this.Find(filter : MongoDB.Bson.BsonDocument, ?options : FindOptions<'TDocument>) =
            let filter = new MongoDB.Driver.BsonDocumentFilterDefinition<'TDocument>(filter)
            let options = options |> Option.map (fun x -> x.ToCSharp()) |> OptionToNull
            let csharpCursor = this._csharpCollection.FindSync(filter, options)
            new Cursor<'TDocument>(csharpCursor) :> ICursor<'TDocument>

        member this.InsertOne(document:'TDocument, ?options:InsertOneOptions) =
            let options = options |> Option.map (fun v -> v.ToCSharp()) |> OptionToNull
            this._csharpCollection.InsertOne(document, options)

        member this.InsertMany(documents : seq<'TDocument>, ?options : InsertManyOptions) =
            let options = options |> Option.map (fun x -> x.ToCSharp()) |> OptionToNull
            this._csharpCollection.InsertMany(documents, options)
    end
        