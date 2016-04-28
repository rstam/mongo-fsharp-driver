namespace MongoDB.FSharp.Driver

open MongoDB.FSharp.Driver

type ConnectionMode = 
    | Automatic = 0
    | Direct = 1
    | ReplicaSet = 2
    | ShardRouter = 3
    | Standalone = 4

[<AutoOpen>]
module internal ConnectionModeExtensions =
    type ConnectionMode with
        static member internal FromCSharp(csharp : MongoDB.Driver.ConnectionMode) =
            enum<ConnectionMode>(int csharp)

        member internal this.ToCSharp() =
            enum<MongoDB.Driver.ConnectionMode>(int this)
