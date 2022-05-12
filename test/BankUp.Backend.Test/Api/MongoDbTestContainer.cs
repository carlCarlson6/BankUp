using DotNet.Testcontainers.Containers.Configurations;
using DotNet.Testcontainers.Containers.Configurations.Abstractions;
using DotNet.Testcontainers.Containers.Modules.Abstractions;
using DotNet.Testcontainers.Containers.WaitStrategies;

namespace BankUp.Backend.Test.Api;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class MongoDbTestContainer : TestcontainerDatabase
{
    internal MongoDbTestContainer(ITestcontainersConfiguration configuration) : base(configuration) { }
    
    public override string ConnectionString => $"mongodb://{Username}:{Password}@{Hostname}:{Port}";
}

public class MongoDbTestContainerConfiguration : TestcontainerDatabaseConfiguration
{
    public const string MongoDbImage = "mongo:5.0.6";
    public const int MongoDbPort = 27017;
    
    public MongoDbTestContainerConfiguration() : base(MongoDbImage, MongoDbPort, 88) { }
    
    public override string Database
    {
        get => Environments["MONGO_INITDB_DATABASE"];
        set => Environments["MONGO_INITDB_DATABASE"] = value;
    }
    
    public override string Username
    {
        get => Environments["MONGO_INITDB_ROOT_USERNAME"];
        set => Environments["MONGO_INITDB_ROOT_USERNAME"] = value;
    }
    
    public override string Password
    {
        get => Environments["MONGO_INITDB_ROOT_PASSWORD"];
        set => Environments["MONGO_INITDB_ROOT_PASSWORD"] = value;
    }
    
    public override IWaitForContainerOS WaitStrategy => Wait.ForUnixContainer()
        .UntilCommandIsCompleted("mongo", $"localhost:{DefaultPort}", "--eval", "db.runCommand(\"ping\").ok", "--quiet");
}