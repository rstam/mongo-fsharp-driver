namespace MongoDB.FSharp.Driver

open MongoDB.FSharp.Driver

type GuidRepresentation =
    | Unspecified = 0
    | Standard = 1
    | CSharpLegacy = 2
    | JavaLegacy = 3
    | PythonLegacy = 4

[<AutoOpen>]
module internal GuidRepresentationExtensions =
    type GuidRepresentation with
        static member internal FromCSharp(csharp: MongoDB.Bson.GuidRepresentation) =
            enum<GuidRepresentation>(int csharp)

        member internal this.ToCSharp() =
            enum<MongoDB.Bson.GuidRepresentation>(int this)
