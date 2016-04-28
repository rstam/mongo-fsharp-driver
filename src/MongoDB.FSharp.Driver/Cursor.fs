namespace MongoDB.FSharp.Driver

open System
open System.Collections
open System.Collections.Generic

type internal Cursor<'TDocument> =
    val _csharpCursor : MongoDB.Driver.IAsyncCursor<'TDocument>

    new(csharpCursor : MongoDB.Driver.IAsyncCursor<'TDocument>) =
        { _csharpCursor = csharpCursor }

    member private this.GetEnumerator() =
        (seq { while this._csharpCursor.MoveNext() do yield! this._csharpCursor.Current }).GetEnumerator()

    interface ICursor<'TDocument> with
        member this.ToBatchSequence() =
            seq { while this._csharpCursor.MoveNext() do yield this._csharpCursor.Current }
    end

    interface IDisposable with
        member this.Dispose() =
            this._csharpCursor.Dispose()
    end

    interface IEnumerable with
        member this.GetEnumerator() =
            this.GetEnumerator() :> IEnumerator
    end

    interface IEnumerable<'TDocument> with
        member this.GetEnumerator() =
            this.GetEnumerator()
    end
