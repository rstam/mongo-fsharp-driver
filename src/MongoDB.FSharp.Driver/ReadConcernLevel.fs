namespace MongoDB.FSharp.Driver

open System
open MongoDB.FSharp.Driver

type ReadConcernLevel = 
    | Local = 0
    | Majority = 1

[<AutoOpen>]
module internal ReadConcernLevelExtensions =
    type ReadConcernLevel with
        static member internal FromCSharp(csharp : MongoDB.Driver.ReadConcernLevel) =
            enum<ReadConcernLevel>(int csharp)

        member internal this.ToCSharp() =
            enum<MongoDB.Driver.ReadConcernLevel>(int this)
