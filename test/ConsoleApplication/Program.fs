open ConsoleApplication
open MongoDB.FSharp.Driver

[<EntryPoint>]
let main argv = 
    let clientSettings = { MongoClientSettings.Defaults with ConnectionMode = ConnectionMode.Standalone }
    let client = MongoClient.Create(clientSettings)
    let databaseSettings = { MongoDatabaseSettings.Defaults with ReadPreference = Some ReadPreference.SecondaryPreferred }
    let database = client.GetDatabase("test", databaseSettings)
    let collectionSettings = { MongoCollectionSettings.Defaults with GuidRepresentation = Some GuidRepresentation.Standard }
    let collection = database.GetCollection<Person>("test")
    database.DropCollection("test")

    let person1 = new Person(1, "John Doe")
    let person2 = new Person(2, "Jane Doe")
    let person3 = new Person(3, "Tom Smith")

    collection.InsertOne(person1)
    collection.InsertMany([person2; person3], { InsertManyOptions.Defaults with IsOrdered = false })

    let filter = new MongoDB.Bson.BsonDocument()
    use cursor = collection.Find(filter)
    for document in cursor do
        printfn "Id = %i" document.Id

    let filter = new MongoDB.Bson.BsonDocument()
    use cursor = collection.Find(filter)
    for batch in cursor.ToBatchSequence() do
        for document in batch do
            printfn "Id = %i" document.Id

    0 // return an integer exit code
