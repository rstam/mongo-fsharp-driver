namespace MongoDB.FSharp.Driver

type IMongoCollection<'TDocument> =
    // properties
    abstract member DatabaseName : string
    abstract member Name : string
    abstract member Settings : MongoCollectionSettings option
    // methods
    abstract member Count : filter : MongoDB.Bson.BsonDocument -> int64
    abstract member Find : filter : MongoDB.Bson.BsonDocument * ?options : FindOptions<'TDocument> -> ICursor<'TDocument>
    abstract member InsertOne : document : 'TDocument * ?options : InsertOneOptions -> unit
    abstract member InsertMany : documents : seq<'TDocument> * ?options : InsertManyOptions -> unit
