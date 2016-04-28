[<AutoOpen>]
module internal DnsEndPointExtensions

open System.Net

type DnsEndPoint with
    static member internal FromCSharp(csharp : MongoDB.Driver.MongoServerAddress) =
        new DnsEndPoint(csharp.Host, csharp.Port)

    member internal this.ToCSharp() =
        new MongoDB.Driver.MongoServerAddress(this.Host, this.Port)
