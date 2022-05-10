using Monads;
using MongoDB.Driver;

namespace BankUp.Backend.Groups.Infrastructure;

public class GroupMongoRepository : IGroupRepository
{
    private readonly IMongoCollection<GroupModel> _collection;
    
    public GroupMongoRepository(IMongoCollection<GroupModel> collection) => _collection = collection;

    public async Task<Result<Group>> Store(Group group)
    {
        try
        {
            await _collection.InsertOneAsync(GroupModel.FromDomain(group));
            return Result<Group>.Ok(group);
        }
        catch (Exception exception)
        {
            return Result<Group>.Ko(new Error("DB ERROR", exception));
        }
    }
}