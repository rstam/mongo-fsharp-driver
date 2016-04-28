namespace MongoDB.FSharp.Driver

open MongoDB.FSharp.Driver

type ReadPreferenceMode =
    | Primary = 0
    | PrimaryPreferred = 1
    | Secondary = 2
    | SecondaryPreferred = 3
    | Nearest = 4

[<AutoOpen>]
module internal ReadPreferenceModeExtensions =
    type ReadPreferenceMode with
        static member internal FromCSharp(csharp : MongoDB.Driver.ReadPreferenceMode) =
            enum<ReadPreferenceMode>(int csharp)

        member internal this.ToCSharp() =
            enum<MongoDB.Driver.ReadPreferenceMode>(int this)
