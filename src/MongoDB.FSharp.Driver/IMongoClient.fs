namespace MongoDB.FSharp.Driver

type IMongoClient = 
    // properties
    abstract member Cluster : ICluster
    abstract member Settings : MongoClientSettings
    // methods
    abstract member DropDatabase : name : string -> unit
    abstract member GetDatabase : name : string * ?settings : MongoDatabaseSettings -> IMongoDatabase
    abstract member ListDatabases : unit -> ICursor<MongoDB.Bson.BsonDocument>
