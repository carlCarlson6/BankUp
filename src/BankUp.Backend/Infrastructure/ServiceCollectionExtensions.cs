using BankUp.Backend.Groups.Infrastructure;
using MongoDB.Driver;

namespace BankUp.Backend.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        var mongoDbConfiguration = configuration.Get<MongoDbConfiguration>();
        var client = new MongoClient(mongoDbConfiguration.ConnectionString);
        var database = client.GetDatabase(mongoDbConfiguration.DatabaseName);

        return services
            .AddGroupsCollection(database);
    }
}