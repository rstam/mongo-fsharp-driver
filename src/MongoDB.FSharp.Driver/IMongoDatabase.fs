namespace MongoDB.FSharp.Driver

type IMongoDatabase = interface
        // properties
        abstract member Name : string
        abstract member Settings : MongoDatabaseSettings option
        // methods
        abstract member DropCollection : name : string -> unit
        abstract member GetCollection<'TDocument> : name : string * ?settings : MongoCollectionSettings -> IMongoCollection<'TDocument>
    end