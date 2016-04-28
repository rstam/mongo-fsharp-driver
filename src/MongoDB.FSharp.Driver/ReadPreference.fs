namespace MongoDB.FSharp.Driver

type TagSet = seq<Tag >

type ReadPreference = {
        Mode : ReadPreferenceMode
        TagSets : option<seq<seq<Tag>>>
    }

    with
        static member Primary = { Mode = ReadPreferenceMode.Primary; TagSets = None }
        static member PrimaryPreferred = { Mode = ReadPreferenceMode.PrimaryPreferred; TagSets = None }
        static member Secondary = { Mode = ReadPreferenceMode.Secondary; TagSets = None }
        static member SecondaryPreferred = { Mode = ReadPreferenceMode.SecondaryPreferred; TagSets = None }
        static member Nearest = { Mode = ReadPreferenceMode.Nearest; TagSets = None }

        static member internal FromCSharp(csharp : MongoDB.Driver.ReadPreference) =
            let mode = ReadPreferenceMode.FromCSharp(csharp.ReadPreferenceMode)
            let tagSets =
                let mapTagSet (tagSet : seq<MongoDB.Driver.Tag>) = tagSet |> Seq.map (fun tag -> Tag.FromCSharp(tag))
                csharp.TagSets |> NullToOption |> Option.map (fun tagSets -> tagSets |> Seq.map (fun tagSet -> tagSet.Tags) |> Seq.map mapTagSet)
            { Mode = mode; TagSets = tagSets }

        member internal this.ToCSharp() =
            let mode = this.Mode.ToCSharp()
            let tagSets =
                let mapTagSet (tagSet : seq<Tag>) = new MongoDB.Driver.TagSet(tagSet |> Seq.map (fun tag -> tag.ToCSharp()))
                this.TagSets |> Option.map (fun tagSets -> tagSets |> Seq.map mapTagSet) |> OptionToNull
            new MongoDB.Driver.ReadPreference(mode, tagSets)
