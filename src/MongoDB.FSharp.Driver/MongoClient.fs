namespace MongoDB.FSharp.Driver

open System

type MongoClient =
    val _cluster : ICluster
    val _csharpClient : MongoDB.Driver.IMongoClient
    val _settings : MongoClientSettings

    private new(settings : MongoClientSettings, csharpSettings : MongoDB.Driver.MongoClientSettings) =
        let csharpClient = new MongoDB.Driver.MongoClient(csharpSettings)
        let cluster = Cluster.FromCSharp(csharpClient.Cluster)
        { _cluster = cluster; _csharpClient = csharpClient; _settings = settings }

    static member Create(connectionString : string) =
        let url = new MongoDB.Driver.MongoUrl(connectionString)       
        let csharpSettings = MongoDB.Driver.MongoClientSettings.FromUrl(url)
        let settings = MongoClientSettings.FromCSharp(csharpSettings)
        new MongoClient(settings, csharpSettings) :> IMongoClient

    static member Create(settings : MongoClientSettings) =
        let csharpSettings = settings.ToCSharp()
        new MongoClient(settings, csharpSettings) :> IMongoClient

    interface IMongoClient with
        // properties
        member this.Cluster = this._cluster
        member this.Settings = this._settings

        // methods
        member this.DropDatabase(name : string) : unit =
            this._csharpClient.DropDatabase(name);

        member this.GetDatabase(name : string, ?settings : MongoDatabaseSettings) : IMongoDatabase =
            let csharpSettings = settings |> Option.map (fun s -> s.ToCSharp()) |> OptionToNull
            let csharpDatabase = this._csharpClient.GetDatabase(name, csharpSettings)
            new MongoDatabase(csharpDatabase, settings) :> IMongoDatabase

        member this.ListDatabases() : ICursor<MongoDB.Bson.BsonDocument> =
            raise (NotImplementedException())
    end
