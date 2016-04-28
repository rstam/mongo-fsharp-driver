namespace ConsoleApplication

open MongoDB.Bson.Serialization.Attributes

type Person =
    val _id : int
    val _name : string

    [<BsonConstructor>]
    new(id : int, name : string) =
        { _id = id; _name = name }

    [<BsonElement>]
    member this.Id = this._id

    [<BsonElement>]
    member this.Name = this._name
