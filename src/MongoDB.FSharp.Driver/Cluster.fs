namespace MongoDB.FSharp.Driver

type Cluster =
    val _clusterId : ClusterId
    val _csharpCluster : MongoDB.Driver.Core.Clusters.ICluster

    private new(csharpCluster : MongoDB.Driver.Core.Clusters.ICluster) =
        let clusterId = ClusterId.FromCSharp(csharpCluster.ClusterId)
        { _clusterId = clusterId; _csharpCluster = csharpCluster }

    static member FromCSharp(csharp : MongoDB.Driver.Core.Clusters.ICluster) =
        Cluster(csharp)

    interface ICluster with
        member this.ClusterId = this._clusterId
    end
