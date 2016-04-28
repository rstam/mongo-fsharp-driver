namespace MongoDB.FSharp.Driver

open System
open System.Net

type MongoClientSettings = {
        ConnectionMode : ConnectionMode
        ConnectionTimeout : TimeSpan
        GuidRepresentation : GuidRepresentation
        IPv6 : bool
        LocalThreshold : TimeSpan
        MaxConnectionIdleTime : TimeSpan
        MaxConnectionLifeTime : TimeSpan
        MaxConnectionPoolSize : int
        MinConnectionPoolSize : int
        ReadConcern : ReadConcern
        ReadPreference : ReadPreference
        ReplicaSetName : string option
        Servers : seq<DnsEndPoint>
        ServerSelectionTimeout : TimeSpan
        SocketTimeout : TimeSpan
        WaitQueueSize : int
        WaitQueueTimeout : TimeSpan
        WriteConcern : WriteConcern
    }

    with
        static member Defaults = {
                ConnectionMode = ConnectionMode.Automatic
                ConnectionTimeout = TimeSpan.FromSeconds(30.0)
                GuidRepresentation = GuidRepresentation.CSharpLegacy
                IPv6 = false
                LocalThreshold = TimeSpan.FromMilliseconds(15.0)
                MaxConnectionIdleTime = TimeSpan.FromMinutes(10.0)
                MaxConnectionLifeTime = TimeSpan.FromMinutes(30.0)
                MaxConnectionPoolSize = 100
                MinConnectionPoolSize = 0
                ReadConcern = ReadConcern.Default
                ReadPreference = ReadPreference.Primary
                ReplicaSetName = None
                Servers = [new DnsEndPoint("localhost", 27017)]
                ServerSelectionTimeout = TimeSpan.FromSeconds(30.0)
                SocketTimeout = TimeSpan.Zero // use operating system default
                WaitQueueSize = 500
                WaitQueueTimeout = TimeSpan.FromMinutes(2.0)
                WriteConcern = WriteConcern.Acknowledged
            }

        static member internal FromCSharp(csharp : MongoDB.Driver.MongoClientSettings) = {
                ConnectionMode = ConnectionMode.FromCSharp(csharp.ConnectionMode)
                ConnectionTimeout = csharp.ConnectTimeout
                GuidRepresentation = GuidRepresentation.FromCSharp(csharp.GuidRepresentation)
                IPv6 = csharp.IPv6
                LocalThreshold = csharp.LocalThreshold
                MaxConnectionIdleTime = csharp.MaxConnectionIdleTime
                MaxConnectionLifeTime = csharp.MaxConnectionLifeTime
                MaxConnectionPoolSize = csharp.MaxConnectionPoolSize
                MinConnectionPoolSize = csharp.MinConnectionPoolSize
                ReadConcern = ReadConcern.FromCSharp(csharp.ReadConcern)
                ReadPreference = ReadPreference.FromCSharp(csharp.ReadPreference)
                ReplicaSetName = csharp.ReplicaSetName |> NullToOption
                Servers = csharp.Servers |> Seq.map (fun s -> DnsEndPoint.FromCSharp(s))
                ServerSelectionTimeout = csharp.ServerSelectionTimeout
                SocketTimeout = csharp.SocketTimeout
                WaitQueueSize = csharp.WaitQueueSize
                WaitQueueTimeout = csharp.WaitQueueTimeout
                WriteConcern = WriteConcern.FromCSharp(csharp.WriteConcern)
            }
            
        member internal this.ToCSharp() =
            let csharp =
                MongoDB.Driver.MongoClientSettings(
                    ConnectionMode = this.ConnectionMode.ToCSharp(),
                    ConnectTimeout = this.ConnectionTimeout,
                    GuidRepresentation = this.GuidRepresentation.ToCSharp(),
                    IPv6 = this.IPv6,
                    LocalThreshold = this.LocalThreshold,
                    MaxConnectionIdleTime = this.MaxConnectionIdleTime,
                    MaxConnectionLifeTime = this.MaxConnectionLifeTime,
                    MaxConnectionPoolSize = this.MaxConnectionPoolSize,
                    MinConnectionPoolSize = this.MinConnectionPoolSize,
                    ReadConcern = this.ReadConcern.ToCSharp(),
                    ReadPreference = this.ReadPreference.ToCSharp(),
                    ReplicaSetName = (this.ReplicaSetName |> OptionToNull),
                    Servers = (this.Servers |> Seq.map (fun s -> s.ToCSharp())),
                    ServerSelectionTimeout = this.ServerSelectionTimeout,
                    WaitQueueSize = this.WaitQueueSize,
                    WaitQueueTimeout = this.WaitQueueTimeout,
                    WriteConcern = this.WriteConcern.ToCSharp())
            csharp.Freeze()
