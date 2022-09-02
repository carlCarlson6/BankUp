using BankUp.Backend.Groups.CreateGroup;
using BankUp.Backend.Groups.Infrastructure.MongoDb;
using MediatR;
using MongoDB.Driver;

namespace BankUp.Backend.Groups.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGroupsServices(this IServiceCollection services) => services
        .AddSingleton<IGroupRepository, GroupMongoRepository>()
        .AddUseCases();

    private static IServiceCollection AddUseCases(this IServiceCollection services) => services
        .AddSingleton<IUseCase<CreateGroupCommand, Group>, GroupCreator>();
    
    public static IServiceCollection AddGroupsCollection(this IServiceCollection services, IMongoDatabase mongoDatabase) => services
        .AddSingleton(mongoDatabase.GetCollection<GroupDocument>("groups"));
}