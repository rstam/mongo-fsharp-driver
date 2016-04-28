namespace MongoDB.FSharp.Driver

type ClusterId = {
        Value : int
    }

    with
        static member FromCSharp(csharp : MongoDB.Driver.Core.Clusters.ClusterId) =
            { ClusterId.Value = csharp.Value }