namespace MongoDB.FSharp.Driver

open System
open System.Collections.Generic

type ICursor<'TDocument> = 
    inherit seq<'TDocument>
    inherit IDisposable
    abstract member ToBatchSequence : unit -> seq<seq<'TDocument>>
    