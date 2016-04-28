namespace MongoDB.FSharp.Driver

type ClusterType =
    | Unknown = 0
    | Standalone = 1
    | ReplicaSet = 2
    | Sharded = 3
