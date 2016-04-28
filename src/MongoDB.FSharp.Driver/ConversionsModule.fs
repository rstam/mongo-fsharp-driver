[<AutoOpen>]
module internal ConversionsModule

open System

let NullableToOption (value : Nullable<_>) =
    if value.HasValue then Some value.Value else None

let NullToOption<'T when 'T : null> (value : 'T) =
    if Object.ReferenceEquals(value, null) then None else Some value

let OptionToNull<'T when 'T : null> (value : 'T option) =
    match value with Some value -> value | None -> null

let OptionToNullable<'T when 'T : struct and 'T :> ValueType and 'T : (new : unit -> 'T)> (value : 'T option) =
    match value with Some value -> new Nullable<'T>(value) | None -> new Nullable<'T>()

let ToOptional<'T> (value : 'T) =
    new MongoDB.Driver.Optional<'T>(value)
