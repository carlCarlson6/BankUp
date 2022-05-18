using BankUp.Backend.Groups.Infrastructure.MongoDb;
using BankUp.Backend.Groups.Members;
using MongoDB.Driver;

namespace BankUp.Backend.Groups.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGroupsServices(this IServiceCollection services) => services
        .AddSingleton<IGroupRepository, GroupMongoRepository>()
        .AddSingleton<IInvitationService, InvitationService>();
    
    public static IServiceCollection AddGroupsCollection(this IServiceCollection services, IMongoDatabase mongoDatabase) => services
        .AddSingleton(mongoDatabase.GetCollection<GroupDocument>("groups"));
}